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
    public class LocalRecommendationDAOImpl : LocalRecommendationDAO
    {
        public bool editLocalRecommendation(int local_recommendation_id, string title, string file_name)
        {
            SqlConnection con = null;
            string sql = "update Local_Recommendation set title=@title,[file_name]=@file_name where local_recommendation_id=@local_recommendation_id";
            SqlCommand com = null;
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@title", SqlDbType.NVarChar);
                com.Parameters["@title"].Value = title;
                com.Parameters.Add("@file_name", SqlDbType.NVarChar);
                com.Parameters["@file_name"].Value = file_name;
                com.Parameters.Add("@local_recommendation_id", SqlDbType.Int);
                com.Parameters["@local_recommendation_id"].Value = local_recommendation_id;
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

        public List<LocalRecommendation> getAllLocalRecommendation()
        {
            SqlConnection con = null;
            string sql = "select * from Local_Recommendation";
            SqlCommand com = null;
            SqlDataReader reader = null;
            List<LocalRecommendation> localRecommendations = new List<LocalRecommendation>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    LocalRecommendation localRecommendation = new LocalRecommendation();
                    localRecommendation.local_recommendation_id = (int)reader.GetValue(reader.GetOrdinal("local_recommendation_id"));
                    localRecommendation.campus_id = (int)reader.GetValue(reader.GetOrdinal("campus_id"));
                    localRecommendation.title = (string)reader.GetValue(reader.GetOrdinal("title"));
                    if (!reader.IsDBNull(reader.GetOrdinal("file_name")))
                    {
                        localRecommendation.file_name = (string)reader.GetValue(reader.GetOrdinal("file_name"));
                    }
                    localRecommendations.Add(localRecommendation);
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
            return localRecommendations;
        }

        public LocalRecommendation GetLocalRecommendationById(int local_recommendation_id)
        {
            SqlConnection con = null;
            string sql = "select * from Local_Recommendation where local_recommendation_id=@local_recommendation_id";
            SqlCommand com = null;
            SqlDataReader reader = null;
            LocalRecommendation localRecommendation = new LocalRecommendation();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                com.Parameters.Add("@local_recommendation_id", SqlDbType.Int);
                com.Parameters["@local_recommendation_id"].Value = local_recommendation_id;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    localRecommendation.local_recommendation_id = (int)reader.GetValue(reader.GetOrdinal("local_recommendation_id"));
                    localRecommendation.campus_id = (int)reader.GetValue(reader.GetOrdinal("campus_id"));
                    localRecommendation.title = (string)reader.GetValue(reader.GetOrdinal("title"));
                    if (!reader.IsDBNull(reader.GetOrdinal("file_name")))
                    {
                        localRecommendation.file_name = (string)reader.GetValue(reader.GetOrdinal("file_name"));
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
            return localRecommendation;
        }
    }
}
