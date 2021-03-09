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
    public class RoleDAOImpl : RoleDAO
    {
        public List<Role> getAllRole(int page, int pageSize)
        {
            int from = page * pageSize - (pageSize - 1);
            int to = page * pageSize;
            SqlConnection con = null;
            String sql = "";
            SqlDataReader reader = null;
            SqlCommand com = null;
            List<Role> roles = new List<Role>();
            try
            {
                con = DBUtils.GetConnection();
                con.Open();
                com = new SqlCommand(sql, con);
                sql = "select * from " +
                         "(select *, ROW_NUMBER() over(order by id asc) as rownumber from[Role]) as temp " +
                         "where temp.rownumber >= @from and temp.rownumber <= @to";
                com.CommandText = sql;
                com.Parameters.Add("@from", SqlDbType.Int);
                com.Parameters["@from"].Value = from;
                com.Parameters.Add("@to", SqlDbType.Int);
                com.Parameters["@to"].Value = to;
                reader = com.ExecuteReader();
                while(reader.Read()){
                    Role newRole = new Role();
                    newRole.id = (int)reader.GetValue(reader.GetOrdinal("id"));
                    newRole.name = (string)reader.GetValue(reader.GetOrdinal("role_name"));
                    roles.Add(newRole);
                }
                return roles;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DBUtils.closeAllResource(con, com, reader, null);
            }
            return null;
        }
    }
}
