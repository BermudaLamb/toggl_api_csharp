using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TogglAPI
{
    [DataContract]
    public class ToggleSessionData
        : Toggl
    {
        [DataMember(Name = "api_token")]
        public string ApiToken { get; set; }

        [DataMember(Name = "defailt_wid")]
        public int DefaultWid { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "fullname")]
        public string Name { get; set; }

        [DataMember(Name = "date_format")]
        public string DateFormat { get; set; }

        [DataMember(Name = "beginning_of_week")]
        public int BeginningOfWeek { get; set; }

        [DataMember(Name = "workspaces")]
        public IEnumerable<TogglWorkspace> Workspaces { get; set; }

    }

}
