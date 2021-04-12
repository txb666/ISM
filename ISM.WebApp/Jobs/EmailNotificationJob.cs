using ISM.WebApp.Constant;
using ISM.WebApp.Helper;
using ISM.WebApp.Models;
using ISM.WebApp.Utils;
using Microsoft.Data.SqlClient;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ISM.WebApp.Jobs
{
    public class EmailNotificationJob : IJob
    {
        private List<Notification> GetNotificationsDegree(string type)
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
                if (type.Equals("passport"))
                {
                    sql = NotificationConst.GET_ALL_NOTIFICATION_DEGREE_PASSPORT;
                    com.CommandText = sql;
                    reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        Notification notification = new Notification();
                        notification.user_id = (int)reader.GetValue(reader.GetOrdinal("user_id"));
                        if (!reader.IsDBNull(reader.GetOrdinal("isUpdatePassport")))
                        {
                            notification.isUpdatePassport = (bool)reader.GetValue(reader.GetOrdinal("isUpdatePassport"));
                        }
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
                        notifications.Add(notification);
                    }
                }
                if (type.Equals("visa"))
                {
                    sql = NotificationConst.GET_ALL_NOTIFICATION_DEGREE_VISA;
                    com.CommandText = sql;
                    reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        Notification notification = new Notification();
                        notification.user_id = (int)reader.GetValue(reader.GetOrdinal("user_id"));
                        if (!reader.IsDBNull(reader.GetOrdinal("isUpdateVisa")))
                        {
                            notification.isUpdateVisa = (bool)reader.GetValue(reader.GetOrdinal("isUpdateVisa"));
                        }
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
                        if (!reader.IsDBNull(reader.GetOrdinal("Visa_Expired")))
                        {
                            notification.visa_expired = (DateTime)reader.GetValue(reader.GetOrdinal("Visa_Expired"));
                        }
                        notifications.Add(notification);
                    }
                }
                if (type.Equals("orientation"))
                {
                    sql = NotificationConst.GET_ALL_NOTIFICATION_DEGREE_ORIENTATION;
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
                        if (!reader.IsDBNull(reader.GetOrdinal("ORT_Date")))
                        {
                            notification.ort_date = (DateTime)reader.GetValue(reader.GetOrdinal("ORT_Date"));
                        }
                        notifications.Add(notification);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, reader, null);
            }
            return notifications;
        }

        public List<Notification> GetNotificationsMobility(string type)
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
                if (type.Equals("passport"))
                {
                    sql = NotificationConst.GET_ALL_NOTIFICATION_MOBILITY_PASSPORT;
                    com.CommandText = sql;
                    reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        Notification notification = new Notification();
                        notification.user_id = (int)reader.GetValue(reader.GetOrdinal("user_id"));
                        if (!reader.IsDBNull(reader.GetOrdinal("isUpdatePassport")))
                        {
                            notification.isUpdatePassport = (bool)reader.GetValue(reader.GetOrdinal("isUpdatePassport"));
                        }
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
                        notifications.Add(notification);
                    }
                }
                if (type.Equals("visa"))
                {
                    sql = NotificationConst.GET_ALL_NOTIFICATION_MOBILITY_VISA;
                    com.CommandText = sql;
                    reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        Notification notification = new Notification();
                        notification.user_id = (int)reader.GetValue(reader.GetOrdinal("user_id"));
                        if (!reader.IsDBNull(reader.GetOrdinal("isUpdateVisa")))
                        {
                            notification.isUpdateVisa = (bool)reader.GetValue(reader.GetOrdinal("isUpdateVisa"));
                        }
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
                        if (!reader.IsDBNull(reader.GetOrdinal("Visa_Expired")))
                        {
                            notification.visa_expired = (DateTime)reader.GetValue(reader.GetOrdinal("Visa_Expired"));
                        }
                        notifications.Add(notification);
                    }
                }
                if (type.Equals("detail_agenda"))
                {
                    sql = NotificationConst.GET_ALL_NOTIFICATION_MOBILITY_DETAIL_AGENDA;
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
                        if (!reader.IsDBNull(reader.GetOrdinal("Detail_Agenda_Date")))
                        {
                            notification.detail_agenda_date = (DateTime)reader.GetValue(reader.GetOrdinal("Detail_Agenda_Date"));
                        }
                        notifications.Add(notification);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, reader, null);
            }
            return notifications;
        }

        private List<InsuranceFlightNotification> GetAllStudentWith(string type)
        {
            SqlConnection con = null;
            String sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<InsuranceFlightNotification> notifications = new List<InsuranceFlightNotification>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                if (type.Equals("AllStudentWithout"))
                {
                    sql = "select a.duration_start,c.isUpdateFlight,c.isUpdateInsurance,c.[user_id],c.email,c.fullname,a.student_group_id,b.[program_name],b.[type] from Student_Group a, Programs b, Users c where a.program_id = b.program_id and a.student_group_id = c.studentGroup_id";
                }
                if (type.Equals("AllStudentWithInsurance"))
                {
                    sql = "select a.duration_start,c.isUpdateFlight,c.isUpdateInsurance,c.[user_id],c.email,c.fullname,a.student_group_id,b.[program_name],b.[type] from Student_Group a, Programs b, Users c, Insurances d where a.program_id = b.program_id and a.student_group_id = c.studentGroup_id and c.[user_id] = d.student_id";
                }
                if (type.Equals("AllStudentWithFlight"))
                {
                    sql = "select a.duration_start,c.isUpdateFlight,c.isUpdateInsurance,c.[user_id],c.email,c.fullname,a.student_group_id,b.[program_name],b.[type] from Student_Group a, Programs b, Users c, Flights d where a.program_id = b.program_id and a.student_group_id = c.studentGroup_id and c.[user_id] = d.student_id";
                }
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    InsuranceFlightNotification insuranceFlightNotification = new InsuranceFlightNotification();
                    if (!reader.IsDBNull(reader.GetOrdinal("isUpdateFlight")))
                    {
                        insuranceFlightNotification.isUpdateFlight = (bool)reader.GetValue(reader.GetOrdinal("isUpdateFlight"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("isUpdateInsurance")))
                    {
                        insuranceFlightNotification.isUpdateInsurance = (bool)reader.GetValue(reader.GetOrdinal("isUpdateInsurance"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("duration_start")))
                    {
                        insuranceFlightNotification.duration_start = (DateTime)reader.GetValue(reader.GetOrdinal("duration_start"));
                    }
                    insuranceFlightNotification.student_id = (int)reader.GetValue(reader.GetOrdinal("user_id"));
                    insuranceFlightNotification.email = (string)reader.GetValue(reader.GetOrdinal("email"));
                    insuranceFlightNotification.fullname = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    insuranceFlightNotification.studentGroup_id = (int)reader.GetValue(reader.GetOrdinal("student_group_id"));
                    insuranceFlightNotification.program_name = (string)reader.GetValue(reader.GetOrdinal("program_name"));
                    insuranceFlightNotification.program_type = (string)reader.GetValue(reader.GetOrdinal("type"));
                    notifications.Add(insuranceFlightNotification);
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

        private List<NotificationConfig> GetNotificationConfigs()
        {
            SqlConnection con = null;
            String sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<NotificationConfig> notificationConfigs = new List<NotificationConfig>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                sql = "SELECT [config_id],[type],[days_before],[hours_before],[kind],[deadline] FROM [ISM].[dbo].[Config]";
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    NotificationConfig config = new NotificationConfig();
                    config.config_id = (int)reader.GetValue(reader.GetOrdinal("config_id"));
                    config.type = (string)reader.GetValue(reader.GetOrdinal("type"));
                    if (!reader.IsDBNull(reader.GetOrdinal("days_before")))
                    {
                        config.days_before = (int)reader.GetValue(reader.GetOrdinal("days_before"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("hours_before")))
                    {
                        config.hours_before = (int)reader.GetValue(reader.GetOrdinal("hours_before"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("kind")))
                    {
                        config.kind = (string)reader.GetValue(reader.GetOrdinal("kind"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("deadline")))
                    {
                        config.deadline = (DateTime)reader.GetValue(reader.GetOrdinal("deadline"));
                    }
                    notificationConfigs.Add(config);
                }
                return notificationConfigs;
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

        private List<MeetingNotification> GetMeetingNotification()
        {
            SqlConnection con = null;
            String sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<MeetingNotification> meetingNotifications = new List<MeetingNotification>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                sql = "select temp.meeting_schedule_id,temp.staff_id,temp.Staff_Name,temp.Staff_Email,temp2.student_id," +
                      "temp2.Student_Name,temp2.Student_Email,temp.[date] from (select a.meeting_schedule_id,a.staff_id," +
                      "b.fullname as Staff_Name,b.email as Staff_Email,a.[date],a.IsAccepted from Meeting_Schedule a, " +
                      "Users b where a.staff_id = b.[user_id] and a.IsAccepted = 1) as temp inner join (select " +
                      "c.meeting_schedule_id,c.student_id,d.fullname as Student_Name,d.email as Student_Email " +
                      "from Meeting_Schedule c, Users d where c.student_id = d.[user_id] and c.IsAccepted = 1) as " +
                      "temp2 on temp.meeting_schedule_id = temp2.meeting_schedule_id";
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    MeetingNotification config = new MeetingNotification();
                    config.mt_schedule_id = (int)reader.GetValue(reader.GetOrdinal("meeting_schedule_id"));
                    config.staff_id = (int)reader.GetValue(reader.GetOrdinal("staff_id"));
                    config.student_id = (int)reader.GetValue(reader.GetOrdinal("student_id"));
                    config.staff_name = (string)reader.GetValue(reader.GetOrdinal("Staff_Name"));
                    config.student_name = (string)reader.GetValue(reader.GetOrdinal("Student_Name"));
                    config.staff_email = (string)reader.GetValue(reader.GetOrdinal("Staff_Email"));
                    config.student_email = (string)reader.GetValue(reader.GetOrdinal("Student_Email"));
                    config.date = (DateTime)reader.GetValue(reader.GetOrdinal("date"));
                    meetingNotifications.Add(config);
                }
                return meetingNotifications;
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

        private void InsertNotificationInformation(int user_id, string title, string content)
        {
            SqlConnection con = null;
            string sql = "insert into Notification_Information([user_id],title,content) values(@user_id,@title,@content)";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@user_id", SqlDbType.Int);
                com.Parameters["@user_id"].Value = user_id;
                com.Parameters.Add("@title", SqlDbType.NVarChar);
                com.Parameters["@title"].Value = title;
                com.Parameters.Add("@content", SqlDbType.Text);
                com.Parameters["@content"].Value = content;
                com.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
        }

        private List<Notification> GetCoordinatorDegree()
        {
            SqlConnection con = null;
            String sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<Notification> coordinatorDegreeList = new List<Notification>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                sql = "select c.staff_id,d.fullname,d.email,a.student_group_id from Student_Group a, Programs b, Coordinators c," +
                      " Users d where a.program_id = b.program_id and a.student_group_id = c.studentGroup_id and c.staff_id = d.[user_id] and b.[type] = 'Degree'";
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Notification temp = new Notification();
                    temp.user_id = (int)reader.GetValue(reader.GetOrdinal("staff_id"));
                    temp.fullname = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    temp.email = (string)reader.GetValue(reader.GetOrdinal("email"));
                    temp.studentGroup_id = (int)reader.GetValue(reader.GetOrdinal("student_group_id"));
                    coordinatorDegreeList.Add(temp);
                }
                return coordinatorDegreeList;
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

        private List<Notification> GetCoordinatorMobility()
        {
            SqlConnection con = null;
            String sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<Notification> coordinatorDegreeList = new List<Notification>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                sql = "select c.staff_id,d.fullname,d.email,a.student_group_id from Student_Group a, Programs b, Coordinators c, " +
                      "Users d where a.program_id = b.program_id and a.student_group_id = c.studentGroup_id and c.staff_id = d.[user_id] and b.[type] = 'Mobility'";
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Notification temp = new Notification();
                    temp.user_id = (int)reader.GetValue(reader.GetOrdinal("staff_id"));
                    temp.fullname = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    temp.email = (string)reader.GetValue(reader.GetOrdinal("email"));
                    temp.studentGroup_id = (int)reader.GetValue(reader.GetOrdinal("student_group_id"));
                    coordinatorDegreeList.Add(temp);
                }
                return coordinatorDegreeList;
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

        private List<Notification> GetMobilityStudent()
        {
            SqlConnection con = null;
            String sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<Notification> coordinatorDegreeList = new List<Notification>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                sql = "select a.[user_id],a.fullname,a.email,a.studentGroup_id from Users a, Roles b where a.role_id = b.role_id and b.role_name = 'Mobility' and a.[status] = 1";
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Notification temp = new Notification();
                    temp.user_id = (int)reader.GetValue(reader.GetOrdinal("user_id"));
                    temp.fullname = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    temp.email = (string)reader.GetValue(reader.GetOrdinal("email"));
                    temp.studentGroup_id = (int)reader.GetValue(reader.GetOrdinal("studentGroup_id"));
                    coordinatorDegreeList.Add(temp);
                }
                return coordinatorDegreeList;
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

        private List<Notification> GetDegreeStudentAccomodation()
        {
            SqlConnection con = null;
            String sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<Notification> degreeAccomodationList = new List<Notification>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                sql = "select a.student_id,b.fullname,b.email,a.fee,b.isPayCurrentAccomodation from Current_Accommodation a, Users b where a.student_id = b.[user_id] and b.[status] = 1";
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Notification temp = new Notification();
                    temp.user_id = (int)reader.GetValue(reader.GetOrdinal("student_id"));
                    temp.fullname = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    temp.email = (string)reader.GetValue(reader.GetOrdinal("email"));
                    temp.fee = (double)reader.GetValue(reader.GetOrdinal("fee"));
                    if (!reader.IsDBNull(reader.GetOrdinal("isPayCurrentAccomodation")))
                    {
                        temp.isPay = (bool)reader.GetValue(reader.GetOrdinal("isPayCurrentAccomodation"));
                    }
                    degreeAccomodationList.Add(temp);
                }
                return degreeAccomodationList;
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

        private NotificationConfig GetConfigAccomodation()
        {
            SqlConnection con = null;
            String sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            NotificationConfig temp = new NotificationConfig();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                sql = "select a.[type],a.days_before from Config a where a.[type] = 'current_accomodation'";
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    temp.type = (string)reader.GetValue(reader.GetOrdinal("type"));
                    temp.days_before = (int)reader.GetValue(reader.GetOrdinal("days_before"));
                }
                return temp;
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

        public Task Execute(IJobExecutionContext context)
        {
            EmailHelper helper = new EmailHelper();
            var now = DateTime.Now;
            DayOfWeek day = now.DayOfWeek;
            List<Notification> degree_notifications_passport = GetNotificationsDegree("passport");
            List<Notification> degree_notifications_visa = GetNotificationsDegree("visa");
            List<Notification> degree_notifications_orientation = GetNotificationsDegree("orientation");
            List<Notification> mobility_notifications_passport = GetNotificationsMobility("passport");
            List<Notification> mobility_notifications_visa = GetNotificationsMobility("visa");
            List<Notification> mobility_notifications_detail_agenda = GetNotificationsMobility("detail_agenda");
            List<InsuranceFlightNotification> allStudentWithout = GetAllStudentWith("AllStudentWithout");
            List<InsuranceFlightNotification> allStudentWithInsurance = GetAllStudentWith("AllStudentWithInsurance");
            List<InsuranceFlightNotification> allStudentWithFlight = GetAllStudentWith("AllStudentWithFlight");
            List<NotificationConfig> notificationConfigs = GetNotificationConfigs();
            List<MeetingNotification> meetingNotifications = GetMeetingNotification();
            List<Notification> coordinatorDegreeList = GetCoordinatorDegree();
            List<Notification> coordinatorMobilityList = GetCoordinatorMobility();
            List<Notification> mobilityStudentList = GetMobilityStudent();
            List<Notification> degreeAccomodationList = GetDegreeStudentAccomodation();
            NotificationConfig accomodationConfig = GetConfigAccomodation();
            List<Notification> degreeNotificationList = new List<Notification>();
            List<Notification> mobilityNotificationList = new List<Notification>();
            try
            {
                #region Degree
                if (degree_notifications_passport.Count != 0)
                {
                    foreach (var item in degree_notifications_passport)
                    {
                        if (item.type.Equals(notificationConst.TYPE_PASSPORT) && !item.isUpdatePassport == true)
                        {
                            var expiredDate = item.passport_expired;
                            var daysBefore = item.days_before;
                            var totalDays = expiredDate.Subtract(now).Days;
                            if (totalDays <= daysBefore && totalDays >= 0)
                            {
                                string body = "Hello " + item.fullname + ",\n\nYour " + item.type + " " +
                                              "expire on " + item.passport_expired.ToString("yyyy-MMM-dd") + ", you have " +
                                              "" + totalDays.ToString() + " days left before it expire. Please renew!";
                                string student_notification = "Your Passport expire on " + item.passport_expired.ToString("yyyy-MMM-dd") + ", you have " + totalDays.ToString() + " days left before it expire." + " - (Notice on: " + day.ToString() + "-" + now.ToString("yyyy-MMM-dd") + ")";
                                helper.SendMail(item.email, notificationConst.SUBJECT_PASSPORT, body);
                                InsertNotificationInformation(item.user_id, notificationConst.SUBJECT_PASSPORT, student_notification);
                                Notification temp = new Notification();
                                temp.user_id = item.user_id;
                                temp.fullname = item.fullname;
                                temp.email = item.email;
                                temp.passport_expired = item.passport_expired;
                                temp.total_days = totalDays;
                                temp.type = item.type;
                                degreeNotificationList.Add(temp);
                            }
                        }
                    }
                }
                if (degree_notifications_visa.Count != 0)
                {
                    foreach (var item in degree_notifications_visa)
                    {
                        if (item.type.Equals(notificationConst.TYPE_VISA) && !item.isUpdateVisa == true)
                        {
                            var expiredDate = item.visa_expired;
                            var daysBefore = item.days_before;
                            var totalDays = expiredDate.Subtract(now).Days;
                            if (totalDays <= daysBefore && totalDays >= 0)
                            {
                                string body = "Hello " + item.fullname + ",\n\nYour " + item.type + " " +
                                              "expire on " + item.visa_expired.ToString("yyyy-MMM-dd") + ", you have " +
                                              "" + totalDays.ToString() + " days left before it expire. Please renew!";
                                string student_notification = "Your Visa expire on " + item.passport_expired.ToString("yyyy-MMM-dd") + ", you have " + totalDays.ToString() + " days left before it expire." + " - (Notice on: " + day.ToString() + "-" + now.ToString("yyyy-MMM-dd") + ")";
                                helper.SendMail(item.email, notificationConst.SUBJECT_VISA, body);
                                InsertNotificationInformation(item.user_id, notificationConst.SUBJECT_VISA, student_notification);
                                Notification temp = new Notification();
                                temp.user_id = item.user_id;
                                temp.fullname = item.fullname;
                                temp.email = item.email;
                                temp.visa_expired = item.passport_expired;
                                temp.total_days = totalDays;
                                temp.type = item.type;
                                degreeNotificationList.Add(temp);
                            }
                        }
                    }
                }
                if (degree_notifications_orientation.Count != 0)
                {
                    foreach (var item in degree_notifications_orientation)
                    {
                        if (item.type.Equals(notificationConst.TYPE_ORIENTATION))
                        {
                            var ort_date = item.ort_date;
                            var daysBefore = item.days_before;
                            var totalDays = ort_date.Subtract(now).Days;
                            if (totalDays <= daysBefore && totalDays >= 0)
                            {
                                string body = "Hello " + item.fullname + ",\n\nYour orientation " +
                                              "dealine on " + item.ort_date.ToString("yyyy-MMM-dd") + ", you have " +
                                              "" + totalDays.ToString() + " days left before dealine.";
                                string student_notification = "Your orientation on " + item.ort_date.ToString("yyyy-MMM-dd") + ", you have " + totalDays.ToString() + " days lefts." + " - (Notice on: " + day.ToString() + "-" + now.ToString("yyyy-MMM-dd") + ")";
                                helper.SendMail(item.email, notificationConst.SUBJECT_ORIENTATION, body);
                                InsertNotificationInformation(item.user_id, notificationConst.SUBJECT_ORIENTATION, student_notification);
                            }
                        }
                    }
                }
                //Insurance
                foreach (var item in allStudentWithout)
                {
                    foreach (var studentInsurance in allStudentWithInsurance)
                    {
                        if (item.student_id != studentInsurance.student_id || item.isUpdateInsurance != true)
                        {
                            foreach (var config in notificationConfigs)
                            {
                                if (config.type.Equals(notificationConst.TYPE_INSURANCE) && config.kind.Equals(notificationConst.KIND_DEGREE))
                                {
                                    var totalDays = config.deadline.Subtract(now).Days;
                                    if (totalDays <= config.days_before && totalDays >= 0)
                                    {
                                        string body = "Hello " + item.fullname + ",\n\nPlease submit your insurance document, " +
                                            "deadline is on " + config.deadline.ToString("yyyy-MMM-dd") + ". You have " +
                                            "" + totalDays + " days left before deadline.";
                                        string student_notification = "Please submit your insurance document, deadline is on " + config.deadline.ToString("yyyy-MMM-dd") + ". You have " + totalDays.ToString() + " days left before deadline." + " - (Notice on: " + day.ToString() + "-" + now.ToString("yyyy-MMM-dd") + ")";
                                        helper.SendMail(item.email, notificationConst.SUBJECT_INSURANCE, body);
                                        InsertNotificationInformation(item.student_id, notificationConst.SUBJECT_INSURANCE, student_notification);
                                    }
                                }
                            }
                        }
                    }
                }
                //Flight
                foreach (var item in allStudentWithout)
                {
                    foreach (var studentFlight in allStudentWithFlight)
                    {
                        if (item.student_id != studentFlight.student_id || item.isUpdateFlight != true)
                        {
                            foreach (var config in notificationConfigs)
                            {
                                if (config.type.Equals(notificationConst.TYPE_FLIGHT) && config.kind.Equals(notificationConst.KIND_DEGREE))
                                {
                                    var totalDays = config.deadline.Subtract(now).Days;
                                    if (totalDays <= config.days_before && totalDays >= 0)
                                    {
                                        string body = "Hello " + item.fullname + ",\n\nPlease submit your flight ticket document, " +
                                            "deadline is on " + config.deadline.ToString("yyyy-MMM-dd") + " . You have " +
                                            "" + totalDays + " days left before deadline.";
                                        string student_notification = "Please submit your flight ticket document, deadline is on " + config.deadline.ToString("yyyy-MMM-dd") + ". You have " + totalDays.ToString() + " days left before deadline." + " - (Notice on: " + day.ToString() + "-" + now.ToString("yyyy-MMM-dd") + ")";
                                        helper.SendMail(item.email, notificationConst.SUBJECT_FLIGHT, body);
                                        InsertNotificationInformation(item.student_id, notificationConst.SUBJECT_FLIGHT, student_notification);
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                #region Mobility
                if (mobility_notifications_passport.Count != 0)
                {
                    foreach (var item in mobility_notifications_passport)
                    {
                        if (item.type.Equals(notificationConst.TYPE_PASSPORT) && !item.isUpdatePassport == true)
                        {
                            var expiredDate = item.passport_expired;
                            var daysBefore = item.days_before;
                            var totalDays = expiredDate.Subtract(now).Days;
                            if (totalDays <= daysBefore && totalDays >= 0)
                            {
                                string body = "Hello " + item.fullname + ",\n\nYour " + item.type + " " +
                                              "expire on " + item.passport_expired.ToString("yyyy-MMM-dd") + ", you have " +
                                              "" + totalDays.ToString() + " days left before it expire. Please renew!";
                                string student_notification = "Your Passport expire on " + item.passport_expired.ToString("yyyy-MMM-dd") + ", you have " + totalDays.ToString() + " days left before it expire." + " - (Notice on: " + day.ToString() + "-" + now.ToString("yyyy-MMM-dd") + ")";
                                helper.SendMail(item.email, notificationConst.SUBJECT_PASSPORT, body);
                                InsertNotificationInformation(item.user_id, notificationConst.SUBJECT_PASSPORT, student_notification);
                                Notification temp = new Notification();
                                temp.user_id = item.user_id;
                                temp.fullname = item.fullname;
                                temp.email = item.email;
                                temp.passport_expired = item.passport_expired;
                                temp.total_days = totalDays;
                                temp.type = item.type;
                                mobilityNotificationList.Add(temp);
                            }
                        }
                    }
                }
                if (mobility_notifications_visa.Count != 0)
                {
                    foreach (var item in mobility_notifications_visa)
                    {
                        if (item.type.Equals(notificationConst.TYPE_VISA) && !item.isUpdateVisa == true)
                        {
                            var expiredDate = item.visa_expired;
                            var daysBefore = item.days_before;
                            var totalDays = expiredDate.Subtract(now).Days;
                            if (totalDays <= daysBefore && totalDays >= 0)
                            {
                                string body = "Hello " + item.fullname + ",\n\nYour " + item.type + " " +
                                              "expire on " + item.visa_expired.ToString("yyyy-MMM-dd") + ", you have " +
                                              "" + totalDays.ToString() + " days left before it expire. Please renew!";
                                string student_notification = "Your Visa expire on " + item.passport_expired.ToString("yyyy-MMM-dd") + ", you have " + totalDays.ToString() + " days left before it expire." + " - (Notice on: " + day.ToString() + "-" + now.ToString("yyyy-MMM-dd") + ")";
                                helper.SendMail(item.email, notificationConst.SUBJECT_VISA, body);
                                InsertNotificationInformation(item.user_id, notificationConst.SUBJECT_VISA, student_notification);
                                Notification temp = new Notification();
                                temp.user_id = item.user_id;
                                temp.fullname = item.fullname;
                                temp.email = item.email;
                                temp.visa_expired = item.passport_expired;
                                temp.total_days = totalDays;
                                temp.type = item.type;
                                mobilityNotificationList.Add(temp);
                            }
                        }
                    }
                }
                if (mobility_notifications_detail_agenda.Count != 0)
                {
                    foreach (var item in mobility_notifications_detail_agenda)
                    {
                        if (item.type.Equals(notificationConst.TYPE_DETAIL_AGENDA))
                        {
                            var date = item.detail_agenda_date;
                            var daysBefore = item.days_before;
                            var totalDays = date.Subtract(now).Days;
                            if (totalDays <= daysBefore && totalDays >= 0)
                            {
                                string body = "Hello " + item.fullname + ",\n\nThere are " +
                                              "an agenda on " + item.detail_agenda_date.ToString("yyyy-MMM-dd") + ", you have " +
                                              "" + totalDays.ToString() + " days left.";
                                string student_notification = "There are an agenda on " + item.detail_agenda_date.ToString("yyyy-MMM-dd") + ", you have " + totalDays.ToString() + " days left." + " - (Notice on: " + day.ToString() + "-" + now.ToString("yyyy-MMM-dd") + ")";
                                helper.SendMail(item.email, notificationConst.SUBJECT_DETAIL_AGENDA, body);
                                InsertNotificationInformation(item.user_id, notificationConst.SUBJECT_DETAIL_AGENDA, student_notification);
                            }
                        }
                    }
                }
                //Insurance
                foreach (var item in allStudentWithout)
                {
                    foreach (var studentInsurance in allStudentWithInsurance)
                    {
                        if (item.student_id != studentInsurance.student_id || !item.isUpdateInsurance == true)
                        {
                            foreach (var config in notificationConfigs)
                            {
                                if (config.type.Equals(notificationConst.TYPE_INSURANCE) && config.kind.Equals(notificationConst.KIND_MOBILITY))
                                {
                                    var totalDays = item.duration_start.Subtract(now).Days;
                                    if (totalDays <= config.days_before && totalDays >= 0)
                                    {
                                        string body = "Hello " + item.fullname + ",\n\nPlease submit your insurance document, " +
                                            "deadline is on " + item.duration_start.ToString("yyyy-MMM-dd") + " . You have " +
                                            "" + totalDays + " days left before deadline.";
                                        string student_notification = "Please submit your insurance document, deadline is on " + item.duration_start.ToString("yyyy-MMM-dd") + ". You have " + totalDays.ToString() + " days left before deadline." + " - (Notice on: " + day.ToString() + "-" + now.ToString("yyyy-MMM-dd") + ")";
                                        helper.SendMail(item.email, notificationConst.SUBJECT_INSURANCE, body);
                                        InsertNotificationInformation(item.student_id, notificationConst.SUBJECT_INSURANCE, student_notification);
                                    }
                                }
                            }
                        }
                    }
                }
                //Flight
                foreach (var item in allStudentWithout)
                {
                    foreach (var studentFlight in allStudentWithFlight)
                    {
                        if (item.student_id != studentFlight.student_id || !item.isUpdateFlight == true)
                        {
                            foreach (var config in notificationConfigs)
                            {
                                if (config.type.Equals(notificationConst.TYPE_FLIGHT) && config.kind.Equals(notificationConst.KIND_MOBILITY))
                                {
                                    var totalDays = item.duration_start.Subtract(now).Days;
                                    if (totalDays <= config.days_before && totalDays >= 0)
                                    {
                                        string body = "Hello " + item.fullname + ",\n\nPlease submit your flight ticket document, " +
                                            "deadline is on " + item.duration_start.ToString("yyyy-MMM-dd") + " . You have " +
                                            "" + totalDays + " days left before deadline.";
                                        string student_notification = "Please submit your flight ticket document, deadline is on " + item.duration_start.ToString("yyyy-MMM-dd") + ". You have " + totalDays.ToString() + " days left before deadline." + " - (Notice on: " + day.ToString() + "-" + now.ToString("yyyy-MMM-dd") + ")";
                                        helper.SendMail(item.email, notificationConst.SUBJECT_FLIGHT, body);
                                        InsertNotificationInformation(item.student_id, notificationConst.SUBJECT_FLIGHT, student_notification);
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                #region Meeting
                foreach (var item in notificationConfigs)
                {
                    if (item.type.Equals(notificationConst.TYPE_MEETING_SCHEDULE))
                    {
                        foreach (var meeting in meetingNotifications)
                        {
                            var totalDays = meeting.date.Subtract(now).Days;
                            if (totalDays <= item.days_before && totalDays >= 0)
                            {
                                string staff_body = "Hello " + meeting.staff_name.ToUpper() + ",\n\nYou have a meeting with" +
                                                    " " + meeting.student_name.ToUpper() + " - " + meeting.student_email + " " +
                                                    "on " + meeting.date.ToString("yyyy-MMM-dd");
                                string staff_notification = "You have a meeting with " + meeting.student_name.ToUpper() + " - " + meeting.student_email + " on " + meeting.date.ToString("yyyy-MMM-dd") + " - (Notice on: " + day.ToString() + "-" + now.ToString("yyyy-MMM-dd") + ")";
                                string student_body = "Hello " + meeting.student_name.ToUpper() + ",\n\nYou have a meeting with" +
                                                      " " + meeting.staff_name.ToUpper() + " - " + meeting.staff_email + " " +
                                                      "on " + meeting.date.ToString("yyyy-MMM-dd");
                                string student_notification = "You have a meeting with " + meeting.staff_name.ToUpper() + " - " + meeting.staff_email + " on " + meeting.date.ToString("yyyy-MMM-dd") + " - (Notice on: " + day.ToString() + "-" + now.ToString("yyyy-MMM-dd") + ")";
                                helper.SendMail(meeting.staff_email, notificationConst.SUBJECT_MEETING_SCHEDULE, staff_body);
                                InsertNotificationInformation(meeting.staff_id, notificationConst.SUBJECT_MEETING_SCHEDULE, staff_notification);
                                helper.SendMail(meeting.student_email, notificationConst.SUBJECT_MEETING_SCHEDULE, student_body);
                                InsertNotificationInformation(meeting.student_id, notificationConst.SUBJECT_MEETING_SCHEDULE, student_notification);
                            }
                        }
                    }
                }
                #endregion

                #region Send mail to Coordinators
                //Send mail to coordinator, who manage degree group
                if (degreeNotificationList.Count != 0)
                {
                    foreach (var item in coordinatorDegreeList)
                    {
                        string body = "Hello " + item.fullname + ",\n";
                        foreach (var student in degreeNotificationList)
                        {
                            if (student.type.Equals(notificationConst.TYPE_PASSPORT))
                            {
                                body += "\nPassport of " + student.fullname + " - " + student.email + " will expire on " + student.passport_expired.ToString("yyyy-MMM-dd") + ". " + student.total_days.ToString() + " days left before expire.";
                            }
                        }
                        foreach (var student in degreeNotificationList)
                        {
                            if (student.type.Equals(notificationConst.TYPE_VISA))
                            {
                                body += "\nVisa of " + student.fullname + " - " + student.email + " will expire on " + student.visa_expired.ToString("yyyy-MMM-dd") + ". " + student.total_days.ToString() + " days left before expire.";
                            }
                        }
                        helper.SendMail(item.email, notificationConst.SUBJECT_ISM_NOTIFICATION, body);
                    }
                }
                //Send mail to coordinator, who manage each mobility group
                if (mobilityNotificationList.Count != 0)
                {
                    foreach (var item in coordinatorMobilityList)
                    {
                        string body = "Hello " + item.fullname + ",\n";
                        foreach (var student in mobilityStudentList)
                        {
                            if (item.studentGroup_id == student.studentGroup_id)
                            {
                                foreach (var notification in mobilityNotificationList)
                                {
                                    if (student.user_id == notification.user_id)
                                    {
                                        if (notification.type.Equals(notificationConst.TYPE_PASSPORT))
                                        {
                                            body += "\nPassport of " + notification.fullname + " - " + notification.email + " will expire on " + notification.passport_expired.ToString("yyyy-MMM-dd") + ". " + notification.total_days.ToString() + " days left before expire.";
                                        }
                                    }
                                }
                            }
                        }
                        foreach (var student in mobilityStudentList)
                        {
                            if (item.studentGroup_id == student.studentGroup_id)
                            {
                                foreach (var notification in mobilityNotificationList)
                                {
                                    if (student.user_id == notification.user_id)
                                    {
                                        if (notification.type.Equals(notificationConst.TYPE_VISA))
                                        {
                                            body += "\nVisa of " + notification.fullname + " - " + notification.email + " will expire on " + notification.visa_expired.ToString("yyyy-MMM-dd") + ". " + notification.total_days.ToString() + " days left before expire.";
                                        }
                                    }
                                }
                            }
                        }
                        helper.SendMail(item.email, notificationConst.SUBJECT_ISM_NOTIFICATION, body);
                    }
                }
                #endregion

                #region Accomodation
                var DaysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
                var lastDay = new DateTime(now.Year, now.Month, DaysInMonth);
                var checkDays = lastDay.Subtract(now).Days;
                if (degreeAccomodationList.Count != 0)
                {
                    if (checkDays <= accomodationConfig.days_before && checkDays > 0)
                    {
                        foreach (var item in degreeAccomodationList)
                        {
                            if (!item.isPay == true)
                            {
                                string body = "Hello " + item.fullname + ",\n\nYou have to pay for accommodation fee for " + now.ToString("MMMM-yyyy") + ". Accommodation fee is " + item.fee.ToString() + ".";
                                string notification_web = "You have to pay for accommodation fee for " + now.ToString("MMMM-yyyy") + ". Accommodation fee is " + item.fee + ". - (Notice on: " + day.ToString() + "-" + now.ToString("yyyy-MMM-dd") + ")";
                                helper.SendMail(item.email, notificationConst.SUBJECT_ACCOMMODATION, body);
                                InsertNotificationInformation(item.user_id, notificationConst.SUBJECT_ACCOMMODATION, notification_web);
                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return Task.CompletedTask;
        }
    }
}