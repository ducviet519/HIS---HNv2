using Dapper;
using DapperExtensions;
using System.App.Entities;
using System.App.Repositories.Common;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace System.App.Repositories
{
    public class TrainingCourse_Repo
    {
        public IEnumerable<TrainingCourse> List(string connectionString)
        {
            return SqlHelper.GetAll<TrainingCourse>(connectionString);
        }

        public TrainingCourse Find(string connectionString, int id)
        {
            var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            predicateGroup.Predicates.Add(Predicates.Field<TrainingCourse>(x => x.ID, Operator.Eq, id, false));
            return SqlHelper.Find<TrainingCourse>(predicateGroup, connectionString);
        }

        public int Insert(string connectionString, TrainingCourse obj)
        {
            return SqlHelper.InsertWithReturnId(obj, connectionString);
        }

        public bool InsertList(string connectionString, List<TrainingCourse> lstObj)
        {
            return SqlHelper.Insert(lstObj, connectionString);
        }

        public bool Update(string connectionString, TrainingCourse trainingCourse)
        {
            return SqlHelper.Update(trainingCourse, connectionString);
        }

        public bool UpdateClone(string connectionString, EmployeeInTraining employeeInTraining)
        {
            return SqlHelper.Insert(employeeInTraining, connectionString);
        }

        public bool UpdateDestroy(string connectionString, EmployeeInTraining employeeInTraining)
        {
            var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };

            predicateGroup.Predicates.Add(Predicates.Field<EmployeeInTraining>(x => x.EMP_ID, Operator.Eq, employeeInTraining.EMP_ID, false));
            predicateGroup.Predicates.Add(Predicates.Field<EmployeeInTraining>(x => x.TC_ID, Operator.Eq, employeeInTraining.TC_ID, false));

            return SqlHelper.Delete<EmployeeInTraining>(predicateGroup, connectionString);
        }

        public bool CounterEmployee(string connectionString, int courseId)
        {
            string q = "UPDATE TrainingCourse SET Quality = (SELECT COUNT(EMP_ID) FROM EmployeeInTraining et WHERE et.TC_ID = " + courseId + ") WHERE ID = " + courseId;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var result = sqlConnection.Query<TrainingCourse>(q);
                sqlConnection.Close();
                return true;
            }
        }

        public IEnumerable<Employee> ListEmployeeNotInTrainingCourse(string connectionString, int courseID)
        {
            string q = "SELECT et.UserEnrollNumber, et.UserFullCode, et.UserFullName, d.KhoaP as PhongKhoaHC, et.NgaySinh, ChucDanh, TAEmail FROM UserInfExt et LEFT JOIN Depts d ON d.STT = et.PhongKhoaHC WHERE et.UserEnrollNumber NOT IN (SELECT EMP_ID FROM EmployeeInTraining WHERE TC_ID = " + courseID + ") AND et.DaNghi = 0";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var result = sqlConnection.Query<Employee>(q);
                sqlConnection.Close();
                return result.ToList();
            }
        }

        public IEnumerable<Employee> ListEmployeeNotInTrainingCourse(string connectionString, int courseID, int dept)
        {
            string q = "SELECT et.UserEnrollNumber, et.UserFullCode, et.UserFullName, d.KhoaP as PhongKhoaHC, et.NgaySinh, ChucDanh, TAEmail FROM UserInfExt et LEFT JOIN Depts d ON d.STT = et.PhongKhoaHC WHERE d.STT = " + dept + " AND et.UserEnrollNumber NOT IN (SELECT EMP_ID FROM EmployeeInTraining WHERE TC_ID = " + courseID + ") AND et.DaNghi = 0";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var result = sqlConnection.Query<Employee>(q);
                sqlConnection.Close();
                return result.ToList();
            }
        }

        public IEnumerable<Employee> ListEmployee(string connectionString, int dept = 0)
        {
            string q = @"SELECT ex.UserEnrollNumber, ex.UserFullCode, ex.UserFullName, d.KhoaP as PhongKhoaHC, ex.NgaySinh, ChucDanh, TAEmail,
                        (select count(ID) from EmployeeInTraining where EMP_ID = ex.UserEnrollNumber) as total_course
                    FROM UserInfExt ex LEFT JOIN Depts d ON d.STT = ex.PhongKhoaHC WHERE ex.DaNghi = 0";

            if (dept > 0)
                q += " AND d.STT = " + dept;

            List<Employee> employees = new List<Employee>();

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
                            employees.Add(new Employee()
                            {
                                UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                                UserFullCode = reader["UserFullCode"].ToString(),
                                UserFullName = reader["UserFullName"].ToString(),
                                PhongKhoaHC = reader["PhongKhoaHC"].ToString(),
                                NgaySinh = String.IsNullOrEmpty(reader["NgaySinh"].ToString()) ? new DateTime() : DateTime.Parse(reader["NgaySinh"].ToString()),
                                ChucDanh = reader["ChucDanh"].ToString(),
                                Total_Course = int.Parse(reader["total_course"].ToString())
                            });
                        }
                    }
                }

                sqlConnection.Close();
                return employees;
            }
        }

        public IEnumerable<Employee> ListEmployeeInTrainingCourse(string connectionString, int courseID)
        {
            string q = "SELECT u.UserEnrollNumber, u.UserFullCode, u.UserFullName, d.KhoaP as PhongKhoaHC, u.NgaySinh, u.ChucDanh, u.TAEmail, u.UPN, et.CHUKY, et.NgayKy FROM EmployeeInTraining et LEFT JOIN UserInfExt u ON u.UserEnrollNumber = et.EMP_ID LEFT JOIN Depts d ON d.STT = u.PhongKhoaHC WHERE et.TC_ID = " + courseID;

            List<Employee> employees = new List<Employee>();

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
                            employees.Add(new Employee()
                            {
                                UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                                UserFullCode = reader["UserFullCode"].ToString(),
                                UserFullName = reader["UserFullName"].ToString(),
                                PhongKhoaHC = reader["PhongKhoaHC"].ToString(),
                                NgaySinh = DateTime.Parse(reader["NgaySinh"].ToString()),
                                ChucDanh = reader["ChucDanh"].ToString(),
                                TAEmail = reader["TAEmail"].ToString(),
                                CHUKY = reader["CHUKY"].ToString(),
                                NgayKy = reader["NgayKy"].ToString() != "" ? DateTime.Parse(reader["NgayKy"].ToString()) : (DateTime?)null,
                                UPN = reader["UPN"].ToString()
                            });
                        }
                    }
                }

                sqlConnection.Close();
                return employees;
            }
        }

        public DataTable ExportExcel(string connectionString, int courseID)
        {
            DataTable dataTable = new DataTable();
            string q = "SELECT Row_number() over(order by u.UserEnrollNumber) STT, u.UserFullCode AS 'Mã NV', u.UserFullName AS 'Họ và tên', d.KhoaP as 'Phòng/Khoa', u.ChucDanh as 'Chức danh' FROM EmployeeInTraining et LEFT JOIN UserInfExt u ON u.UserEnrollNumber = et.EMP_ID LEFT JOIN Depts d ON d.STT = u.PhongKhoaHC WHERE et.TC_ID = " + courseID;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(q, sqlConnection))
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                    dataAdapter.Fill(dataTable);
                }

                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }

            return dataTable;
        }

        public bool UpdateSignature(int empID, int courseID, string chuky, string connectionString)
        {
            var rowAffected = 0;
            string q = "UPDATE EmployeeInTraining SET CHUKY = '" + chuky + "', NgayKy = '" + DateTime.UtcNow.AddHours(7) + "' WHERE TC_ID = " + courseID + " AND EMP_ID = " + empID;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(q, sqlConnection))
                {
                    rowAffected = sqlCommand.ExecuteNonQuery();
                }

                sqlConnection.Close();
            }

            if (rowAffected > 0)
                return true;
            else
                return false;
        }

        public int CheckExist(string connectionString, string username, int courseID)
        {
            int rowAffected = 0;

            string q = "SELECT COUNT(ID) FROM EmployeeInTraining e LEFT JOIN UserInfExt ex ON e.EMP_ID = ex.UserEnrollNumber WHERE e.TC_ID = " + courseID + " AND ex.UPN = '" + username + "'";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand cmd = new SqlCommand(q, sqlConnection))
                {
                    object o = cmd.ExecuteScalar();

                    if (o != null)
                        rowAffected = int.Parse(o.ToString());
                    else
                        rowAffected = 0;
                }
                sqlConnection.Close();
            }

            return rowAffected;
        }

        public bool ImportExcel(string connectionString, List<TrainingCourse> courses)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                var rowEffected = 0;

                foreach (var item in courses)
                {
                    string q = @"SELECT ID FROM TrainingCourse WHERE Name = N'" + item.Name.Trim() + "'";

                    using (SqlCommand cmd = new SqlCommand(q, sqlConnection))
                    {
                        object o = cmd.ExecuteScalar();

                        if (o == null)
                            rowEffected = Insert(connectionString, item);
                        else
                            rowEffected = (int)o;
                    }

                    string ins = @"IF NOT EXISTS (SELECT ID FROM EmployeeInTraining WHERE TC_ID = " + rowEffected + @" AND EMP_ID = (SELECT UserEnrollNumber FROM UserInfo WHERE UserFullCode = N'" + item.EmployeeCode + @"')) BEGIN INSERT INTO EmployeeInTraining (TC_ID, EMP_ID)
                        VALUES (" + rowEffected + @", (SELECT UserEnrollNumber FROM UserInfo WHERE UserFullCode = N'" + item.EmployeeCode + "')) END";

                    using (SqlCommand cmd = new SqlCommand(ins, sqlConnection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                sqlConnection.Close();
            }
            return true;
        }

        public IEnumerable<TrainingCourse> TrainingCourseWithEmpID(string connectionString, int empId)
        {
            string q = @"SELECT distinct(tc.ID), tc.Type, tc.Name, tc.Lessions, tc.Cost, tc.DateFrom, tc.DateTo FROM TrainingCourse tc
	                LEFT JOIN EmployeeInTraining ei ON ei.TC_ID = tc.ID
                    WHERE ei.EMP_ID = " + empId;

            List<TrainingCourse> _trainingCourse = new List<TrainingCourse>();

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
                            _trainingCourse.Add(new TrainingCourse()
                            {
                                ID = int.Parse(reader["ID"].ToString()),
                                Type = reader["Type"].ToString(),
                                Name = reader["Name"].ToString(),
                                Lessions = int.Parse(reader["Lessions"].ToString()),
                                Cost = float.Parse(reader["Cost"].ToString()),
                                DateFrom = DateTime.Parse(reader["DateFrom"].ToString()),
                                DateTo = DateTime.Parse(reader["DateTo"].ToString())
                            });
                        }
                    }
                }

                sqlConnection.Close();
                return _trainingCourse;
            }
        }

        public DataTable ExportTrainingCourseWithEmpID(string connectionString, int empId)
        {
            string q = @"SELECT distinct(tc.ID),
	                    (case tc.[Type] when 0 then N'Nội viện' when 1 then N'Ngoại viện' end) as 'Loại hình', tc.Name as N'Tên khóa đào tạo', tc.Lessions as N'Số tiết học',
	                    tc.Cost as 'Giá', tc.DateFrom as 'Bắt đầu', tc.DateTo as 'Kết thúc' FROM TrainingCourse tc
	                    LEFT JOIN EmployeeInTraining ei ON ei.TC_ID = tc.ID
                    WHERE ei.EMP_ID = " + empId;

            DataTable data = new DataTable();

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(q, sqlConnection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                    adapter.Fill(data);
                }

                sqlConnection.Close();
                return data;
            }
        }
    }
}