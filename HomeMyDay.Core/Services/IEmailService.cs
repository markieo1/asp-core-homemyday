using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Core.Services
{
	/// <summary>
	/// EmailServices inteface to send mails
	/// </summary>
	public interface IEmailService
    {
		/// <summary>
		/// Send the mail
		/// </summary>
		/// <param name="email">Target mail to send.</param>
		/// <param name="subject">Subject of the mail.</param>
		/// <param name="message">A message to send.</param>
		Task SendEmailAsync(string email, string subject, string message);
    }
}
