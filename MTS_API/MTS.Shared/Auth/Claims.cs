using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTS.Shared.Auth
{
    public class Claims
    {
        public string Id { get; set; }
        //public string SchoolId { get; set; }
        //public string SchoolName { get; set; }
        public string AuthToken { get; set; }
        public int ExpiresIn { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string OauthToken { get; set; }
        public string OauthTokenSecret { get; set; }
        public string OauthCallbackConfirmed { get; set; }
        public int AccessFailedCount { get; set; }
    }
}
