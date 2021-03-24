using ISM.WebApp.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ISM.WebApp.Helper
{
    public class EmailHelper
    {
        private EmailConfig GetEmailConfig()
        {
            try
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                                        .AddEnvironmentVariables();
                var _config = builder.Build();
                EmailConfig config = new EmailConfig();
                config.email = _config.GetSection("EmailConfig").GetSection("email").Value;
                config.password = _config.GetSection("EmailConfig").GetSection("password").Value;
                config.host = _config.GetSection("EmailConfig").GetSection("host").Value;
                config.port = Int32.Parse(_config.GetSection("EmailConfig").GetSection("port").Value);
                return config;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
        public void SendMail(string toEmail, string subject, string content)
        {
            try
            {
                EmailConfig config = GetEmailConfig();
                var fromEmailAdress = new MailAddress(config.email, "ISM Notification");
                var toEmailAdress = new MailAddress(toEmail);
                MailMessage message = new MailMessage(fromEmailAdress, toEmailAdress);
                message.Subject = subject;
                message.Body = content;

                var client = new SmtpClient();
                client.Credentials = new NetworkCredential(config.email, config.password);
                client.Host = config.host;
                client.EnableSsl = true;
                client.Port = config.port;
                client.Send(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
