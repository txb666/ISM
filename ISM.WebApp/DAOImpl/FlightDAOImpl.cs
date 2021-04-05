using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISM.WebApp.Utils;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ISM.WebApp.DAOImpl
{
    public class FlightDAOImpl : FlightDAO
    {
        public bool editFlightDegree(int? flight_id, string flight_number_a, DateTime? arrival_date_a, TimeSpan? arrival_time_a, string airport_departure_a, string airport_arrival_a, string picture_a)
        {
            SqlConnection con = null;
            string sql = "update Flights set [flight_number_a]=@flight_number_a,[arrival_date_a]=@arrival_date_a,[arrival_time_a]=@arrival_time_a,[airport_departure_a]=@airport_departure_a,[airport_arrival_a]=@airport_arrival_a,[picture_a]=@picture_a where flight_id=@flight_id";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@flight_number_a", SqlDbType.NVarChar);
                com.Parameters["@flight_number_a"].Value = flight_number_a;

                com.Parameters.Add("@airport_departure_a", SqlDbType.NVarChar);
                com.Parameters["@airport_departure_a"].Value = airport_departure_a;

                com.Parameters.Add("@airport_arrival_a", SqlDbType.NVarChar);
                com.Parameters["@airport_arrival_a"].Value = airport_arrival_a;

                com.Parameters.Add("@arrival_date_a", SqlDbType.Date);
                com.Parameters["@arrival_date_a"].Value = arrival_date_a;

                com.Parameters.Add("@arrival_time_a", SqlDbType.Time);
                com.Parameters["@arrival_time_a"].Value = arrival_time_a;

                com.Parameters.Add("@flight_id", SqlDbType.Int);
                com.Parameters["@flight_id"].Value = flight_id;

                com.Parameters.Add("@picture_a", SqlDbType.NVarChar);
                com.Parameters["@picture_a"].Value = picture_a;

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

        public bool editFlightMobility(int? flight_id, string flight_number_a, DateTime? arrival_date_a, TimeSpan? arrival_time_a, string airport_departure_a, string airport_arrival_a, string picture_a, string flight_number_d, DateTime? arrival_date_d, TimeSpan? arrival_time_d, string airport_departure_d, string airport_arrival_d, string picture_d)
        {
            SqlConnection con = null;
            string sql = "update Flights set [flight_number_a]=@flight_number_a,[arrival_date_a]=@arrival_date_a,[arrival_time_a]=@arrival_time_a,[airport_departure_a]=@airport_departure_a,[airport_arrival_a]=@airport_arrival_a,[picture_a]=@picture_a,[flight_number_d]=@flight_number_d,[arrival_date_d]=@arrival_date_d,[arrival_time_d]=@arrival_time_d,[airport_departure_d]=@airport_departure_d,[airport_arrival_d]=@airport_arrival_d,[picture_d]=@picture_d where flight_id=@flight_id";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@flight_number_a", SqlDbType.NVarChar);
                com.Parameters["@flight_number_a"].Value = flight_number_a;

                com.Parameters.Add("@airport_departure_a", SqlDbType.NVarChar);
                com.Parameters["@airport_departure_a"].Value = airport_departure_a;

                com.Parameters.Add("@airport_departure_d", SqlDbType.NVarChar);
                com.Parameters["@airport_departure_d"].Value = airport_departure_d;

                com.Parameters.Add("@airport_arrival_a", SqlDbType.NVarChar);
                com.Parameters["@airport_arrival_a"].Value = airport_arrival_a;

                com.Parameters.Add("@airport_arrival_d", SqlDbType.NVarChar);
                com.Parameters["@airport_arrival_d"].Value = airport_arrival_d;

                com.Parameters.Add("@flight_number_d", SqlDbType.NVarChar);
                com.Parameters["@flight_number_d"].Value = flight_number_d;

                com.Parameters.Add("@arrival_date_a", SqlDbType.Date);
                com.Parameters["@arrival_date_a"].Value = arrival_date_a;

                com.Parameters.Add("@arrival_date_d", SqlDbType.Date);
                com.Parameters["@arrival_date_d"].Value = arrival_date_d;

                com.Parameters.Add("@arrival_time_a", SqlDbType.Time);
                com.Parameters["@arrival_time_a"].Value = arrival_time_a;

                com.Parameters.Add("@arrival_time_d", SqlDbType.Time);
                com.Parameters["@arrival_time_d"].Value = arrival_time_d;

                com.Parameters.Add("@flight_id", SqlDbType.Int);
                com.Parameters["@flight_id"].Value = flight_id;

                com.Parameters.Add("@picture_d", SqlDbType.NVarChar);
                com.Parameters["@picture_d"].Value = picture_d;

                com.Parameters.Add("@picture_a", SqlDbType.NVarChar);
                com.Parameters["@picture_a"].Value = picture_a;

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

        public List<Flight> getFlight(bool isAdmin, string degreeOrMobility, bool haveDegree, int current_staff_id, int page, int pageSize, string account, string fullname, string flight_number_a, DateTime? arrival_date_a, TimeSpan? arrival_time_a, string airport_departure_a, string airport_arrival_a, string flight_number_d, DateTime? arrival_date_d, TimeSpan? arrival_time_d, string airport_departure_d, string airport_arrival_d)
        {
            int from = page * pageSize - (pageSize - 1);
            int to = page * pageSize;
            SqlConnection con = null;
            string sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<Flight> flights = new List<Flight>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = "";
                if (!string.IsNullOrEmpty(account))
                {
                    where += " and upper(b.account) like upper('%' + @account + '%')";
                    com.Parameters.Add("@account", SqlDbType.NVarChar);
                    com.Parameters["@account"].Value = account;
                }
                if (!string.IsNullOrEmpty(fullname))
                {
                    where += " and upper(b.fullname) like upper('%' + @fullname + '%')";
                    com.Parameters.Add("@fullname", SqlDbType.NVarChar);
                    com.Parameters["@fullname"].Value = fullname;
                }
                if (!string.IsNullOrEmpty(flight_number_a))
                {
                    where += " and upper(flight_number_a) like upper('%' + @flight_number_a + '%')";
                    com.Parameters.Add("@flight_number_a", SqlDbType.NVarChar);
                    com.Parameters["@flight_number_a"].Value = flight_number_a;
                }
                if (!string.IsNullOrEmpty(flight_number_d))
                {
                    where += " and upper(flight_number_d) like upper('%' + @flight_number_d + '%')";
                    com.Parameters.Add("@flight_number_d", SqlDbType.NVarChar);
                    com.Parameters["@flight_number_d"].Value = flight_number_d;
                }
                if (!string.IsNullOrEmpty(airport_departure_a))
                {
                    where += " and upper(airport_departure_a) like upper('%' + @airport_departure_a + '%')";
                    com.Parameters.Add("@airport_departure_a", SqlDbType.NVarChar);
                    com.Parameters["@airport_departure_a"].Value = airport_departure_a;
                }
                if (!string.IsNullOrEmpty(airport_departure_d))
                {
                    where += " and upper(airport_departure_d) like upper('%' + @airport_departure_d + '%')";
                    com.Parameters.Add("@airport_departure_d", SqlDbType.NVarChar);
                    com.Parameters["@airport_departure_d"].Value = airport_departure_d;
                }
                if (!string.IsNullOrEmpty(airport_arrival_a))
                {
                    where += " and upper(airport_arrival_a) like upper('%' + @airport_arrival_a + '%')";
                    com.Parameters.Add("@airport_arrival_a", SqlDbType.NVarChar);
                    com.Parameters["@airport_arrival_a"].Value = airport_arrival_a;
                }
                if (!string.IsNullOrEmpty(airport_arrival_d))
                {
                    where += " and upper(airport_arrival_d) like upper('%' + @airport_arrival_d + '%')";
                    com.Parameters.Add("@airport_arrival_d", SqlDbType.NVarChar);
                    com.Parameters["@airport_arrival_d"].Value = airport_arrival_d;
                }
                if (arrival_date_a != null)
                {
                    where += " and arrival_date_a=@arrival_date_a";
                    com.Parameters.Add("@arrival_date_a", SqlDbType.Date);
                    com.Parameters["@arrival_date_a"].Value = arrival_date_a;
                }
                if (arrival_date_d != null)
                {
                    where += " and arrival_date_d=@arrival_date_d";
                    com.Parameters.Add("@arrival_date_d", SqlDbType.Date);
                    com.Parameters["@arrival_date_d"].Value = arrival_date_d;
                }
                if (arrival_time_a != null)
                {
                    where += " and arrival_time_a=@arrival_time_a";
                    com.Parameters.Add("@arrival_time_a", SqlDbType.Time);
                    com.Parameters["@arrival_time_a"].Value = arrival_time_a;
                }
                if (arrival_time_d != null)
                {
                    where += " and arrival_time_d=@arrival_time_d";
                    com.Parameters.Add("@arrival_time_d", SqlDbType.Time);
                    com.Parameters["@arrival_time_d"].Value = arrival_time_d;
                }
                com.Parameters.Add("@from", SqlDbType.Int);
                com.Parameters["@from"].Value = from;
                com.Parameters.Add("@to", SqlDbType.Int);
                com.Parameters["@to"].Value = to;
                if (degreeOrMobility.Equals("Mobility"))
                {
                    if (isAdmin)
                    {
                        sql = " select * from"
                            + " (select ROW_NUMBER() over (order by flight_id asc) rownumber, flight_id,student_id,b.fullname,b.account,flight_number_a,arrival_date_a,arrival_time_a,airport_departure_a,airport_arrival_a,picture_a,flight_number_d,arrival_date_d,arrival_time_d,airport_departure_d,airport_arrival_d,picture_d"
                            + " from Flights, Users b, Student_Group c, Programs d"
                            + " where student_id=b.[user_id] and b.studentGroup_id=c.student_group_id and c.program_id=d.program_id and d.[type]='Mobility'" + where + ")"
                            + " as temp where temp.rownumber>=@from and temp.rownumber<=@to";
                    }
                    else
                    {
                        sql = " select * from"
                            + " (select ROW_NUMBER() over (order by flight_id asc) rownumber, flight_id,student_id,b.fullname,b.account,flight_number_a,arrival_date_a,arrival_time_a,airport_departure_a,airport_arrival_a,picture_a,flight_number_d,arrival_date_d,arrival_time_d,airport_departure_d,airport_arrival_d,picture_d"
                            + " from Flights, Users b, Student_Group c, Programs d, Coordinators e"
                            + " where a.student_id=b.[user_id] and b.studentGroup_id=c.student_group_id and c.program_id=d.program_id and c.student_group_id=e.studentGroup_id and d.[type]='Mobility' and e.staff_id=@current_staff_id" + where + ")"
                            + " as temp where temp.rownumber>=@from and temp.rownumber<=@to";
                        com.Parameters.Add("@current_staff_id", SqlDbType.Int);
                        com.Parameters["@current_staff_id"].Value = current_staff_id;
                    }
                }
                else if (degreeOrMobility.Equals("Degree"))
                {
                    if (haveDegree)
                    {
                        sql = " select * from"
                            + " (select ROW_NUMBER() over (order by flight_id asc) rownumber, flight_id,student_id,b.fullname,b.account,flight_number_a,arrival_date_a,arrival_time_a,airport_departure_a,airport_arrival_a,picture_a,flight_number_d,arrival_date_d,arrival_time_d,airport_departure_d,airport_arrival_d,picture_d"
                            + " from Flights, Users b, Student_Group c, Programs d"
                            + " where student_id=b.[user_id] and b.studentGroup_id=c.student_group_id and c.program_id=d.program_id and d.[type]='Degree'" + where + ")"
                            + " as temp where temp.rownumber>=@from and temp.rownumber<=@to";
                    }
                    else
                    {
                        return flights;
                    }
                }
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Flight flight = new Flight();
                    flight.flight_id = (int)reader.GetValue(reader.GetOrdinal("flight_id"));
                    flight.student_id = (int)reader.GetValue(reader.GetOrdinal("student_id"));
                    flight.account = (string)reader.GetValue(reader.GetOrdinal("account"));
                    if (!reader.IsDBNull(reader.GetOrdinal("fullname")))
                    {
                        flight.fullname = (string)reader.GetValue(reader.GetOrdinal("fullname"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("flight_number_a")))
                    {
                        flight.flight_number_a = (string)reader.GetValue(reader.GetOrdinal("flight_number_a"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("flight_number_d")))
                    {
                        flight.flight_number_d = (string)reader.GetValue(reader.GetOrdinal("flight_number_d"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("airport_departure_a")))
                    {
                        flight.airport_departure_a = (string)reader.GetValue(reader.GetOrdinal("airport_departure_a"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("airport_arrival_a")))
                    {
                        flight.airport_arrival_a = (string)reader.GetValue(reader.GetOrdinal("airport_arrival_a"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("airport_arrival_d")))
                    {
                        flight.airport_arrival_d = (string)reader.GetValue(reader.GetOrdinal("airport_arrival_d"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("airport_departure_d")))
                    {
                        flight.airport_departure_d = (string)reader.GetValue(reader.GetOrdinal("airport_departure_d"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("picture_a")))
                    {
                        flight.picture_a = (string)reader.GetValue(reader.GetOrdinal("picture_a"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("picture_d")))
                    {
                        flight.picture_d = (string)reader.GetValue(reader.GetOrdinal("picture_d"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("arrival_date_a")))
                    {
                        flight.arrival_date_a = (DateTime)reader.GetValue(reader.GetOrdinal("arrival_date_a"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("arrival_time_a")))
                    {
                        flight.arrival_time_a = (TimeSpan)reader.GetValue(reader.GetOrdinal("arrival_time_a"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("arrival_time_d")))
                    {
                        flight.arrival_time_d = (TimeSpan)reader.GetValue(reader.GetOrdinal("arrival_time_d"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("arrival_date_d")))
                    {
                        flight.arrival_date_d = (DateTime)reader.GetValue(reader.GetOrdinal("arrival_date_d"));
                    }
                    flights.Add(flight);
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
            return flights;
        }

        public int getTotalFlight(bool isAdmin, string degreeOrMobility, bool haveDegree, int current_staff_id, string account, string fullname, string flight_number_a, DateTime? arrival_date_a, TimeSpan? arrival_time_a, string airport_departure_a, string airport_arrival_a, string flight_number_d, DateTime? arrival_date_d, TimeSpan? arrival_time_d, string airport_departure_d, string airport_arrival_d)
        {
            SqlConnection con = null;
            string sql = "";
            SqlCommand com = null;
            int totalFlight = 0;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = "";
                if (!string.IsNullOrEmpty(account))
                {
                    where += " and upper(b.account) like upper('%' + @account + '%')";
                    com.Parameters.Add("@account", SqlDbType.NVarChar);
                    com.Parameters["@account"].Value = account;
                }
                if (!string.IsNullOrEmpty(fullname))
                {
                    where += " and upper(b.fullname) like upper('%' + @fullname + '%')";
                    com.Parameters.Add("@fullname", SqlDbType.NVarChar);
                    com.Parameters["@fullname"].Value = fullname;
                }
                if (!string.IsNullOrEmpty(flight_number_a))
                {
                    where += " and upper(flight_number_a) like upper('%' + @flight_number_a + '%')";
                    com.Parameters.Add("@flight_number_a", SqlDbType.NVarChar);
                    com.Parameters["@flight_number_a"].Value = flight_number_a;
                }
                if (!string.IsNullOrEmpty(flight_number_d))
                {
                    where += " and upper(flight_number_d) like upper('%' + @flight_number_d + '%')";
                    com.Parameters.Add("@flight_number_d", SqlDbType.NVarChar);
                    com.Parameters["@flight_number_d"].Value = flight_number_d;
                }
                if (!string.IsNullOrEmpty(airport_departure_a))
                {
                    where += " and upper(airport_departure_a) like upper('%' + @airport_departure_a + '%')";
                    com.Parameters.Add("@airport_departure_a", SqlDbType.NVarChar);
                    com.Parameters["@airport_departure_a"].Value = airport_departure_a;
                }
                if (!string.IsNullOrEmpty(airport_departure_d))
                {
                    where += " and upper(airport_departure_d) like upper('%' + @airport_departure_d + '%')";
                    com.Parameters.Add("@airport_departure_d", SqlDbType.NVarChar);
                    com.Parameters["@airport_departure_d"].Value = airport_departure_d;
                }
                if (!string.IsNullOrEmpty(airport_arrival_a))
                {
                    where += " and upper(airport_arrival_a) like upper('%' + @airport_arrival_a + '%')";
                    com.Parameters.Add("@airport_arrival_a", SqlDbType.NVarChar);
                    com.Parameters["@airport_arrival_a"].Value = airport_arrival_a;
                }
                if (!string.IsNullOrEmpty(airport_arrival_d))
                {
                    where += " and upper(airport_arrival_d) like upper('%' + @airport_arrival_d + '%')";
                    com.Parameters.Add("@airport_arrival_d", SqlDbType.NVarChar);
                    com.Parameters["@airport_arrival_d"].Value = airport_arrival_d;
                }
                if (arrival_date_a != null)
                {
                    where += " and arrival_date_a=@arrival_date_a";
                    com.Parameters.Add("@arrival_date_a", SqlDbType.Date);
                    com.Parameters["@arrival_date_a"].Value = arrival_date_a;
                }
                if (arrival_date_d != null)
                {
                    where += " and arrival_date_d=@arrival_date_d";
                    com.Parameters.Add("@arrival_date_d", SqlDbType.Date);
                    com.Parameters["@arrival_date_d"].Value = arrival_date_d;
                }
                if (arrival_time_a != null)
                {
                    where += " and arrival_time_a=@arrival_time_a";
                    com.Parameters.Add("@arrival_time_a", SqlDbType.Time);
                    com.Parameters["@arrival_time_a"].Value = arrival_time_a;
                }
                if (arrival_time_d != null)
                {
                    where += " and arrival_time_d=@arrival_time_d";
                    com.Parameters.Add("@arrival_time_d", SqlDbType.Time);
                    com.Parameters["@arrival_time_d"].Value = arrival_time_d;
                }
                if (degreeOrMobility.Equals("Mobility"))
                {
                    if (isAdmin)
                    {
                        sql = " select count(*)"
                            + " from Flights, Users b, Student_Group c, Programs d"
                            + " where student_id=b.[user_id] and b.studentGroup_id=c.student_group_id and c.program_id=d.program_id and d.[type]='Mobility'" + where;
                    }
                    else
                    {
                        sql = " select count(*)"
                            + " from Flights, Users b, Student_Group c, Programs d, Coordinators e"
                            + " where student_id=b.[user_id] and b.studentGroup_id=c.student_group_id and c.program_id=d.program_id and c.student_group_id=e.studentGroup_id and d.[type]='Mobility' and e.staff_id=@current_staff_id" + where;
                        com.Parameters.Add("@current_staff_id", SqlDbType.Int);
                        com.Parameters["@current_staff_id"].Value = current_staff_id;

                    }
                }
                else if (degreeOrMobility.Equals("Degree"))
                {
                    if (haveDegree)
                    {
                        sql = " select count(*)"
                            + " from Flights, Users b, Student_Group c, Programs d"
                            + " where student_id=b.[user_id] and b.studentGroup_id=c.student_group_id and c.program_id=d.program_id and d.[type]='Degree'" + where;
                    }
                    else
                    {
                        return totalFlight;
                    }
                }
                com.CommandText = sql;
                totalFlight = (int)com.ExecuteScalar();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return totalFlight;
        }

        public bool SetupNotificationDegree(int days_before, DateTime deadline)
        {
            SqlConnection con = null;
            string sql = "begin tran if exists (select * from Config with (updlock,serializable) where [type] = 'flight' and [kind] = 'degree') begin update Config set days_before = @days_before, deadline = @deadline where [type] = 'flight' and [kind] = 'degree' end else begin insert into Config([type],[kind],days_before,deadline) values ('flight','degree',@days_before,@deadline) end commit tran";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@days_before", SqlDbType.Int);
                com.Parameters["@days_before"].Value = days_before;
                com.Parameters.Add("@deadline", SqlDbType.DateTime);
                com.Parameters["@deadline"].Value = deadline;
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

        public bool SetupNotificationMobility(int days_before)
        {
            SqlConnection con = null;
            string sql = "begin tran if exists (select * from Config with (updlock,serializable) where [type] = 'flight' and [kind] = 'mobility') begin update Config set days_before = @days_before where [type] = 'flight' and [kind] = 'mobility' end else begin insert into Config([type],[kind],days_before) values ('flight','mobility',@days_before) end commit tran";
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
