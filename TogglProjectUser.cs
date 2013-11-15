using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TogglAPI
{
    [DataContract]
    public class TogglProjectUser
        : Toggl, IExtensibleDataObject
    {
        [DataMember(Name = "uid")]
        public int UserId { get; set; }

    }
}
