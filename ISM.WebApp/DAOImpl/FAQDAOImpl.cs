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
    public class FAQDAOImpl : FAQDAO
    {
        public bool createFAQ(string question, string answer)
        {
            SqlConnection con = null;
            SqlCommand com = null;
            string sql = "insert into FAQs(question,answer) values (@question,@answer)";
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@question", SqlDbType.NVarChar);
                com.Parameters["@question"].Value = question;
                com.Parameters.Add("@answer", SqlDbType.NVarChar);
                com.Parameters["@answer"].Value = answer;
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

        public bool deleteFAQ(int faq_id)
        {
            SqlConnection con = null;
            SqlCommand com = null;
            string sql = "delete from FAQs where faq_id=@faq_id";
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@faq_id", SqlDbType.Int);
                com.Parameters["@faq_id"].Value = faq_id;
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

        public bool editFAQ(int faq_id, string question, string answer)
        {
            SqlConnection con = null;
            SqlCommand com = null;
            string sql = "update FAQs set question=@question, answer=@answer where faq_id=@faq_id";
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@question", SqlDbType.NVarChar);
                com.Parameters["@question"].Value = question;
                com.Parameters.Add("@answer", SqlDbType.NVarChar);
                com.Parameters["@answer"].Value = answer;
                com.Parameters.Add("@faq_id", SqlDbType.Int);
                com.Parameters["@faq_id"].Value = faq_id;
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

        public List<FAQ> getFAQ(int page, int pageSize, string question, string answer)
        {
            int from = page * pageSize - (pageSize - 1);
            int to = page * pageSize;
            SqlConnection con = null;
            SqlCommand com = null;
            string sql = "";
            SqlDataReader reader = null;
            List<FAQ> faqs = new List<FAQ>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = "";
                if (!string.IsNullOrEmpty(question))
                {
                    where += " and upper(question) like upper('%' + @question + '%')";
                    com.Parameters.Add("@question", SqlDbType.NVarChar);
                    com.Parameters["@question"].Value = question;
                }
                if (!string.IsNullOrEmpty(answer))
                {
                    where += " and upper(answer) like upper('%' + @answer + '%')";
                    com.Parameters.Add("@answer", SqlDbType.NVarChar);
                    com.Parameters["@answer"].Value = answer;
                }
                com.Parameters.Add("@from", SqlDbType.Int);
                com.Parameters["@from"].Value = from;
                com.Parameters.Add("@to", SqlDbType.Int);
                com.Parameters["@to"].Value = to;
                sql = " select * from("
                    + " select ROW_NUMBER() over (order by faq_id asc) rownumber,faq_id,question,answer from FAQs where 1=1" + where
                    + " ) as temp where temp.rownumber>=@from and temp.rownumber<=@to";
                com.CommandText = sql;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    FAQ faq = new FAQ();
                    faq.faq_id = (int)reader.GetValue(reader.GetOrdinal("faq_id"));
                    faq.question = (string)reader.GetValue(reader.GetOrdinal("question"));
                    faq.answer = (string)reader.GetValue(reader.GetOrdinal("answer"));
                    faqs.Add(faq);
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, reader, null);
            }
            return faqs;
        }

        public int getTotalFAQ(string question, string answer)
        {
            SqlConnection con = null;
            SqlCommand com = null;
            string sql = "";
            int totalFAQ = 0;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                string where = "";
                if (!string.IsNullOrEmpty(question))
                {
                    where += " and upper(question) like upper('%' + @question + '%')";
                    com.Parameters.Add("@question", SqlDbType.NVarChar);
                    com.Parameters["@question"].Value = question;
                }
                if (!string.IsNullOrEmpty(answer))
                {
                    where += " and upper(answer) like upper('%' + @answer + '%')";
                    com.Parameters.Add("@answer", SqlDbType.NVarChar);
                    com.Parameters["@answer"].Value = answer;
                }
                sql = " select count(*) from FAQs where 1=1" + where;
                com.CommandText = sql;
                totalFAQ = (int)com.ExecuteScalar();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return totalFAQ;
        }
    }
}
