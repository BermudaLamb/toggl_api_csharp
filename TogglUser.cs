using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TogglAPI
{
    [DataContract]
    public class TogglUser
        : TogglWithDetails, IExtensibleDataObject
    {
        protected override string DetailsUrl
        {
            get { return null; }
        }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "at")]
        public string AtStr { get; set; }

        [DataMember(Name = "admin")]
        public bool Admin { get; set; }

        [DataMember(Name = "active")]
        public bool Active { get; set; }

        public DateTime At
        {
            get
            {
                return Convert.ToDateTime(AtStr);
            }
            set
            {
                AtStr = Convert.ToString(value);
            }
        }

    }
}
