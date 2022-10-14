using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace System.App.Repositories.HCNS
{
    public class KhaiBaoVang
    {
        public Dictionary<string, string> DanhSachKhaiBao(string connectionString)
        {
            Dictionary<string, string> lst = new Dictionary<string, string>();

            string q = @"SELECT AbsentCode, AbsentDescription FROM AbsentSymbol ORDER BY AbsentCode";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(q, sqlConnection))
                {
                    var reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lst.Add(reader["AbsentCode"].ToString(), reader["AbsentDescription"].ToString());
                        }
                    }
                }
            }
            return lst;
        }
        public bool ThemMoiKhaiBao(string connectionString, KhaiBaoVang obj)
        {
            int rowAffected = 0;

            string _query = @"INSERT INTO Absent(UserEnrollNumber, TimeDate, AbsentCode, WorkingDay, WorkingTime, AddedTime, UserFullCode, Thang, Nam, Lydo) 
                VALUES (@UserEnrollNumber, @TimeDate, @AbsentCode, @WorkingDay, @WorkingTime, @AddedTime, @UserFullCode, @Thang, @Nam, @Lydo)";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(_query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@UserEnrollNumber", "");
                    sqlCommand.Parameters.AddWithValue("@TimeDate", "");
                    sqlCommand.Parameters.AddWithValue("@AbsentCode", "");
                    sqlCommand.Parameters.AddWithValue("@WorkingDay", "");
                    sqlCommand.Parameters.AddWithValue("@WorkingTime", "");
                    sqlCommand.Parameters.AddWithValue("@AddedTime", "");
                    sqlCommand.Parameters.AddWithValue("@UserFullCode", "");
                    sqlCommand.Parameters.AddWithValue("@Thang", "");
                    sqlCommand.Parameters.AddWithValue("@Nam", "");
                    sqlCommand.Parameters.AddWithValue("@Lydo", "");
                }
            }

            if (rowAffected > 0)
                return true;
            return false;
        }
    }
}
