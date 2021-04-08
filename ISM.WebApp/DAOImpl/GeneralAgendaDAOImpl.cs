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
    public class GeneralAgendaDAOImpl : GeneralAgendaDAO
    {
        public bool EditGeneralAgenda(int general_agenda_id, string content, string note)
        {
            SqlConnection con = null;
            string sql = "";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string set = "";
                if (!string.IsNullOrEmpty(content))
                {
                    set += " content=@content, ";
                    com.Parameters.Add("@content", SqlDbType.NVarChar);
                    com.Parameters["@content"].Value = content;
                }
                com.Parameters.Add("@general_agenda_id", SqlDbType.Int);
                com.Parameters["@general_agenda_id"].Value = general_agenda_id;               
                com.Parameters.Add("@note", SqlDbType.NVarChar);
                com.Parameters["@note"].Value = note;
                if (string.IsNullOrEmpty(note))
                {
                    com.Parameters["@note"].Value = DBNull.Value;
                }
                sql = " update General_Agenda set " + set + " note=@note where general_agenda_id=@general_agenda_id";
                com.CommandText = sql;
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

        public GeneralAgenda GetGeneralAgendaByStudentGroup(int student_group_id)
        {
            SqlConnection con = null;
            string sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            GeneralAgenda generalAgenda = new GeneralAgenda();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                sql = " select count(*) from General_Agenda where studentGroup_id=@student_group_id";
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@student_group_id", SqlDbType.Int);
                com.Parameters["@student_group_id"].Value = student_group_id;
                int count = (int)com.ExecuteScalar();
                if (count == 0)
                {
                    sql = " insert into General_Agenda(studentGroup_id) values (@student_group_id); select SCOPE_IDENTITY();";
                    com = new SqlCommand(sql, con);
                    com.Parameters.Add("@student_group_id", SqlDbType.Int);
                    com.Parameters["@student_group_id"].Value = student_group_id;
                    decimal inserted_idraw = (decimal)com.ExecuteScalar();
                    int inserted_id = Convert.ToInt32(inserted_idraw);
                    generalAgenda.general_agenda_id = inserted_id;
                    generalAgenda.student_group_id = student_group_id;
                }
                else
                {
                    sql = " select * from General_Agenda where studentGroup_id=@student_group_id";
                    com = new SqlCommand(sql, con);
                    com.Parameters.Add("@student_group_id", SqlDbType.Int);
                    com.Parameters["@student_group_id"].Value = student_group_id;
                    reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        generalAgenda.general_agenda_id = (int)reader.GetValue(reader.GetOrdinal("general_agenda_id"));
                        generalAgenda.student_group_id = (int)reader.GetValue(reader.GetOrdinal("studentGroup_id"));
                        if (!reader.IsDBNull(reader.GetOrdinal("content")))
                        {
                            generalAgenda.content = (string)reader.GetValue(reader.GetOrdinal("content"));
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("note")))
                        {
                            generalAgenda.note = (string)reader.GetValue(reader.GetOrdinal("note"));
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, reader, null);
            }
            return generalAgenda;
        }
    }
}
