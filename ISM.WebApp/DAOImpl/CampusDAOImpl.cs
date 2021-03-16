using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISM.WebApp.Utils;

namespace ISM.WebApp.DAOImpl
{
    public class CampusDAOImpl : CampusDAO
    {
        public List<Campus> getAllCampus()
        {
            SqlConnection con = null;
            string sql = "select * from Campus";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<Campus> campuses = new List<Campus>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Campus campus = new Campus();
                    campus.campus_id = (int)reader.GetValue(reader.GetOrdinal("campus_id"));
                    campus.campus_name = (string)reader.GetValue(reader.GetOrdinal("campus_name"));
                    if (!reader.IsDBNull(reader.GetOrdinal("description")))
                    {
                        campus.description = (string)reader.GetValue(reader.GetOrdinal("description"));
                    }
                    campuses.Add(campus);
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
            return campuses;
        }
    }
}
