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
    public class TogglTimeEntryObj
    {
        [DataMember(Name = "time_entry")]
        public TogglTimeEntry TimeEntry { get; set; }
    }

    [DataContract]
    public class TogglTimeEntry
        : Toggl
    {
        [DataMember(Name = "description")]
        public string Name { get; set; }

        [DataMember(Name = "wid", EmitDefaultValue = false)]
        public int? WorkspaceId { get; set; }

        [DataMember(Name = "pid", EmitDefaultValue = false)]
        public int? ProjectId { get; set; }

        [DataMember(Name = "tid")]
        public int TaskId { get; set; }

        [DataMember(Name = "billable", EmitDefaultValue = false)]
        public bool? Billable { get; set; }

        [DataMember(Name = "start")]
        public string StartStr { get; set; }

        [DataMember(Name = "stop", EmitDefaultValue = false)]
        public string StopStr { get; set; }

        [DataMember(Name = "duration")]
        public int Duration { get; set; }

        [DataMember(Name = "created_with")]
        public string CreatedWith { get; set; }

        [DataMember(Name = "tags")]
        public List<string> Tags { get; set; }

        public DateTime Start
        {
            get
            {
                return Convert.ToDateTime(StartStr);
            }
            set
            {
                string append = value.ToString("%K");
                if (string.IsNullOrEmpty(append)) append = DateTime.Now.ToString("%K");
                StartStr = value.ToString("s", DateTimeFormatInfo.InvariantInfo) + append;
            }
        }

        public DateTime Stop
        {
            get
            {
                return Convert.ToDateTime(StopStr);
            }
            set
            {
                string append = value.ToString("%K");
                if (string.IsNullOrEmpty(append)) append = DateTime.Now.ToString("%K");
                StopStr = value.ToString("s", DateTimeFormatInfo.InvariantInfo) + append;
            }
        }

        public TogglTimeEntry(TogglTask task, string description, bool? billable, DateTime start, int duration,
            List<string> tags)
        {
            this.Billable = billable;
            this.CreatedWith = "TogglConsole";
            this.Duration = duration;
            this.Name = description;
            this.Start = start;
            this.Tags = tags;
            //this.WorkspaceId = task.WorkspaceId.Value;
            //this.ProjectId = task.ProjectId;
            this.TaskId = task.Id.Value;
        }
    }
}
