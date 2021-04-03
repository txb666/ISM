using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using ISM.WebApp.Utils;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAOImpl
{
    public class StudentHandbookDAOImpl : StudentHandbookDAO
    {
        public List<StudentHandbook> getAllStudentHandbook()
        {
            SqlConnection con = null;
            string sql = "select * from Student_Handbook";
            SqlCommand com = null;
            SqlDataReader reader = null;
            List<StudentHandbook> studentHandbooks = new List<StudentHandbook>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    StudentHandbook studentHandbook = new StudentHandbook();
                    studentHandbook.student_handbook_id = (int)reader.GetValue(reader.GetOrdinal("student_handbook_id"));
                    studentHandbook.campus_id = (int)reader.GetValue(reader.GetOrdinal("campus_id"));
                    if (!reader.IsDBNull(reader.GetOrdinal("file_name")))
                    {
                        studentHandbook.file_name = (string)reader.GetValue(reader.GetOrdinal("file_name"));
                    }
                    studentHandbook.title = (string)reader.GetValue(reader.GetOrdinal("title"));
                    studentHandbooks.Add(studentHandbook);
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
            return studentHandbooks;
        }

        public StudentHandbook GetStudentHandbookById(int student_handbook_id)
        {
            SqlConnection con = null;
            string sql = "select * from Student_Handbook where student_handbook_id=@student_handbook_id";
            SqlCommand com = null;
            SqlDataReader reader = null;
            StudentHandbook studentHandbook = new StudentHandbook();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@student_handbook_id", SqlDbType.Int);
                com.Parameters["@student_handbook_id"].Value = student_handbook_id;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    studentHandbook.student_handbook_id = (int)reader.GetValue(reader.GetOrdinal("student_handbook_id"));
                    studentHandbook.campus_id = (int)reader.GetValue(reader.GetOrdinal("campus_id"));
                    studentHandbook.title = (string)reader.GetValue(reader.GetOrdinal("title"));
                    if (!reader.IsDBNull(reader.GetOrdinal("file_name")))
                    {
                        studentHandbook.file_name = (string)reader.GetValue(reader.GetOrdinal("file_name"));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return studentHandbook;
        }
    }
}
