using System.App.Entities.HCNS;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace System.App.Repositories.HCNS
{
    public class ZK_Repo
    {
        public IEnumerable<ZK_Machine> DS_ThietBi(string ConnectionString)
        {
            string __query = "SELECT ID, MachineID, DeviceName, IPAddress, Port, UserCount, FPCount, FaceCount, AttLogCount FROM Machines WHERE AreaID = 1";

            List<ZK_Machine> lstMachines = new List<ZK_Machine>();

            try
            {
                using (var sqlConnection = new SqlConnection(ConnectionString))
                {
                    if (sqlConnection.State == ConnectionState.Closed)
                        sqlConnection.Open();

                    using (var cmd = new SqlCommand(__query, sqlConnection))
                    {
                        var reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                lstMachines.Add(new ZK_Machine()
                                {
                                    ID = int.Parse(reader["ID"].ToString()),
                                    MachineID = int.Parse(reader["MachineID"].ToString()),
                                    DeviceName = reader["DeviceName"].ToString(),
                                    IPAddress = reader["IPAddress"].ToString(),
                                    Port = int.Parse(reader["Port"].ToString()),
                                    UserCount = reader["UserCount"].ToString() == "" ? 0 : int.Parse(reader["UserCount"].ToString()),
                                    FPCount = reader["FPCount"].ToString() == "" ? 0 : int.Parse(reader["FPCount"].ToString()),
                                    FaceCount = reader["FaceCount"].ToString() == "" ? 0 : int.Parse(reader["FaceCount"].ToString()),
                                    AttLogCount = reader["AttLogCount"].ToString() == "" ? 0 : int.Parse(reader["AttLogCount"].ToString())
                                });
                            }
                        }
                    }

                    if (sqlConnection.State == ConnectionState.Open)
                        sqlConnection.Close();
                }

                return lstMachines;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ZK_Person_Finger> DS_VanTay(string ConnectionString, ArrayList arrObj)
        {
            List<ZK_Person_Finger> lsVanTay = new List<ZK_Person_Finger>();

            try
            {
                string str_id = "";

                for (int i = 0; i < arrObj.Count; i++)
                {
                    str_id += ((DataRow)arrObj[i])[0].ToString() + ",";
                }

                string __query = "SELECT a.UserEnrollNumber, x.UserFullName, FingerID, FingerTemplate, TempLength, Flag FROM Template a LEFT JOIN UserInfExt x ON a.UserEnrollNumber = x.UserEnrollNumber WHERE a.UserEnrollNumber IN (" + str_id.Substring(0, str_id.Length - 1) + ") AND Flag = 1";

                using (var sqlConnection = new SqlConnection(ConnectionString))
                {
                    if (sqlConnection.State == ConnectionState.Closed)
                        sqlConnection.Open();

                    using (var cmd = new SqlCommand(__query, sqlConnection))
                    {
                        var reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                lsVanTay.Add(new ZK_Person_Finger()
                                {
                                    UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                                    UserFullName = reader["UserFullName"].ToString(),
                                    FingerIndex = int.Parse(reader["FingerID"].ToString()),
                                    DataFinger = reader["FingerTemplate"].ToString(),
                                    DataLength = int.Parse(reader["TempLength"].ToString()),
                                    Flag = bool.Parse(reader["Flag"].ToString()) ? 1 : 0
                                });
                            }
                        }
                    }

                    if (sqlConnection.State == ConnectionState.Open)
                        sqlConnection.Close();
                }

                return lsVanTay;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ZK_Person> DS_User(string ConnectionString)
        {
            string __query = @"SELECT a.UserEnrollNumber, a.UserFullCode, TATitle, a.UserFullName, NgaySinh, ChucDanh, GioiTinh, b.UserPW,
                                (SELECT COUNT(CAST(FingerTemplate AS nvarchar(MAX))) FROM Template temp WHERE UserEnrollNumber = a.UserEnrollNumber AND LEN(CAST(FingerTemplate AS nvarchar(MAX))) > 0) AS SL_VanTay,
	                            (SELECT COUNT(CAST(FaceTemplate AS nvarchar(MAX))) FROM Template_Face temp WHERE UserEnrollNumber = a.UserEnrollNumber AND LEN(CAST(FaceTemplate AS nvarchar(MAX))) > 0) AS SL_Face
                            FROM UserInfExt a 
                                left join UserInfo b on a.UserEnrollNumber = b.UserEnrollNumber
                            WHERE DaNghi = 0 ORDER BY UserEnrollNumber DESC";

            List<ZK_Person> lstMachines = new List<ZK_Person>();

            try
            {
                using (var sqlConnection = new SqlConnection(ConnectionString))
                {
                    if (sqlConnection.State == ConnectionState.Closed)
                        sqlConnection.Open();

                    using (var cmd = new SqlCommand(__query, sqlConnection))
                    {
                        var reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                lstMachines.Add(new ZK_Person()
                                {
                                    UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                                    UserFullCode = reader["UserFullCode"].ToString(),
                                    TATitle = reader["TATitle"].ToString(),
                                    UserFullName = reader["UserFullName"].ToString(),
                                    NgaySinh = String.IsNullOrEmpty(reader["NgaySinh"].ToString()) ? "" : DateTime.Parse(reader["NgaySinh"].ToString()).ToString("dd/MM/yyyy"),
                                    ChucDanh = reader["ChucDanh"].ToString(),
                                    GioiTinh = reader["GioiTinh"].ToString() == "0" ? "Nam" : "Nữ",
                                    UserPW = reader["UserPW"].ToString(),
                                    VanTay = int.Parse(reader["SL_VanTay"].ToString()) > 0 ? true : false,
                                    Face = int.Parse(reader["SL_Face"].ToString()) > 0 ? true : false
                                });
                            }
                        }
                    }

                    if (sqlConnection.State == ConnectionState.Open)
                        sqlConnection.Close();
                }

                return lstMachines;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ZK_Person> DS_UserNghiViec(string ConnectionString)
        {
            string __query = @"SELECT a.UserEnrollNumber, a.UserFullCode, TATitle, a.UserFullName, NgaySinh, ChucDanh, GioiTinh, b.UserPW,
                                (SELECT COUNT(CAST(FingerTemplate AS nvarchar(MAX))) FROM Template temp WHERE UserEnrollNumber = a.UserEnrollNumber AND LEN(CAST(FingerTemplate AS nvarchar(MAX))) > 0) AS SL_VanTay,
                                (SELECT COUNT(CAST(FaceTemplate AS nvarchar(MAX))) FROM Template_Face temp WHERE UserEnrollNumber = a.UserEnrollNumber AND LEN(CAST(FaceTemplate AS nvarchar(MAX))) > 0) AS SL_Face
                            FROM UserInfExt a 
                                left join UserInfo b on a.UserEnrollNumber = b.UserEnrollNumber
                            WHERE DaNghi = 1 ORDER BY UserEnrollNumber DESC";

            List<ZK_Person> lstMachines = new List<ZK_Person>();

            try
            {
                using (var sqlConnection = new SqlConnection(ConnectionString))
                {
                    if (sqlConnection.State == ConnectionState.Closed)
                        sqlConnection.Open();

                    using (var cmd = new SqlCommand(__query, sqlConnection))
                    {
                        var reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                lstMachines.Add(new ZK_Person()
                                {
                                    UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                                    UserFullCode = reader["UserFullCode"].ToString(),
                                    TATitle = reader["TATitle"].ToString(),
                                    UserFullName = reader["UserFullName"].ToString(),
                                    NgaySinh = String.IsNullOrEmpty(reader["NgaySinh"].ToString()) ? "" : DateTime.Parse(reader["NgaySinh"].ToString()).ToString("dd/MM/yyyy"),
                                    ChucDanh = reader["ChucDanh"].ToString(),
                                    GioiTinh = reader["GioiTinh"].ToString() == "0" ? "Nam" : "Nữ",
                                    UserPW = reader["UserPW"].ToString(),
                                    VanTay = int.Parse(reader["SL_VanTay"].ToString()) > 0 ? true : false,
                                    Face = int.Parse(reader["SL_Face"].ToString()) > 0 ? true : false
                                });
                            }
                        }
                    }

                    if (sqlConnection.State == ConnectionState.Open)
                        sqlConnection.Close();
                }

                return lstMachines;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool KiemTra_Template(string ConnectionString, int userEnrollNumber)
        {
            string __query = "SELECT COUNT(UserEnrollNumber) FROM Template WHERE UserEnrollNumber = @UserEnrollNumber";

            int rowAffected = 0;

            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var cmd = new SqlCommand(__query, sqlConnection))
                {
                    var obj = cmd.ExecuteScalar();

                    if (obj != null)
                    {
                        rowAffected = (int)obj;
                    }
                }

                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }

            return rowAffected == 0 ? false : true;
        }

        public bool CapNhat_Template(string ConnectionString, List<ZK_Person_Finger> lsObj)
        {
            string __query = "";

            int rowAffected = 0;

            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                foreach (var item in lsObj)
                {
                    __query = @"
                    IF EXISTS (SELECT UserEnrollNumber FROM Template WHERE UserEnrollNumber = " + item.UserEnrollNumber + @" AND FingerID = " + item.FingerIndex + @")
                        BEGIN
	                        UPDATE Template SET FingerTemplate = '" + item.DataFinger + "',TempLength = " + item.DataLength + ",Flag = " + item.Flag + " WHERE UserEnrollNumber = " + item.UserEnrollNumber + " AND FingerID = " + item.FingerIndex + @"
                        END
                    ELSE
                        BEGIN
	                        INSERT INTO Template(UserEnrollNumber, FingerID, FingerTemplate, TempLength, Flag) VALUES (" + item.UserEnrollNumber + ", " + item.FingerIndex + ", '" + item.DataFinger + "', " + item.DataLength + ", " + item.Flag + ")" + @"
                        END";

                    using (var cmd = new SqlCommand(__query.ToString(), sqlConnection))
                    {
                        rowAffected += cmd.ExecuteNonQuery();
                    }
                }

                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }

            return rowAffected == 0 ? false : true;
        }
    }
}