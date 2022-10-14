using System.App.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace System.App.Repositories
{
    public class Config_Repo
    {
        public Config GetConfig(string connectionString, string param)
        {
            Config config = new Config();

            string sql = "SELECT ID, Parameter, ParameterID, StringVal, NumberVal, DatetimeVal FROM Config WHERE ParameterID = @ParameterID";

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == Data.ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand(sql, sConnection))
                {
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.Add("@ParameterID", SqlDbType.VarChar).Value = param;

                    var reader = sCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            config.ID = int.Parse(reader["ID"].ToString());
                            config.Parameter = reader["Parameter"].ToString();
                            config.ParameterID = reader["ParameterID"].ToString();
                            config.StringVal = reader["StringVal"].ToString();
                            config.NumberVal = int.Parse(reader["NumberVal"].ToString());
                            config.DateTimeVal = string.IsNullOrEmpty(reader["DatetimeVal"].ToString()) ? (DateTime?)null : (DateTime)reader["DatetimeVal"];
                        }
                    }
                }
            }

            return config;
        }
    }
}