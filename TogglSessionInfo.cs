using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace TogglAPI
{
    [DataContract]
    public class TogglSessionInfo
        : IExtensibleDataObject
    {

        [DataMember(Name = "since")]
        public int Since { get; set; }

        [DataMember(Name = "data")]
        public ToggleSessionData Data { get; set; }

        public ExtensionDataObject ExtensionData { get; set; }
    }

}
