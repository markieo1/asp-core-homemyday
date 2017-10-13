using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;
using HomeMyDay.Core.Services;
using HomeMyDay.Infrastructure.Options;

namespace HomeMyDay.Infrastructure.Services
{
	public class EmailService : IEmailService
    {
        public MimeMessage message;

		private readonly MailServiceOptions _options;

		public EmailService(IOptions<MailServiceOptions> optionsAccessor)
        {
            message = new MimeMessage();
			_options = optionsAccessor.Value;
			message.From.Add(new MailboxAddress(_options.SmtpMailFromName, _options.SmtpMailFromEmail));
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

				await client.ConnectAsync(_options.SmtpServer, _options.SmtpPort, SecureSocketOptions.SslOnConnect);

                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate(_options.UserName, _options.Password);

                await client.SendAsync(this.message);
                await client.DisconnectAsync(true);
            }
        }
        
    }
}
