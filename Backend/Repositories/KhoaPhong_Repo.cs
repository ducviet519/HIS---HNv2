using Oracle.ManagedDataAccess.Client;
using System.App.Entities;
using System.Collections.Generic;
using System.Data;

namespace System.App.Repositories
{
    public class KhoaPhong_Repo
    {
        public IEnumerable<KhoaPhong> List(string connectionString)
        {
            List<KhoaPhong> lstKhoaPhong = new List<KhoaPhong>();

            string queryOracle = "select MAKP, TENKP, NGAYUD, MABH from hsofttamanh.btdkp_bv where loai = 0";

            using (var oracleConnection = new OracleConnection(connectionString))
            {
                if (oracleConnection.State == ConnectionState.Closed)
                    oracleConnection.Open();

                using (var oracleCommand = new OracleCommand(queryOracle, oracleConnection))
                {
                    OracleDataReader dataReader = oracleCommand.ExecuteReader();

                    while (dataReader.Read())
                    {
                        lstKhoaPhong.Add(new KhoaPhong()
                        {
                            MAKP = dataReader["MAKP"].ToString(),
                            TENKP = dataReader["TENKP"].ToString(),
                            NGAYUD = DateTime.Parse(dataReader["NGAYUD"].ToString()),
                            MABH = dataReader["MABH"].ToString()
                        });
                    }
                }

                oracleConnection.Close();
            }

            return lstKhoaPhong;
        }
    }
}