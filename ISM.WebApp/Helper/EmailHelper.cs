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
                config.email = _config.GetValue<string>("EmailConfig:email");
                config.password = _config.GetValue<string>("EmailConfig:password");
                config.host = _config.GetValue<string>("EmailConfig:host");
                config.port = _config.GetValue<int>("EmailConfig:port");
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
                string display = "ISM Notification";
                EmailConfig config = GetEmailConfig();
                var fromEmailAdress = new MailAddress(config.email,display);
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
