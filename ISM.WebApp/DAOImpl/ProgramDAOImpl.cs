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
    public class ProgramDAOImpl : ProgramDAO
    {
        public List<ProgramStudy> getAllProgram()
        {
            SqlConnection con = null;
            string sql = "select * from Programs";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<ProgramStudy> programs = new List<ProgramStudy>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    ProgramStudy program = new ProgramStudy();
                    program.programm_id = (int)reader.GetValue(reader.GetOrdinal("program_id"));
                    program.program_name = (string)reader.GetValue(reader.GetOrdinal("program_name"));
                    if (!reader.IsDBNull(reader.GetOrdinal("description")))
                    {
                        program.description = (string)reader.GetValue(reader.GetOrdinal("description"));
                    }
                    programs.Add(program);
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
            return programs;
        }
    }
}
