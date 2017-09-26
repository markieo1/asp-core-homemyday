using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Services
{
	/// <summary>
	/// Email Smtp Setting
	/// </summary>
	public class AuthMailServices
    {
		public string UserName { get; set; }
		public string Password { get; set; }
		public string SmtpServer { get; set; }
		public int SmtpPort { get; set; }
		public string SmtpMailFromEmail { get; set; }
		public string SmtpMailFromName { get; set; }
	}
}
