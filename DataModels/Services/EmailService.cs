using MailKit.Security;
using Microsoft.AspNet.Identity;
using MimeKit;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace DataModels.Services
{
    public class EmailService
    {
        public static async Task<bool> SendMailAsync(IdentityMessage message)
        {
            var mailSettings = new MailSettings();
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail);
            email.From.Add(new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail));
            email.To.Add(MailboxAddress.Parse(message.Destination));
            email.Subject = message.Subject;

            var builder = new BodyBuilder
            {
                HtmlBody = message.Body
            };
            email.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                await smtp.ConnectAsync(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(mailSettings.Mail, mailSettings.Password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
                return true;
            }
            catch
            {
                System.IO.Directory.CreateDirectory("MailError");
                var emailsavefile = $@"MailError/{Guid.NewGuid()}.eml";
                await email.WriteToAsync(emailsavefile);
                return false;
            }

        }

    }

    public class MailSettings
    {
        public string Host { get; set; } = ConfigurationManager.AppSettings["Host"];
        public int Port { get; set; } = int.Parse(ConfigurationManager.AppSettings["Port"]);
        public string DisplayName { get; set; } = ConfigurationManager.AppSettings["DisplayName"];
        public string Mail { get; set; } = ConfigurationManager.AppSettings["Mail"];
        public string Password { get; set; } = ConfigurationManager.AppSettings["Password"];

    }
}
