using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace System.App.Repositories.Common
{
    public static class SqlHelper
    {
        public static bool Insert<T>(T parameter, string connectionString) where T : class
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                sqlConnection.Insert(parameter);
                sqlConnection.Close();

                return true;
            }
        }

        public static bool Insert<T>(List<T> parameters, string connectionString) where T : class
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                sqlConnection.Insert<T>(parameters);
                sqlConnection.Close();

                return true;
            }
        }

        public static int InsertWithReturnId<T>(T parameter, string connectionString) where T : class
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                var recordId = sqlConnection.Insert(parameter);

                sqlConnection.Close();
                return recordId;
            }
        }

        public static bool Update<T>(T parameter, string connectionString) where T : class
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                sqlConnection.Update(parameter);
                sqlConnection.Close();
                return true;
            }
        }

        public static IList<T> GetAll<T>(string connectionString) where T : class
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var result = sqlConnection.GetList<T>();
                sqlConnection.Close();
                return result.ToList();
            }
        }

        public static T Find<T>(PredicateGroup predicate, string connectionString) where T : class
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var result = sqlConnection.GetList<T>(predicate).SingleOrDefault();
                sqlConnection.Close();
                return result;
            }
        }

        public static IEnumerable<T> Search<T>(PredicateGroup predicate, string connectionString) where T : class
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var result = sqlConnection.GetList<T>(predicate).ToList();
                sqlConnection.Close();
                return result;
            }
        }

        public static bool Delete<T>(PredicateGroup predicate,
            string connectionString) where T : class
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                sqlConnection.Delete<T>(predicate);
                sqlConnection.Close();
                return true;
            }
        }

        public static IEnumerable<T> QuerySP<T>(string storedProcedure, dynamic param = null,
            dynamic outParam = null, SqlTransaction transaction = null,
            bool buffered = true, int? commandTimeout = null,
            string connectionString = null) where T : class
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            var output = connection.Query<T>(storedProcedure, param: (object)param,
            transaction: transaction, buffered: buffered, commandTimeout: commandTimeout,
            commandType: CommandType.StoredProcedure);
            return output;
        }

        private static void CombineParameters(ref dynamic param, dynamic outParam = null)
        {
            if (outParam != null)
            {
                if (param != null)
                {
                    param = new DynamicParameters(param);
                    ((DynamicParameters)param).AddDynamicParams(outParam);
                }
                else
                {
                    param = outParam;
                }
            }
        }
        private static int ConnectionTimeout { get; set; }
    }
}