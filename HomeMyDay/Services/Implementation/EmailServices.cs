using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Services.Implementation
{
    public class EmailServices : IEmailServices
    {
        public MimeMessage message;

		private readonly MailServiceOptions Options;

		public EmailServices(IOptions<MailServiceOptions> optionsAccessor)
        {
            message = new MimeMessage();
			Options = optionsAccessor.Value;
			message.From.Add(new MailboxAddress(Options.SmtpMailFromName, Options.SmtpMailFromEmail));
		}

		public async Task SendEmailAsync(string email, string subject, string message)
        {
            this.message.To.Add(new MailboxAddress(email));
            this.message.Subject = subject;
            this.message.Body = new TextPart("plain")
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
				client.ServerCertificateValidationCallback = (s, c, h, e) => true;

				await client.ConnectAsync(Options.SmtpServer, Options.SmtpPort, SecureSocketOptions.SslOnConnect);

                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate(Options.UserName, Options.Password);

                await client.SendAsync(this.message);
                await client.DisconnectAsync(true);
            }
        }
        
    }
}
