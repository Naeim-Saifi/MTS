using System;
using System.Collections.Generic;
using System.Text;

namespace MTS.Models
{
	public class Tokens
	{
		public UserModel User { get; set; }
		public string Token { get; set; }
		public string Role { get; set; }
		public string RefreshToken { get; set; }
	}
}
