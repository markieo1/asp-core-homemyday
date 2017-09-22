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

        public EmailServices()
        {
            message = new MimeMessage();
            message.From.Add(new MailboxAddress("HomeMyWay", "kenwai2010@gmail.com"));
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

				await client.ConnectAsync("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);

                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate("kenwai2010@gmail.com", "qfevaksissurlgvt");

                await client.SendAsync(this.message);
                await client.DisconnectAsync(true);
            }
        }
        
    }
}
