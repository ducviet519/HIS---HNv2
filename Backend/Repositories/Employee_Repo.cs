using Oracle.ManagedDataAccess.Client;
using System.App.Entities;
using System.App.Repositories.Common;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace System.App.Repositories
{
    public class Employee_Repo
    {
        public int Insert(string connectionString, Employee obj)
        {
            return SqlHelper.InsertWithReturnId(obj, connectionString);
        }

        public bool InsertList(string connectionString, List<Employee> lstObj)
        {
            return SqlHelper.Insert(lstObj, connectionString);
        }

        public Employee Find(string connectionString, int id)
        {
            string q = @"SELECT et.UserEnrollNumber, et.UserFullCode, et.UserFullName, d.KhoaP as PhongKhoaHC, et.NgaySinh, ChucDanh, TAEmail
                FROM UserInfExt et LEFT JOIN Depts d ON d.STT = et.PhongKhoaHC where et.UserEnrollNumber = " + id;

            Employee employee = new Employee();

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(q, sqlConnection))
                {
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            employee.UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString());
                            employee.UserFullCode = reader["UserFullCode"].ToString();
                            employee.UserFullName = reader["UserFullName"].ToString();
                            employee.PhongKhoaHC = reader["PhongKhoaHC"].ToString();
                            employee.NgaySinh = String.IsNullOrEmpty(reader["NgaySinh"].ToString()) ? new DateTime() : DateTime.Parse(reader["NgaySinh"].ToString());
                            employee.ChucDanh = reader["ChucDanh"].ToString();
                        }
                    }
                }

                sqlConnection.Close();
            }

            return employee;
        }

        public IEnumerable<EmployeeHealth> SucKhoeNhanVien(string connectionString, string time)
        {
            List<EmployeeHealth> lst = new List<EmployeeHealth>();

            string q = @"SELECT
                             'Loại ' || a.tk_dieutri AS healthtype,
                             COUNT(a.tk_dieutri) AS amount,
                             SUM(COUNT(a.tk_dieutri) ) OVER() AS total,
                             round(100 * (COUNT(a.tk_dieutri) / SUM(COUNT(a.tk_dieutri) ) OVER() ),2) AS percentage
                         FROM
                             hsofttamanh1118.ba_chung a
                             LEFT JOIN hsofttamanh1118.benhanpk b ON a.id = b.maql
                             LEFT JOIN hsofttamanh.btdbn c ON b.mabn = c.mabn
                         WHERE
                             c.mann = 19
                             AND a.tk_dieutri IS NOT NULL
                         GROUP BY
                             a.tk_dieutri
                         ORDER BY
                             a.tk_dieutri";

            using (OracleConnection oracleConnection = new OracleConnection(connectionString))
            {
                if (oracleConnection.State == ConnectionState.Closed)
                    oracleConnection.Open();

                using (OracleCommand cmd = new OracleCommand(q, oracleConnection))
                {
                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lst.Add(new EmployeeHealth()
                            {
                                Type = reader["healthtype"].ToString(),
                                Amount = int.Parse(reader["amount"].ToString()),
                                Total = int.Parse(reader["Total"].ToString()),
                                Percentage = float.Parse(reader["percentage"].ToString())
                            });
                        }
                    }
                }
            }

            return lst;
        }
    }
}