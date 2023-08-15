using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MTS.DataAccess.Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime MailActivationTime { get; set; }
        public DateTime ResetMailActivationTime { get; set; }
    }
}
