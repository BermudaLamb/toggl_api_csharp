using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace TogglAPI
{
    public static class TogglSession
    {
        private const string TogglAuthUrl = "https://www.toggl.com/api/v8/me";

        private const string AuthenticationType = "Basic";

        public static TogglSessionInfo TogglSessionInfo(string ApiToken, string Password = "api_token")
        {
            TogglSessionInfo tsi = default(TogglSessionInfo);
            using (var client = new HttpClient())
            {
                try
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, TogglAuthUrl);
                    //request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    request.Headers.Authorization = new AuthenticationHeaderValue(
                        AuthenticationType,
                        Convert.ToBase64String(Encoding.UTF8.GetBytes(
                            string.Format("{0}:{1}", ApiToken, Password))));
                    var response = client.SendAsync(request).Result;
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(TogglSessionInfo));
                    using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(response.Content.ReadAsStringAsync().Result)))
                    {
                        tsi = ser.ReadObject(ms) as TogglSessionInfo;
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                return tsi;
            }
        }

        public static string Json<T>(T obj)
        {
            string rslt = string.Empty;
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                using (MemoryStream ms = new MemoryStream())
                {
                    ser.WriteObject(ms, obj);
                    ms.Seek(0, SeekOrigin.Begin);
                    using (StreamReader rdr = new StreamReader(ms))
                    {
                        rslt = rdr.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rslt;
        }

        public static TogglDataObj unJson(string json)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(TogglDataObj));
            using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                return ser.ReadObject(ms) as TogglDataObj;
            }
        }

        public static TogglDataObj PostDetails<T>(string ApiToken, T obj, string Password = "api_token", string RequestUrl = null)
        {
            if (RequestUrl == null)
                return default(TogglDataObj);
            TogglDataObj result = default(TogglDataObj);
            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                        AuthenticationType,
                        Convert.ToBase64String(Encoding.UTF8.GetBytes(
                            string.Format("{0}:{1}", ApiToken, Password))));

                    string str = Json<T>(obj);
#if debug
                    Console.WriteLine(str);
#endif
                    StringContent content = new StringContent(str, Encoding.UTF8, "application/json");
                    var response = client.PostAsync(RequestUrl, content).Result;
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        result = unJson(response.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        string rslt = response.Content.ReadAsStringAsync().Result;
                        throw new Exception(rslt);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }

        public static TogglDataObj PutDetails<T>(string ApiToken, T obj, string Password = "api_token", string RequestUrl = null)
        {
            if (RequestUrl == null)
                return default(TogglDataObj);
            TogglDataObj result = default(TogglDataObj);
            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                        AuthenticationType,
                        Convert.ToBase64String(Encoding.UTF8.GetBytes(
                            string.Format("{0}:{1}", ApiToken, Password))));

                    string str = Json<T>(obj);
#if debug
                    Console.WriteLine(str);
#endif
                    StringContent content = new StringContent(str, Encoding.UTF8, "application/json");
                    var response = client.PutAsync(RequestUrl, content).Result;
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        result = unJson(response.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        string rslt = response.Content.ReadAsStringAsync().Result;
                        throw new Exception(rslt);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }

    }
}
