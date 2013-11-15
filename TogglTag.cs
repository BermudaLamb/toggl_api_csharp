using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TogglAPI
{
    [DataContract]
    public class TogglTag
        : Toggl
    {
        [DataMember(Name = "wid")]
        public int WorkspaceId { get; set; }

    }
}
