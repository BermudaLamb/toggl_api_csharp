using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace TogglAPI
{
    [DataContract]
    public class TogglDataObj
    {
        [DataMember(Name = "data")]
        public TogglData Data { get; set; }
    }

    [DataContract]
    public class TogglData
        : IExtensibleDataObject
    {
        public ExtensionDataObject ExtensionData { get; set; }

        private Dictionary<string, object> _ExtensionData;

        public Dictionary<string, object> ExtensionDataMembers
        {
            get
            {
                if (_ExtensionData == null)
                    GetExtensionDataMembers();
                return _ExtensionData;
            }
        }

        public object GetExtensionDataMembers()
        {
            _ExtensionData = new Dictionary<string, object>();
            object innerValue = null;
            PropertyInfo membersProperty = typeof(ExtensionDataObject).GetProperty("Members", BindingFlags.NonPublic | BindingFlags.Instance);
            IList members = (IList)membersProperty.GetValue(ExtensionData, null);
            foreach (object member in members)
            {
                PropertyInfo[] Properties = member.GetType().GetProperties();//y("Name", BindingFlags.IgnoreCase);
                PropertyInfo nameProperty = Properties.Where(x => x.Name.Equals("Name", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                if (nameProperty == null) continue;
                string name = (string)nameProperty.GetValue(member, null);
                PropertyInfo valueProperty = Properties.Where(x => x.Name.Equals("Value", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                if (valueProperty != null)
                {
                    object value = valueProperty.GetValue(member, null);
                    PropertyInfo innerValueProperty = value.GetType().GetProperty("Value", BindingFlags.Public | BindingFlags.Instance);
                    innerValue = innerValueProperty.GetValue(value, null);
                }
                _ExtensionData.Add(name, innerValue);
            }
            return innerValue;
        }

    }

}
