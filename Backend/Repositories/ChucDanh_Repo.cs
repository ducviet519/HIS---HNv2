using Dapper;
using System;
using System.App.Entities.HCNS;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace System.App.Repositories
{
    public class ChucDanh_Repo
    {
        public IEnumerable<ChucDanh> DanhSach(string connectionString)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("cmd", "all");

                return db.Query<ChucDanh>("sp_dm_chucdanh", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public ChucDanh ThongTin(string connectionString, int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("cmd", "info");
                parameters.Add("id", id);

                return db.Query<ChucDanh>("sp_dm_chucdanh", parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public int CapNhat(string connectionString, ChucDanh obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("cmd", "update");
                parameters.Add("id", obj.ID);
                parameters.Add("deptid", obj.DeptID);
                parameters.Add("machucdanh", obj.MaChucDanh);
                parameters.Add("chucdanh", obj.TenChucDanh);
                parameters.Add("phanloai", obj.PhanLoai);
                parameters.Add("status", obj.Status);

                return db.ExecuteScalar<int>("sp_dm_chucdanh", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
