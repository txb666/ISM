using ISM.WebApp.Models;
using ISM.WebApp.Utils;
using Microsoft.Data.SqlClient;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Jobs
{
    public class CheckNotificationJob : IJob
    {
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

        private List<StudentGroup> GetStudentGroupsWith(string type)
        {
            SqlConnection con = null;
            String sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<StudentGroup> studentGroups = new List<StudentGroup>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                if(type.Equals("passport"))
                {
                    sql = "select distinct c.[user_id],a.student_group_id,b.[program_name],b.[type] from Student_Group a, Programs b, Users c, Passports d where a.program_id = b.program_id and a.student_group_id = c.studentGroup_id and c.[user_id] = d.student_id";
                }
                if (type.Equals("visa"))
                {
                    sql = "select distinct c.[user_id],a.student_group_id,b.[program_name],b.[type] from Student_Group a, Programs b, Users c, Visa d where a.program_id = b.program_id and a.student_group_id = c.studentGroup_id and c.[user_id] = d.student_id";
                }
                if (type.Equals("ort_schedule"))
                {
                    sql = "select distinct c.[user_id],a.student_group_id,b.[program_name],b.[type] from Student_Group a, Programs b, Users c, ORT_Schedule d where a.program_id = b.program_id and a.student_group_id = c.studentGroup_id and c.[user_id] = d.student_id";
                }
                if (type.Equals("Detail_Agenda"))
                {
                    sql = "select c.[user_id],a.student_group_id,b.[program_name],b.[type] from Student_Group a, Programs b, Detailed_Agenda c where a.program_id = b.program_id and a.student_group_id = c.studentGroup_id";
                }
                if (type.Equals("Transportation"))
                {
                    sql = "select c.[user_id],a.student_group_id,b.[program_name],b.[type] from Student_Group a, Programs b, Transportations c where a.program_id = b.program_id and a.student_group_id = c.studentGroup_id";
                }
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    StudentGroup studentGroup = new StudentGroup();
                    studentGroup.studentGroup_id = (int)reader.GetValue(reader.GetOrdinal("student_group_id"));
                    studentGroup.student_id = (int)reader.GetValue(reader.GetOrdinal("user_id"));
                    studentGroup.program_name = (string)reader.GetValue(reader.GetOrdinal("program_name"));
                    studentGroup.type = (string)reader.GetValue(reader.GetOrdinal("type"));
                    studentGroups.Add(studentGroup);
                }
                return studentGroups;
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

        private List<StudentGroup> GetStudentGroups()
        {
            SqlConnection con = null;
            String sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<StudentGroup> studentGroups = new List<StudentGroup>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                sql = "select a.student_group_id,b.[program_name],b.[type] from Student_Group a, Programs b where a.program_id = b.program_id";
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    StudentGroup studentGroup = new StudentGroup();
                    studentGroup.studentGroup_id = (int)reader.GetValue(reader.GetOrdinal("student_group_id"));
                    studentGroup.program_name = (string)reader.GetValue(reader.GetOrdinal("program_name"));
                    studentGroup.type = (string)reader.GetValue(reader.GetOrdinal("type"));
                    studentGroups.Add(studentGroup);
                }
                return studentGroups;
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

        private void InsertDay(string notificationType, int studentGroup_id, int days_before)
        {
            SqlConnection con = null;
            String sql = "begin tran if exists (select * from Notification_Configuration with (updlock,serializable) where [type] = @notificationType and studentGroup_id = @studentGroup_id) begin update Notification_Configuration set days_before = @days_before where [type] = @notificationType and studentGroup_id = @studentGroup_id end else begin insert into Notification_Configuration(studentGroup_id,[type],days_before) values (@studentGroup_id,@notificationType,@days_before) end commit tran";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@notificationType", SqlDbType.NVarChar);
                com.Parameters["@notificationType"].Value = notificationType;
                com.Parameters.Add("@studentGroup_id", SqlDbType.Int);
                com.Parameters["@studentGroup_id"].Value = studentGroup_id;
                com.Parameters.Add("@days_before", SqlDbType.Int);
                com.Parameters["@days_before"].Value = days_before;
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

        private void InsertDayWithDeadline(string notificationType, int studentGroup_id, int days_before, DateTime deadline)
        {
            SqlConnection con = null;
            String sql = "begin tran if exists (select * from Notification_Configuration with (updlock,serializable) where [type] = @notificationType and studentGroup_id = @studentGroup_id) begin update Notification_Configuration set days_before = @days_before,deadline = @deadline where [type] = @notificationType and studentGroup_id = @studentGroup_id end else begin insert into Notification_Configuration(studentGroup_id,[type],days_before,deadline) values (@studentGroup_id,@notificationType,@days_before,@deadline) end commit tran";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@notificationType", SqlDbType.NVarChar);
                com.Parameters["@notificationType"].Value = notificationType;
                com.Parameters.Add("@studentGroup_id", SqlDbType.Int);
                com.Parameters["@studentGroup_id"].Value = studentGroup_id;
                com.Parameters.Add("@days_before", SqlDbType.Int);
                com.Parameters["@days_before"].Value = days_before;
                com.Parameters.Add("@deadline", SqlDbType.DateTime);
                com.Parameters["@deadline"].Value = deadline;
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

        private void InsertHour(string notificationType, int studentGroup_id, int hours_before)
        {
            SqlConnection con = null;
            String sql = "begin tran if exists (select * from Notification_Configuration with (updlock,serializable) where [type] = @notificationType and studentGroup_id = @studentGroup_id) begin update Notification_Configuration set hours_before = @hours_before where [type] = @notificationType and studentGroup_id = @studentGroup_id end else begin insert into Notification_Configuration(studentGroup_id,[type],hours_before) values (@studentGroup_id,@notificationType,@hours_before) end commit tran";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@notificationType", SqlDbType.NVarChar);
                com.Parameters["@notificationType"].Value = notificationType;
                com.Parameters.Add("@studentGroup_id", SqlDbType.Int);
                com.Parameters["@studentGroup_id"].Value = studentGroup_id;
                com.Parameters.Add("@hours_before", SqlDbType.Int);
                com.Parameters["@hours_before"].Value = hours_before;
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

        private void Config()
        {
            var configList = GetNotificationConfigs();
            try
            {
                foreach (var item in configList)
                {
                    if (item.type.Equals("passport"))
                    {
                        var studentGroupListWith = GetStudentGroupsWith(item.type);
                        foreach (var studentGroup in studentGroupListWith)
                        {
                            InsertDay(item.type, studentGroup.studentGroup_id, item.days_before);
                        }
                    }
                    if (item.type.Equals("visa"))
                    {
                        var studentGroupListWith = GetStudentGroupsWith(item.type);
                        foreach (var studentGroup in studentGroupListWith)
                        {
                            InsertDay(item.type, studentGroup.studentGroup_id, item.days_before);
                        }
                    }
                    if (item.type.Equals("ort_schedule"))
                    {
                        var studentGroupListWith = GetStudentGroupsWith(item.type);
                        foreach (var studentGroup in studentGroupListWith)
                        {
                            InsertDay(item.type, studentGroup.studentGroup_id, item.days_before);
                        }
                    }
                    if (item.type.Equals("Detail_Agenda"))
                    {
                        var studentGroupListWith = GetStudentGroupsWith(item.type);
                        foreach (var studentGroup in studentGroupListWith)
                        {
                            InsertDay(item.type, studentGroup.studentGroup_id, item.days_before);
                        }
                    }
                    if (item.type.Equals("Transportation"))
                    {
                        var studentGroupListWith = GetStudentGroupsWith(item.type);
                        foreach (var studentGroup in studentGroupListWith)
                        {
                            InsertHour(item.type, studentGroup.studentGroup_id, item.hours_before);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public Task Execute(IJobExecutionContext context)
        {
            Config();
            return Task.CompletedTask;
        }
    }
}
