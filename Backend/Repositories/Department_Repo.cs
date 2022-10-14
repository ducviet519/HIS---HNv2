using Dapper;
using System.App.Entities;
using System.App.Repositories.Common;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace System.App.Repositories
{
    public class Department_Repo
    {
        public int Insert(string connectionString, Department obj)
        {
            return SqlHelper.InsertWithReturnId(obj, connectionString);
        }

        public bool InsertList(string connectionString, List<Department> lstObj)
        {
            return SqlHelper.Insert(lstObj, connectionString);
        }

        public IEnumerable<Department> ListDepartment(string connectionString)
        {
            string q = "SELECT STT, KhoaP FROM Depts ORDER BY KhoaP";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var result = sqlConnection.Query<Department>(q);
                sqlConnection.Close();
                return result.ToList();
            }
        }
    }
}