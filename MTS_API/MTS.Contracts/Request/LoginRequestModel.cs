using System;
using System.Collections.Generic;
using System.Text;

namespace MTS.Contracts.Request
{
    public class LoginRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
