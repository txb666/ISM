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
    public class ArticleDAOImpl : ArticleDAO
    {
        public bool CreateArticle(string type, string title, string fileName)
        {
            SqlConnection con = null;
            string sql = "insert into Articles([type],title,[fileName]) values (@type,@title,@fileName)";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@type", SqlDbType.NVarChar);
                com.Parameters["@type"].Value = type;
                com.Parameters.Add("@title", SqlDbType.NVarChar);
                com.Parameters["@title"].Value = title;
                com.Parameters.Add("@fileName", SqlDbType.NVarChar);
                com.Parameters["@fileName"].Value = fileName;
                if (string.IsNullOrEmpty(fileName))
                {
                    com.Parameters["@fileName"].Value = DBNull.Value;
                }
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

        public bool DeleteArticle(int article_id)
        {
            SqlConnection con = null;
            string sql = "delete from Articles where article_id=@article_id";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@article_id", SqlDbType.Int);
                com.Parameters["@article_id"].Value = article_id;              
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

        public bool EditArticle(int article_id, string title, string fileName)
        {
            SqlConnection con = null;
            string sql = "update Articles set title=@title,[fileName]=@fileName where article_id=@article_id";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);        
                com.Parameters.Add("@title", SqlDbType.NVarChar);
                com.Parameters["@title"].Value = title;
                com.Parameters.Add("@fileName", SqlDbType.NVarChar);
                com.Parameters["@fileName"].Value = fileName;
                if (string.IsNullOrEmpty(fileName))
                {
                    com.Parameters["@fileName"].Value = DBNull.Value;
                }
                com.Parameters.Add("@article_id", SqlDbType.Int);
                com.Parameters["@article_id"].Value = article_id;
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

        public Article getArticleById(int article_id)
        {
            SqlConnection con = null;
            string sql = "select * from Articles where article_id=@article_id";
            SqlCommand com = null;
            SqlDataReader reader = null;
            Article article = new Article();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@article_id", SqlDbType.Int);
                com.Parameters["@article_id"].Value = article_id;
                reader = com.ExecuteReader();
                while (reader.Read())
                {                  
                    article.article_id = (int)reader.GetValue(reader.GetOrdinal("article_id"));
                    article.type = (string)reader.GetValue(reader.GetOrdinal("type"));
                    article.title = (string)reader.GetValue(reader.GetOrdinal("title"));
                    if (!reader.IsDBNull(reader.GetOrdinal("fileName")))
                    {
                        article.fileName = (string)reader.GetValue(reader.GetOrdinal("fileName"));
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
            return article;
        }

        public List<Article> getArticleByType(string type)
        {
            SqlConnection con = null;
            string sql = "select * from Articles where [type]=@type";
            SqlCommand com = null;
            SqlDataReader reader = null;
            List<Article> articles = new List<Article>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@type", SqlDbType.NVarChar);
                com.Parameters["@type"].Value = type;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Article article = new Article();
                    article.article_id = (int)reader.GetValue(reader.GetOrdinal("article_id"));
                    article.type = (string)reader.GetValue(reader.GetOrdinal("type"));
                    article.title = (string)reader.GetValue(reader.GetOrdinal("title"));
                    if (!reader.IsDBNull(reader.GetOrdinal("fileName")))
                    {
                        article.fileName = (string)reader.GetValue(reader.GetOrdinal("fileName"));
                    }
                    articles.Add(article);
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
            return articles;
        }

        public bool isTitleExist(string title, string type)
        {
            SqlConnection con = null;
            string sql = "select count(*) from Articles where [type]=@type and title=@title";
            SqlCommand com = null;
            bool isExist = true;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, null, null);
            }
            return isExist;
        }
    }
}
