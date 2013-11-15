using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TogglAPI
{
    [DataContract]
    public class TogglTaskObj
    {
        [DataMember(Name = "task")]
        public TogglTask Task { get; set; }
    }

    [DataContract]
    public class TogglTask
        : Toggl
    {
        //        [DataMember(Name = "id", EmitDefaultValue = false)]
        //        public int? Id { get; set; }

        [DataMember(Name = "wid", EmitDefaultValue = false)]
        public int? WorkspaceId { get; set; }

        [DataMember(Name = "pid")]
        public int ProjectId { get; set; }

        [DataMember(Name = "uid", EmitDefaultValue = false)]
        public int? UserId { get; set; }

        [DataMember(Name = "fields", EmitDefaultValue = false)]
        public string Fields
        {
            get
            {
                return String.IsNullOrEmpty(UName) ? null : "uname";
            }
            set
            {
            }
        }

        [DataMember(Name = "uname", EmitDefaultValue = false)]
        public string UName { get; set; }

        [DataMember(Name = "active")]
        public bool Active { get; set; }

        [DataMember(Name = "at", EmitDefaultValue = false)]
        public string AtStr { get; set; }

        public DateTime At
        {
            get
            {
                return Convert.ToDateTime(AtStr);
            }
            set
            {
                string append = value.ToString("%K");
                if (string.IsNullOrEmpty(append)) append = DateTime.Now.ToString("%K");
                AtStr = value.ToString("s", DateTimeFormatInfo.InvariantInfo) + append;
            }
        }

        [DataMember(Name = "estimated_seconds", EmitDefaultValue = false)]
        public int Duration { get; set; }

        //        public ExtensionDataObject ExtensionData { get; set; }
    }
}
