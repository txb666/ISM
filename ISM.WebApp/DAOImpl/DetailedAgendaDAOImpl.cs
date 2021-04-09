using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISM.WebApp.Utils;
using System.Data;

namespace ISM.WebApp.DAOImpl
{
    public class DetailedAgendaDAOImpl : DetailedAgendaDAO
    {
        public bool CreateDetailedAgenda(int student_group_id, DateTime date, TimeSpan time_start, TimeSpan time_end, string time_zone, string venue, string PIC, string content)
        {
            SqlConnection con = null;
            string sql = " insert into Detailed_Agenda(studentGroup_id,[date],time_start,time_end,timezone,venue,PIC,content)"
                       + " values(@student_group_id,@date,@time_start,@time_end,@time_zone,@venue,@PIC,@content)";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@student_group_id", SqlDbType.Int);
                com.Parameters["@student_group_id"].Value = student_group_id;
                com.Parameters.Add("@date", SqlDbType.Date);
                com.Parameters["@date"].Value = date;
                com.Parameters.Add("@time_start", SqlDbType.Time);
                com.Parameters["@time_start"].Value = time_start;
                com.Parameters.Add("@time_end", SqlDbType.Time);
                com.Parameters["@time_end"].Value = time_end;
                com.Parameters.Add("@time_zone", SqlDbType.NVarChar);
                com.Parameters["@time_zone"].Value = time_zone;
                com.Parameters.Add("@venue", SqlDbType.NVarChar);
                com.Parameters["@venue"].Value = venue;
                com.Parameters.Add("@PIC", SqlDbType.NVarChar);
                com.Parameters["@PIC"].Value = PIC;
                com.Parameters.Add("@content", SqlDbType.NVarChar);
                com.Parameters["@content"].Value = content;
                com.ExecuteNonQuery();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return false;
        }

        public bool DeleteDetailedAgenda(int detailed_agenda_id)
        {
            SqlConnection con = null;
            string sql = " delete from Detailed_Agenda  where detailed_agenda_id=@detail_agenda_id";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@detail_agenda_id", SqlDbType.Int);
                com.Parameters["@detail_agenda_id"].Value = detailed_agenda_id;              
                com.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return false;
        }

        public bool EditDetailedAgenda(int detailed_agenda_id, DateTime date, TimeSpan time_start, TimeSpan time_end, string time_zone, string venue, string PIC, string content)
        {
            SqlConnection con = null;
            string sql = " update Detailed_Agenda"
                       + " set [date]=@date,time_start=@time_start,time_end=@time_end,timezone=@time_zone,venue=@venue,PIC=@PIC,content=@content"
                       + " where detailed_agenda_id=@detail_agenda_id";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@detail_agenda_id", SqlDbType.Int);
                com.Parameters["@detail_agenda_id"].Value = detailed_agenda_id;
                com.Parameters.Add("@date", SqlDbType.Date);
                com.Parameters["@date"].Value = date;
                com.Parameters.Add("@time_start", SqlDbType.Time);
                com.Parameters["@time_start"].Value = time_start;
                com.Parameters.Add("@time_end", SqlDbType.Time);
                com.Parameters["@time_end"].Value = time_end;
                com.Parameters.Add("@time_zone", SqlDbType.NVarChar);
                com.Parameters["@time_zone"].Value = time_zone;
                com.Parameters.Add("@venue", SqlDbType.NVarChar);
                com.Parameters["@venue"].Value = venue;
                com.Parameters.Add("@PIC", SqlDbType.NVarChar);
                com.Parameters["@PIC"].Value = PIC;
                com.Parameters.Add("@content", SqlDbType.NVarChar);
                com.Parameters["@content"].Value = content;
                com.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return false;
        }

        public List<DetailedAgenda> GetDetailedAgenda(int student_group_id, DateTime? date, string time_zone, string venue, string PIC, string content, int page, int pageSize)
        {
            int from = page * pageSize - (pageSize - 1);
            int to = page * pageSize;
            SqlConnection con = null;
            string sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<DetailedAgenda> detailedAgendas = new List<DetailedAgenda>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = " ";
                if (date != null)
                {
                    where += " and [date]=@date";
                    com.Parameters.Add("@date", SqlDbType.Date);
                    com.Parameters["@date"].Value = date;
                }
                if (!string.IsNullOrEmpty(time_zone))
                {
                    where += " and upper(timezone) like upper('%' + @time_zone + '%')";
                    com.Parameters.Add("@time_zone", SqlDbType.NVarChar);
                    com.Parameters["@time_zone"].Value = time_zone;
                }
                if (!string.IsNullOrEmpty(venue))
                {
                    where += " and upper(venue) like upper('%' + @venue + '%')";
                    com.Parameters.Add("@venue", SqlDbType.NVarChar);
                    com.Parameters["@venue"].Value = venue;
                }
                if (!string.IsNullOrEmpty(PIC))
                {
                    where += " and upper(PIC) like upper('%' + @PIC + '%')";
                    com.Parameters.Add("@PIC", SqlDbType.NVarChar);
                    com.Parameters["@PIC"].Value = PIC;
                }
                if (!string.IsNullOrEmpty(content))
                {
                    where += " and upper(content) like upper('%' + @content + '%')";
                    com.Parameters.Add("@content", SqlDbType.NVarChar);
                    com.Parameters["@content"].Value = content;
                }
                com.Parameters.Add("@from", SqlDbType.Int);
                com.Parameters["@from"].Value = from;
                com.Parameters.Add("@to", SqlDbType.Int);
                com.Parameters["@to"].Value = to;
                com.Parameters.Add("@student_group_id", SqlDbType.Int);
                com.Parameters["@student_group_id"].Value = student_group_id;
                sql = " select * from("
                    + " select ROW_NUMBER() over (order by detailed_agenda_id asc) rownumber, detailed_agenda_id,studentGroup_id,[date],time_start,time_end,timezone,venue,PIC,content"
                    + " from Detailed_Agenda where [date]>=CAST(getdate() as date) and studentGroup_id=@student_group_id" + where
                    + " ) as temp where temp.rownumber>=@from and temp.rownumber<=@to";
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    DetailedAgenda detailedAgenda = new DetailedAgenda();
                    detailedAgenda.detailed_agenda_id = (int)reader.GetValue(reader.GetOrdinal("detailed_agenda_id"));
                    detailedAgenda.student_group_id = (int)reader.GetValue(reader.GetOrdinal("studentGroup_id"));
                    detailedAgenda.date = (DateTime)reader.GetValue(reader.GetOrdinal("date"));
                    detailedAgenda.time_start = (TimeSpan)reader.GetValue(reader.GetOrdinal("time_start"));
                    detailedAgenda.time_end = (TimeSpan)reader.GetValue(reader.GetOrdinal("time_end"));
                    detailedAgenda.time_zone = (string)reader.GetValue(reader.GetOrdinal("timezone"));
                    detailedAgenda.venue = (string)reader.GetValue(reader.GetOrdinal("venue"));
                    detailedAgenda.PIC = (string)reader.GetValue(reader.GetOrdinal("PIC"));
                    detailedAgenda.content = (string)reader.GetValue(reader.GetOrdinal("content"));
                    detailedAgendas.Add(detailedAgenda);
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
            return detailedAgendas;
        }


        public int GetTotalDetailedAgenda(int student_group_id, DateTime? date, string time_zone, string venue, string PIC, string content)
        {
            SqlConnection con = null;
            string sql = "";
            SqlCommand com = null;
            int total = 0;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = " ";
                if (date != null)
                {
                    where += " and [date]=@date";
                    com.Parameters.Add("@date", SqlDbType.Date);
                    com.Parameters["@date"].Value = date;
                }
                if (!string.IsNullOrEmpty(time_zone))
                {
                    where += " and upper(timezone) like upper('%' + @time_zone + '%')";
                    com.Parameters.Add("@time_zone", SqlDbType.NVarChar);
                    com.Parameters["@time_zone"].Value = time_zone;
                }
                if (!string.IsNullOrEmpty(venue))
                {
                    where += " and upper(venue) like upper('%' + @venue + '%')";
                    com.Parameters.Add("@venue", SqlDbType.NVarChar);
                    com.Parameters["@venue"].Value = venue;
                }
                if (!string.IsNullOrEmpty(PIC))
                {
                    where += " and upper(PIC) like upper('%' + @PIC + '%')";
                    com.Parameters.Add("@PIC", SqlDbType.NVarChar);
                    com.Parameters["@PIC"].Value = PIC;
                }
                if (!string.IsNullOrEmpty(content))
                {
                    where += " and upper(content) like upper('%' + @content + '%')";
                    com.Parameters.Add("@content", SqlDbType.NVarChar);
                    com.Parameters["@content"].Value = content;
                }
                com.Parameters.Add("@student_group_id", SqlDbType.Int);
                com.Parameters["@student_group_id"].Value = student_group_id;
                sql = " select count(*)"
                    + " from Detailed_Agenda where [date]>=CAST(getdate() as date) and studentGroup_id=@student_group_id" + where;
                com.CommandText = sql;
                total = (int)com.ExecuteScalar();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return total;
        }

        public bool isDetailedAgendaExist(DateTime date, TimeSpan time_start, TimeSpan time_end, string time_zone)
        {
            SqlConnection con = null;
            string sql = " select count(*) from Detailed_Agenda"
                       + " where [date]=@date and (@time_start>=time_start and @time_start<=time_end) or (@time_end>=time_start and @time_end<=time_end)";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                
            }
            catch(Exception e)
            {
                Console.Write(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return false;
        }

        public bool SetupNotification(int days_before)
        {
            SqlConnection con = null;
            string sql = "begin tran if exists (select * from Config with (updlock,serializable) " +
                         "where [type] = 'Detail_Agenda') begin update Config set days_before = @days_before where " +
                         "[type] = 'Detail_Agenda' end else begin insert into Config([type],days_before) " +
                         "values ('Detail_Agenda',@days_before) end commit tran";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@days_before", SqlDbType.Int);
                com.Parameters["@days_before"].Value = days_before;
                com.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return false;
        }
    }
}
