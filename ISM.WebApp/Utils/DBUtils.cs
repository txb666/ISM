using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using ISM.WebApp.Constant;

namespace ISM.WebApp.Utils
{
    public class DBUtils
    {
        public static SqlConnection GetConnection()
        {
            try
            {
                //string connectionString = "Data Source=localhost;Initial Catalog=ISM;Integrated Security=true;";
                string connectionString = Query.STRING_CONNECTION;
                return new SqlConnection(connectionString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public static void closeAllResource(SqlConnection con, SqlCommand com, SqlDataReader reader, SqlDataAdapter adapter)
        {
            if (con != null && con.State == ConnectionState.Open)
            {
                con.Close();
                com.Dispose();
            }
            if (com != null)
            {
                com.Dispose();
            }
            if (reader != null)
            {
                reader.Close();
            }
            if (adapter != null)
            {
                adapter.Dispose();
            }
        }
    }
}
