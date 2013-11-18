using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace TogglAPI
{
    [DataContract]
    abstract public class TogglWithDetails
        : Toggl
    {
        private const string AuthenticationType = "Basic";

        abstract protected string DetailsUrl { get; }

        public T GetDetails<T>(string ApiToken, string Password = "api_token", string RequestUrl = null, string query = null)
        {
            if (RequestUrl == null)
                RequestUrl = DetailsUrl;
            if (RequestUrl == null)
                return default(T);
            T details = default(T);
            using (var client = new HttpClient())
            {
                try
                {
                    UriBuilder uri = new UriBuilder(string.Format(RequestUrl, this.Id));
                    if (!string.IsNullOrEmpty(query))
                    {
                        uri.Query = WebUtility.UrlEncode(query);//.Replace("%3D", "=");//.Replace("%26", "&");
                        uri.Query = uri.Query.Replace("%3D", "="); //.Replace("%26", "&");
                    }
                    var request = new HttpRequestMessage(HttpMethod.Get, uri.Uri);
                    //request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    request.Headers.Authorization = new AuthenticationHeaderValue(
                        AuthenticationType,
                        Convert.ToBase64String(Encoding.UTF8.GetBytes(
                            string.Format("{0}:{1}", ApiToken, Password))));
                    var response = client.SendAsync(request).Result;
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                    using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(response.Content.ReadAsStringAsync().Result)))
                    {
                        details = (T)ser.ReadObject(ms);
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            return details;
        }

        public string PostDetails<T>(string ApiToken, string Password, T obj, string RequestUrl = null)
        {
            if (RequestUrl == null)
                RequestUrl = DetailsUrl;
            if (RequestUrl == null)
                return string.Empty;
            string result = string.Empty;
            using (var client = new HttpClient())
            {
                try
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                    string str;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        ser.WriteObject(ms, obj);
                        ms.Seek(0, SeekOrigin.Begin);
                        using (StreamReader rdr = new StreamReader(ms))
                        {
                            str = rdr.ReadToEnd();
                        }
                    }
                    StringContent content = new StringContent(str, Encoding.UTF8, "application/json");

                    //var request = new HttpRequestMessage(HttpMethod.Post,
                    //    String.Format(RequestUrl, this.Id),
                    //    content);
                    content.Headers.Add("Authorization", new AuthenticationHeaderValue(
                        AuthenticationType,
                        Convert.ToBase64String(Encoding.UTF8.GetBytes(
                            string.Format("{0}:{1}", ApiToken, Password)))).Parameter);
                    var response = client.PostAsync(String.Format(RequestUrl, this.Id), content).Result;
                    result = response.Content.ReadAsStringAsync().Result;
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            return result;
        }

    }
}
