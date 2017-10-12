using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Web.Services
{
	/// <summary>
	/// Email Smtp Setting
	/// </summary>
	public class MailServiceOptions
    {
		/// <summary>
		/// Smtp username
		/// </summary>
		public string UserName { get; set; }
		/// <summary>
		/// smtp password
		/// </summary>
		public string Password { get; set; }
		/// <summary>
		/// smtp server url
		/// </summary>
		public string SmtpServer { get; set; }
		/// <summary>
		/// smtp port
		/// </summary>
		public int SmtpPort { get; set; }
		/// <summary>
		/// from who it is send
		/// </summary>
		public string SmtpMailFromEmail { get; set; }
		/// <summary>
		/// Mail name
		/// </summary>
		public string SmtpMailFromName { get; set; }
	}
}
