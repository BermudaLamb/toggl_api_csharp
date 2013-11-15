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
    public class TogglWorkspace
        : TogglWithDetails
    {
        protected override string DetailsUrl { get { return "https://www.toggl.com/api/v8/workspaces/{0}/projects"; } }

    }
}
