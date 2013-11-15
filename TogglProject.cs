using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class TogglProject
        : TogglWithDetails
    {
        protected override string DetailsUrl { get { return "https://www.toggl.com/api/v8/projects/{0}/tasks"; } }

        [DataMember(Name = "wid")]
        public int WorkspaceId { get; set; }

        [DataMember(Name = "billable")]
        public bool Billable { get; set; }

        [DataMember(Name = "is_private")]
        public bool IsPrivate { get; set; }

        [DataMember(Name = "active")]
        public bool Active { get; set; }

        [DataMember(Name = "at")]
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

        private const string TogglTaskUrl = "https://www.toggl.com/api/v8/tasks";

        public TogglTask AddTask(string name, string owner, string uApiToken, string ApiToken,
            IEnumerable<TogglUser> users, IEnumerable<TogglProjectUser> projectUsers)
        {
            TogglTask task = default(TogglTask);
            // create a new task
            TogglProjectUser projectUser = default(TogglProjectUser);
            TogglUser user = users.Where(x => x.Email.Equals(owner, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (user != null)
                projectUser = projectUsers.Where(x => x.UserId == user.Id).FirstOrDefault();
            if (projectUser == null) uApiToken = ApiToken;
            TogglTaskObj taskObj = new TogglTaskObj()
            {
                Task = new TogglTask()
                {
                    Name = name,
                    ProjectId = this.Id.Value,
                    Active = true
                }
            };
            task = taskObj.Task;
            try
            {
                TogglDataObj result = TogglSession.PostDetails<TogglTaskObj>(uApiToken, taskObj, RequestUrl: TogglTaskUrl);
                task.Id = (int)result.Data.ExtensionDataMembers["id"];
            }
            catch (Exception)
            {
                task = null;
            }
            return task;
        }

        public TogglTask UpdateTask(TogglTask task, string owner, string uApiToken, string ApiToken,
            IEnumerable<TogglUser> users, IEnumerable<TogglProjectUser> projectUsers)
        {
            TogglProjectUser projectUser = default(TogglProjectUser);
            TogglUser user = users.Where(x => x.Email.Equals(owner, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (user != null)
                projectUser = projectUsers.Where(x => x.UserId == user.Id).FirstOrDefault();
            if (projectUser == null) uApiToken = ApiToken;
            TogglTaskObj taskObj = new TogglTaskObj()
            {
                Task = task
            };
            try
            {
                TogglDataObj result = TogglSession.PutDetails<TogglTaskObj>(uApiToken, taskObj, 
                    RequestUrl: String.Format("{0}/{1}", TogglTaskUrl, task.Id));
            }
            catch (Exception)
            {
                task = null;
            }
            return task;
        }

    }

}
