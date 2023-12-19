using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows;

namespace Database_Hospital_Application.Models.Tools
{
    public class TwoWayAuth
    {
        private readonly string smtpServer;
        private readonly int smtpPort;
        private readonly string smtpUsername;
        private readonly string smtpPassword;

        public TwoWayAuth(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword)
        {
            this.smtpServer = smtpServer;
            this.smtpPort = smtpPort;
            this.smtpUsername = smtpUsername;
            this.smtpPassword = smtpPassword;
        }

        public TwoWayAuth(IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            var emailConfiguration = configuration.GetSection("EmailConfiguration");
            this.smtpServer = emailConfiguration["SmtpServer"];
            this.smtpPort = int.Parse(emailConfiguration["SmtpPort"]);
            this.smtpUsername = emailConfiguration["SmtpUsername"];
            this.smtpPassword = emailConfiguration["SmtpPassword"];
        }

        public async Task SendEmailAsync(string toEmailAddress, string subject, string body)
        {
            MailMessage mail = new MailMessage
            {
                From = new MailAddress(smtpUsername),
                Sender = new MailAddress(smtpUsername),
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };
            mail.To.Add(new MailAddress(toEmailAddress));

            SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                EnableSsl = true
            };

            try
            {
                await smtpClient.SendMailAsync(mail);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Email se nepodařilo odeslat.");
                throw;
            }
            finally
            {
                mail.Dispose();
            }
        }
    }
}
