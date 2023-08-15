using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MTS.DataAccess.Entities
{
    public class Role: IdentityRole<int>
    {
        public Role()
        {
        }
        public Role(string name)
         : this()
        {
            this.Name = name;
        }
        public string ShowingItsPossible { get; set; }
    }
}
