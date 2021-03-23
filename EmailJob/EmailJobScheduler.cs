using ISM.WebApp.Constant;
using ISM.WebApp.Models;
using ISM.WebApp.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Xml;

namespace EmailJob
{
    public class EmailJobScheduler
    {
        private List<Notification> GetNotificationsDegree()
        {
            SqlConnection con = null;
            String sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<Notification> notifications = new List<Notification>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                sql = NotificationConst.GET_ALL_NOTIFICATION_DEGREE;
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Notification notification = new Notification();
                    notification.user_id = (int)reader.GetValue(reader.GetOrdinal("user_id"));
                    notification.fullname = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    notification.email = (string)reader.GetValue(reader.GetOrdinal("email"));
                    if (!reader.IsDBNull(reader.GetOrdinal("type")))
                    {
                        notification.type = (string)reader.GetValue(reader.GetOrdinal("type"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("days_before")))
                    {
                        notification.days_before = (int)reader.GetValue(reader.GetOrdinal("days_before"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Passport_Expired")))
                    {
                        notification.passport_expired = (DateTime)reader.GetValue(reader.GetOrdinal("Passport_Expired"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Visa_Expired")))
                    {
                        notification.visa_expired = (DateTime)reader.GetValue(reader.GetOrdinal("Visa_Expired"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("deadline")))
                    {
                        notification.deadline = (DateTime)reader.GetValue(reader.GetOrdinal("deadline"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("ORT_Date")))
                    {
                        notification.ort_date = (DateTime)reader.GetValue(reader.GetOrdinal("ORT_Date"));
                    }
                    notifications.Add(notification);
                }
                return notifications;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, reader, null);
            }
            return null;
        }

        public Config GetConfigs()
        {
            try
            {
                Config config = new Config();
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appConfig.json");
                var configruration = builder.Build();
                config.email = configruration["email"];
                config.password = configruration["password"];
                config.host = configruration["host"];
                config.port = Int32.Parse(configruration["post"]);
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
                Config config = GetConfigs();
                var fromEmailAdress = new MailAddress(config.email);
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

        public void Execute()
        {
            var now = DateTime.Now;
            List<Notification> notifications = GetNotificationsDegree();
            try
            {
                foreach (var item in notifications)
                {
                    if (item.type.Equals("passport") || item.type.Equals("visa"))
                    {
                        var expiredDate = item.passport_expired;
                        var daysBefore = item.days_before;
                        var totalDays = expiredDate.Subtract(now).Days;
                        if (totalDays <= daysBefore)
                        {
                            string subject = "Passport Notification";
                            string body = "Hello " + item.fullname + ",\n\nYour Your " + item.type + " " +
                                          "expires on " + item.passport_expired.ToString("yyyy-MMM-dd") + ", you have " +
                                          "" + totalDays.ToString() + " days left before it expires. Please renew!";
                            SendMail(item.email, subject, body);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
