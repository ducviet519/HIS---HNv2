using Dapper;
using System.App.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace System.App.Repositories
{
    public class DanhMuc_Repo
    {
        public IEnumerable<DanhMuc_LoaiVang> DS_LoaiVang_Search(string connectionString, DanhMuc_LoaiVang_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.AbsentCode ?? "-1");
                parameters.Add("param_Condition2", obj.AbsentDescription ?? "-1");
                parameters.Add("param_Condition3", obj.AbsentSymbol ?? "-1");
                parameters.Add("param_Type", 1);

                return db.Query<DanhMuc_LoaiVang>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int AddNew_LoaiVang(string connectionString, DanhMuc_LoaiVang_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.AbsentCode);
                parameters.Add("param_Condition2", obj.AbsentDescription);
                parameters.Add("param_Condition3", obj.AbsentSymbol);
                parameters.Add("param_Condition4", obj.IsYes);
                parameters.Add("param_Condition5", obj.IsCount);
                parameters.Add("param_Condition6", obj.Score);
                parameters.Add("param_Type", 2);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int Delete_LoaiVang(string connectionString, DanhMuc_LoaiVang_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.AbsentCode);
                parameters.Add("param_Type", 3);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public DanhMuc_LoaiVang Get_LoaiVang(string connectionString, DanhMuc_LoaiVang_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.AbsentCode);
                parameters.Add("param_Type", 4);

                return db.Query<DanhMuc_LoaiVang>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        public int Edit_LoaiVang(string connectionString, DanhMuc_LoaiVang_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.AbsentCode);
                parameters.Add("param_Condition2", obj.AbsentDescription);
                parameters.Add("param_Condition3", obj.AbsentSymbol);
                parameters.Add("param_Condition4", obj.IsYes);
                parameters.Add("param_Condition5", obj.IsCount);
                parameters.Add("param_Condition6", obj.Score);
                parameters.Add("param_Type", 5);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DanhMuc_NganHang> DS_NganHang_Search(string connectionString, DanhMuc_NganHang_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.BankName ?? "-1");
                parameters.Add("param_Type", 6);

                return db.Query<DanhMuc_NganHang>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int Delete_NganHang(string connectionString, DanhMuc_NganHang_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 7);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int AddNew_NganHang(string connectionString, DanhMuc_NganHang_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.BankName);
                parameters.Add("param_Type", 8);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DanhMuc_HopDong> DS_HopDong_Search(string connectionString, DanhMuc_HopDong_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.ConName ?? "-1");
                parameters.Add("param_Condition3", obj.ConSym ?? "-1");
                parameters.Add("param_Type", 9);

                return db.Query<DanhMuc_HopDong>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int Delete_HopDong(string connectionString, DanhMuc_HopDong_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 10);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int AddNew_HopDong(string connectionString, DanhMuc_HopDong_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.ConName);
                parameters.Add("param_Condition3", obj.ConSym);
                parameters.Add("param_Type", 11);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public DanhMuc_HopDong Get_HopDong(string connectionString, DanhMuc_HopDong_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 12);

                return db.Query<DanhMuc_HopDong>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        public int Edit_HopDong(string connectionString, DanhMuc_HopDong_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Condition2", obj.ConName);
                parameters.Add("param_Condition3", obj.ConSym);
                parameters.Add("param_Type", 13);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DanhMuc_KhoaPhong> DS_KhoaPhong_Search(string connectionString, DanhMuc_KhoaPhong_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.KhoaP ?? "-1");
                parameters.Add("param_Type", 14);

                return db.Query<DanhMuc_KhoaPhong>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int Delete_KhoaPhong(string connectionString, DanhMuc_KhoaPhong_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.STT);
                parameters.Add("param_Type", 15);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int AddNew_KhoaPhong(string connectionString, DanhMuc_KhoaPhong_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.KhoaP);
                parameters.Add("param_Condition3", obj.Object);
                parameters.Add("param_Condition4", obj.Code);
                parameters.Add("param_Type", 16);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DanhMuc_ThanhPho> DS_ThanhPho_Search(string connectionString, DanhMuc_ThanhPho_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.TownName ?? "-1");
                parameters.Add("param_Condition7", obj.TownShip ?? "-1");
                parameters.Add("param_Type", 17);

                return db.Query<DanhMuc_ThanhPho>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int AddNew_ThanhPho(string connectionString, DanhMuc_ThanhPho_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.TownName);
                parameters.Add("param_Condition7", obj.TownShip);
                parameters.Add("param_Condition8", obj.Pupulation);
                parameters.Add("param_Condition3", obj.Area);
                parameters.Add("param_Type", 18);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int Delete_ThanhPho(string connectionString, DanhMuc_ThanhPho_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 19);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public DanhMuc_ThanhPho Get_ThanhPho(string connectionString, DanhMuc_ThanhPho_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 20);

                return db.Query<DanhMuc_ThanhPho>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        public int Edit_ThanhPho(string connectionString, DanhMuc_ThanhPho_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Condition2", obj.TownName);
                parameters.Add("param_Condition7", obj.TownShip);
                parameters.Add("param_Condition8", obj.Pupulation);
                parameters.Add("param_Condition3", obj.Area);
                parameters.Add("param_Type", 21);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DanhMuc_QuanHuyen> DS_QuanHuyen_Search(string connectionString, DanhMuc_QuanHuyen_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.Name ?? "-1");
                parameters.Add("param_Condition7", obj.DLevel ?? "-1");
                parameters.Add("param_Condition4", obj.City ?? "-1");
                parameters.Add("param_Type", 22);

                return db.Query<DanhMuc_QuanHuyen>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int AddNew_QuanHuyen(string connectionString, DanhMuc_QuanHuyen_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.Name);
                parameters.Add("param_Condition7", obj.DLevel);
                parameters.Add("param_Condition4", obj.City);
                parameters.Add("param_Type", 23);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int Delete_QuanHuyen(string connectionString, DanhMuc_QuanHuyen_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 24);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public DanhMuc_QuanHuyen Get_QuanHuyen(string connectionString, DanhMuc_QuanHuyen_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 25);

                return db.Query<DanhMuc_QuanHuyen>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        public int Edit_QuanHuyen(string connectionString, DanhMuc_QuanHuyen_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Condition2", obj.Name);
                parameters.Add("param_Condition7", obj.DLevel);
                parameters.Add("param_Condition4", obj.City);
                parameters.Add("param_Type", 26);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DS_DropDownList> DS_ThanhPho(string connectionString)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Type", 27);

                return db.Query<DS_DropDownList>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public IEnumerable<DanhMuc_BenhVien> DS_BenhVien_Search(string connectionString, DanhMuc_BenhVien_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.HospitalName ?? "-1");
                parameters.Add("param_Type", 28);

                return db.Query<DanhMuc_BenhVien>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int Delete_BenhVien(string connectionString, DanhMuc_BenhVien_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 29);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int AddNew_BenhVien(string connectionString, DanhMuc_BenhVien_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.HospitalName);
                parameters.Add("param_Type", 30);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DanhMuc_QuocGia> DS_QuocGia_Search(string connectionString, DanhMuc_QuocGia_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.Nationality ?? "-1");
                parameters.Add("param_Type", 31);

                return db.Query<DanhMuc_QuocGia>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int Delete_QuocGia(string connectionString, DanhMuc_QuocGia_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 32);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int AddNew_QuocGia(string connectionString, DanhMuc_QuocGia_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.Nationality);
                parameters.Add("param_Type", 33);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DanhMuc_NoiCapCMT> DS_NoiCapCMT_Search(string connectionString, DanhMuc_NoiCapCMT_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.NoiCap ?? "-1");
                parameters.Add("param_Type", 34);

                return db.Query<DanhMuc_NoiCapCMT>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int Delete_NoiCapCMT(string connectionString, DanhMuc_NoiCapCMT_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 35);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int AddNew_NoiCapCMT(string connectionString, DanhMuc_NoiCapCMT_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.NoiCap);
                parameters.Add("param_Type", 36);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DanhMuc_DanToc> DS_DanToc_Search(string connectionString, DanhMuc_DanToc_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.PeopleName ?? "-1");
                parameters.Add("param_Condition7", obj.PeopleOName ?? "-1");
                parameters.Add("param_Condition9", obj.PeoplePlace ?? "-1");
                parameters.Add("param_Type", 37);

                return db.Query<DanhMuc_DanToc>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int Delete_DanToc(string connectionString, DanhMuc_DanToc_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 38);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int AddNew_DanToc(string connectionString, DanhMuc_DanToc_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.PeopleName ?? "-1");
                parameters.Add("param_Condition7", obj.PeopleOName ?? "-1");
                parameters.Add("param_Condition9", obj.PeoplePlace ?? "-1");
                parameters.Add("param_Type", 39);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public DanhMuc_DanToc Get_DanToc(string connectionString, DanhMuc_DanToc_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 40);

                return db.Query<DanhMuc_DanToc>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        public int Edit_DanToc(string connectionString, DanhMuc_DanToc_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Condition2", obj.PeopleName ?? "-1");
                parameters.Add("param_Condition7", obj.PeopleOName ?? "-1");
                parameters.Add("param_Condition9", obj.PeoplePlace ?? "-1");
                parameters.Add("param_Type", 41);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DanhMuc_TonGiao> DS_TonGiao_Search(string connectionString, DanhMuc_TonGiao_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.Religion ?? "-1");
                parameters.Add("param_Type", 42);

                return db.Query<DanhMuc_TonGiao>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int Delete_TonGiao(string connectionString, DanhMuc_TonGiao_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 43);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int AddNew_TonGiao(string connectionString, DanhMuc_TonGiao_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.Religion);
                parameters.Add("param_Type", 44);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DanhMuc_PhongHop> DS_PhongHop_Search(string connectionString, DanhMuc_PhongHop_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.Name ?? "-1");
                parameters.Add("param_Condition3", obj.Code ?? "-1");
                parameters.Add("param_Type", 45);

                return db.Query<DanhMuc_PhongHop>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int AddNew_PhongHop(string connectionString, DanhMuc_PhongHop_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.Name);
                parameters.Add("param_Condition3", obj.Code);
                parameters.Add("param_Type", 46);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int Delete_PhongHop(string connectionString, DanhMuc_PhongHop_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 47);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public DanhMuc_PhongHop Get_PhongHop(string connectionString, DanhMuc_PhongHop_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 48);

                return db.Query<DanhMuc_PhongHop>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        public int Edit_PhongHop(string connectionString, DanhMuc_PhongHop_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.Name);
                parameters.Add("param_Condition3", obj.Code);
                parameters.Add("param_Type", 49);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int UnDelete_PhongHop(string connectionString, DanhMuc_PhongHop_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 50);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DanhMuc_KhoaPhongCC> DS_KhoaPhongCC_Search(string connectionString, DanhMuc_KhoaPhongCC_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.Description ?? "-1");
                parameters.Add("param_Type", 51);

                return db.Query<DanhMuc_KhoaPhongCC>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int AddNew_KhoaPhongCC(string connectionString, DanhMuc_KhoaPhongCC_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.Description);
                parameters.Add("param_Condition3", obj.RelationID);
                parameters.Add("param_Condition4", obj.LevelID);
                parameters.Add("param_Condition5", DateTime.ParseExact(obj.UngPhepMin, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"));
                parameters.Add("param_Condition6", DateTime.ParseExact(obj.UngPhepMax, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"));
                parameters.Add("param_Condition7", obj.DeptCode);
                parameters.Add("param_Type", 52);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int Delete_KhoaPhongCC(string connectionString, DanhMuc_KhoaPhongCC_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 53);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public DanhMuc_KhoaPhongCC Get_KhoaPhongCC(string connectionString, DanhMuc_KhoaPhongCC_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 54);

                return db.Query<DanhMuc_KhoaPhongCC>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        public int Edit_KhoaPhongCC(string connectionString, DanhMuc_KhoaPhongCC_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Condition2", obj.Description);
                parameters.Add("param_Condition3", obj.RelationID);
                parameters.Add("param_Condition5", DateTime.ParseExact(obj.UngPhepMin, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"));
                parameters.Add("param_Condition6", DateTime.ParseExact(obj.UngPhepMax, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"));
                parameters.Add("param_Type", 55);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DS_DropDownList> DS_QuanHe(string connectionString)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Type", 56);

                return db.Query<DS_DropDownList>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public IEnumerable<DanhMuc_PhuongXa> DS_PhuongXa_Search(string connectionString, DanhMuc_PhuongXa_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.Name ?? "-1");
                parameters.Add("param_Condition3", obj.Dis ?? "-1");
                parameters.Add("param_Condition4", obj.CityID ?? "-1");
                parameters.Add("param_Type", 57);

                return db.Query<DanhMuc_PhuongXa>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int AddNew_PhuongXa(string connectionString, DanhMuc_PhuongXa_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.Name);
                parameters.Add("param_Condition3", obj.Dis);
                parameters.Add("param_Condition4", obj.CityID);
                parameters.Add("param_Condition7", obj.WLevel);
                parameters.Add("param_Type", 58);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int Delete_PhuongXa(string connectionString, DanhMuc_PhuongXa_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 59);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public DanhMuc_PhuongXa Get_PhuongXa(string connectionString, DanhMuc_PhuongXa_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 60);

                return db.Query<DanhMuc_PhuongXa>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        public int Edit_PhuongXa(string connectionString, DanhMuc_PhuongXa_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Condition2", obj.Name);
                parameters.Add("param_Condition3", obj.Dis);
                parameters.Add("param_Condition4", obj.CityID);
                parameters.Add("param_Condition7", obj.WLevel);
                parameters.Add("param_Type", 61);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DS_DropDownList> DS_QuanHuyen(string connectionString)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Type", 62);

                return db.Query<DS_DropDownList>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public IEnumerable<DanhMuc_Tang> DS_Tang_Search(string connectionString, DanhMuc_Tang_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.FloorDes ?? "-1");
                parameters.Add("param_Condition3", obj.Block ?? "-1");
                parameters.Add("param_Type", 63);

                return db.Query<DanhMuc_Tang>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int Delete_Tang(string connectionString, DanhMuc_Tang_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 64);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int AddNew_Tang(string connectionString, DanhMuc_Tang_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.FloorDes ?? "-1");
                parameters.Add("param_Condition3", obj.Block ?? "-1");
                parameters.Add("param_Type", 65);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DS_DropDownList> DS_Tang(string connectionString)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Type", 66);

                return db.Query<DS_DropDownList>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public IEnumerable<DanhMuc_CaLamViec> DS_CaLamViec_Search(string connectionString, DanhMuc_CaLamViec_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.ShiftCode ?? "-1");
                parameters.Add("param_Type", 67);

                return db.Query<DanhMuc_CaLamViec>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int AddNew_CaLamViec(string connectionString, DanhMuc_CaLamViec_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.ShiftCode);
                parameters.Add("param_Condition3", obj.Onduty);
                parameters.Add("param_Condition4", obj.Offduty);
                parameters.Add("param_Condition5", obj.DayCount);
                parameters.Add("param_Condition6", obj.OnTimeIn);
                parameters.Add("param_Condition7", obj.OnLunch);
                parameters.Add("param_Condition8", obj.WKOTLevel);
                parameters.Add("param_Condition9", obj.OffLunch);
                parameters.Add("param_Condition10", obj.OnTimeOut);
                parameters.Add("param_Condition11", obj.CutIn);
                parameters.Add("param_Condition12", obj.CutOut);
                parameters.Add("param_Condition13", obj.WorkingTime);
                parameters.Add("param_Condition14", obj.Workingday);
                parameters.Add("param_Condition15", obj.IsHolidayOT);
                parameters.Add("param_Type", 68);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int Delete_CaLamViec(string connectionString, DanhMuc_CaLamViec_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ShiftID);
                parameters.Add("param_Type", 69);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public DanhMuc_CaLamViec Get_CaLamViec(string connectionString, DanhMuc_CaLamViec_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ShiftID);
                parameters.Add("param_Type", 70);

                return db.Query<DanhMuc_CaLamViec>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        public int Edit_CaLamViec(string connectionString, DanhMuc_CaLamViec_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ShiftID);
                parameters.Add("param_Condition2", obj.ShiftCode);
                parameters.Add("param_Condition3", obj.Onduty);
                parameters.Add("param_Condition4", obj.Offduty);
                parameters.Add("param_Condition5", obj.DayCount);
                parameters.Add("param_Condition6", obj.OnTimeIn);
                parameters.Add("param_Condition7", obj.OnLunch);
                parameters.Add("param_Condition8", obj.WKOTLevel);
                parameters.Add("param_Condition9", obj.OffLunch);
                parameters.Add("param_Condition10", obj.OnTimeOut);
                parameters.Add("param_Condition11", obj.CutIn);
                parameters.Add("param_Condition12", obj.CutOut);
                parameters.Add("param_Condition13", obj.WorkingTime);
                parameters.Add("param_Condition14", obj.Workingday);
                parameters.Add("param_Condition15", obj.IsHolidayOT);
                parameters.Add("param_Type", 71);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DanhMuc_LichLamViec> DS_LichLamViec_Search(string connectionString, DanhMuc_LichLamViec_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.SchName ?? "-1");
                parameters.Add("param_Type", 72);

                return db.Query<DanhMuc_LichLamViec>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int AddNew_LichLamViec(string connectionString, DanhMuc_LichLamViec_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.SchName);
                parameters.Add("param_Condition3", obj.InOutID);
                parameters.Add("param_Type", 73);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int Delete_LichLamViec(string connectionString, DanhMuc_LichLamViec_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.SchID);
                parameters.Add("param_Type", 74);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public DanhMuc_LichLamViec Get_LichLamViec(string connectionString, DanhMuc_LichLamViec_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.SchID);
                parameters.Add("param_Type", 75);

                return db.Query<DanhMuc_LichLamViec>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        public int Edit_LichLamViec(string connectionString, DanhMuc_LichLamViec_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.SchID);
                parameters.Add("param_Condition2", obj.SchName);
                parameters.Add("param_Condition3", obj.InOutID);
                parameters.Add("param_Type", 76);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DS_DropDownList> DS_InOutID(string connectionString)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Type", 77);

                return db.Query<DS_DropDownList>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public IEnumerable<DanhMuc_LichTrinh> DS_LichTrinh_Search(string connectionString, DanhMuc_LichTrinh_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition4", obj.SchID ?? "-1");
                parameters.Add("param_Condition5", obj.ShiftID ?? "-1");
                parameters.Add("param_Type", 78);

                return db.Query<DanhMuc_LichTrinh>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int AddNew_LichTrinh(string connectionString, DanhMuc_LichTrinh_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition4", obj.SchID);
                parameters.Add("param_Condition5", obj.ShiftID1);
                parameters.Add("param_Condition6", obj.ShiftID2);
                parameters.Add("param_Condition10", obj.ShiftID3);
                parameters.Add("param_Condition11", obj.ShiftID4);
                parameters.Add("param_Condition12", obj.ShiftID5);
                parameters.Add("param_Condition13", obj.ShiftID6);
                parameters.Add("param_Condition14", obj.ShiftID7);
                parameters.Add("param_Type", 79);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int Delete_LichTrinh(string connectionString, DanhMuc_LichTrinh_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 80);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public DanhMuc_LichTrinh Get_LichTrinh(string connectionString, DanhMuc_LichTrinh_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 81);

                return db.Query<DanhMuc_LichTrinh>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        public int Edit_LichTrinh(string connectionString, DanhMuc_LichTrinh_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Condition5", obj.ShiftID);
                parameters.Add("param_Type", 82);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DS_DropDownList> DS_Schedule(string connectionString)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Type", 83);

                return db.Query<DS_DropDownList>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public IEnumerable<DS_DropDownList> DS_Shift(string connectionString)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Type", 84);

                return db.Query<DS_DropDownList>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public IEnumerable<DanhMuc_NoiAn> DS_NoiAn_Search(string connectionString, DanhMuc_NoiAn_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.Place ?? "-1");
                parameters.Add("param_Type", 85);

                return db.Query<DanhMuc_NoiAn>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int Delete_NoiAn(string connectionString, DanhMuc_NoiAn_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 86);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int AddNew_NoiAn(string connectionString, DanhMuc_NoiAn_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.Place ?? "-1");
                parameters.Add("param_Type", 87);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DanhMuc_NgayNghi> DS_NgayNghi_Search(string connectionString, DanhMuc_NgayNghi_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.Holiday ?? "-1");
                parameters.Add("param_Type", 88);

                return db.Query<DanhMuc_NgayNghi>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int Delete_NgayNghi(string connectionString, DanhMuc_NgayNghi_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 89);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int AddNew_NgayNghi(string connectionString, DanhMuc_NgayNghi_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.Holiday ?? "-1");
                parameters.Add("param_Condition3", DateTime.ParseExact(obj.HDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"));
                parameters.Add("param_Condition4", String.IsNullOrEmpty(obj.NgayBu) ? "" : DateTime.ParseExact(obj.NgayBu, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"));
                parameters.Add("param_Type", 90);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DanhMuc_LoaiOT> DS_LoaiOT_Search(string connectionString, DanhMuc_LoaiOT_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.Des ?? "-1");
                parameters.Add("param_Type", 91);

                return db.Query<DanhMuc_LoaiOT>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int Delete_LoaiOT(string connectionString, DanhMuc_LoaiOT_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 92);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int AddNew_LoaiOT(string connectionString, DanhMuc_LoaiOT_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.Des ?? "-1");
                parameters.Add("param_Condition3", obj.Code ?? "-1");
                parameters.Add("param_Type", 93);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DanhMuc_KyNang> DS_KyNang_Search(string connectionString, DanhMuc_KyNang_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.SkillName ?? "-1");
                parameters.Add("param_Type", 94);

                return db.Query<DanhMuc_KyNang>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int Delete_KyNang(string connectionString, DanhMuc_KyNang_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 95);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int AddNew_KyNang(string connectionString, DanhMuc_KyNang_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.SkillName ?? "-1");
                parameters.Add("param_Type", 96);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DanhMuc_LoaiYeuCauSuaCong> DS_LoaiYeuCauSuaCong_Search(string connectionString, DanhMuc_LoaiYeuCauSuaCong_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.Des ?? "-1");
                parameters.Add("param_Type", 97);

                return db.Query<DanhMuc_LoaiYeuCauSuaCong>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int Delete_LoaiYeuCauSuaCong(string connectionString, DanhMuc_LoaiYeuCauSuaCong_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 98);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int AddNew_LoaiYeuCauSuaCong(string connectionString, DanhMuc_LoaiYeuCauSuaCong_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.Des ?? "-1");
                parameters.Add("param_Type", 99);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DanhMuc_DoiTuongCC> DS_DoiTuongCC_Search(string connectionString, DanhMuc_DoiTuongCC_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.TitleName ?? "-1");
                parameters.Add("param_Condition3", obj.TitleCode ?? "-1");
                parameters.Add("param_Type", 100);

                return db.Query<DanhMuc_DoiTuongCC>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int AddNew_DoiTuongCC(string connectionString, DanhMuc_DoiTuongCC_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.TitleName);
                parameters.Add("param_Condition3", obj.TitleCode);
                parameters.Add("param_Type", 101);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int Delete_DoiTuongCC(string connectionString, DanhMuc_DoiTuongCC_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.IDT);
                parameters.Add("param_Type", 102);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public DanhMuc_DoiTuongCC Get_DoiTuongCC(string connectionString, DanhMuc_DoiTuongCC_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.IDT);
                parameters.Add("param_Type", 103);

                return db.Query<DanhMuc_DoiTuongCC>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        public int Edit_DoiTuongCC(string connectionString, DanhMuc_DoiTuongCC_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.IDT);
                parameters.Add("param_Condition2", obj.TitleName);
                parameters.Add("param_Condition3", obj.TitleCode);
                parameters.Add("param_Type", 104);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DanhMuc_LoaiLamThem> DS_LoaiLamThem_Search(string connectionString, DanhMuc_LoaiLamThem_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.Des ?? "-1");
                parameters.Add("param_Type", 105);

                return db.Query<DanhMuc_LoaiLamThem>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int AddNew_LoaiLamThem(string connectionString, DanhMuc_LoaiLamThem_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Condition2", obj.Des);
                parameters.Add("param_Condition3", obj.Code);
                parameters.Add("param_Type", 106);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int Delete_LoaiLamThem(string connectionString, DanhMuc_LoaiLamThem_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 107);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public DanhMuc_LoaiLamThem Get_LoaiLamThem(string connectionString, DanhMuc_LoaiLamThem_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 108);

                return db.Query<DanhMuc_LoaiLamThem>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        public int Edit_LoaiLamThem(string connectionString, DanhMuc_LoaiLamThem_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Condition2", obj.Des);
                parameters.Add("param_Condition3", obj.Code);
                parameters.Add("param_Type", 109);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DanhMuc_LoaiNhanVien> DS_LoaiNhanVien_Search(string connectionString, DanhMuc_LoaiNhanVien_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.TypeDes ?? "-1");
                parameters.Add("param_Type", 110);

                return db.Query<DanhMuc_LoaiNhanVien>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int AddNew_LoaiNhanVien(string connectionString, DanhMuc_LoaiNhanVien_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.TypeDes);
                parameters.Add("param_Type", 111);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int Delete_LoaiNhanVien(string connectionString, DanhMuc_LoaiNhanVien_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 112);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public DanhMuc_LoaiNhanVien Get_LoaiNhanVien(string connectionString, DanhMuc_LoaiNhanVien_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 113);

                return db.Query<DanhMuc_LoaiNhanVien>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        public int Edit_LoaiNhanVien(string connectionString, DanhMuc_LoaiNhanVien_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Condition2", obj.TypeDes);
                parameters.Add("param_Type", 114);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DanhMuc_LoaiLoiCC> DS_LoaiLoiCC_Search(string connectionString, DanhMuc_LoaiLoiCC_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.Des ?? "-1");
                parameters.Add("param_Type", 115);

                return db.Query<DanhMuc_LoaiLoiCC>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int AddNew_LoaiLoiCC(string connectionString, DanhMuc_LoaiLoiCC_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.Des);
                parameters.Add("param_Type", 116);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int Delete_LoaiLoiCC(string connectionString, DanhMuc_LoaiLoiCC_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 117);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public DanhMuc_LoaiLoiCC Get_LoaiLoiCC(string connectionString, DanhMuc_LoaiLoiCC_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 118);

                return db.Query<DanhMuc_LoaiLoiCC>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        public int Edit_LoaiLoiCC(string connectionString, DanhMuc_LoaiLoiCC_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Condition2", obj.Des);
                parameters.Add("param_Type", 119);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DanhMuc_BangCap> DS_BangCap_Search(string connectionString, DanhMuc_BangCap_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.RName ?? "-1");
                parameters.Add("param_Type", 120);

                return db.Query<DanhMuc_BangCap>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int Delete_BangCap(string connectionString, DanhMuc_BangCap_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.RID);
                parameters.Add("param_Type", 121);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int AddNew_BangCap(string connectionString, DanhMuc_BangCap_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.RID ?? "-1");
                parameters.Add("param_Condition2", obj.RName ?? "-1");
                parameters.Add("param_Condition3", obj.RLevel ?? "-1");
                parameters.Add("param_Type", 122);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DanhMuc_ThietBiPhongHop> DS_ThietBiPhongHop_Search(string connectionString, DanhMuc_ThietBiPhongHop_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.Name ?? "-1");
                parameters.Add("param_Type", 123);

                return db.Query<DanhMuc_ThietBiPhongHop>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int Delete_ThietBiPhongHop(string connectionString, DanhMuc_ThietBiPhongHop_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 124);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int AddNew_ThietBiPhongHop(string connectionString, DanhMuc_ThietBiPhongHop_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.Name ?? "-1");
                parameters.Add("param_Condition7", obj.Note ?? "-1");
                parameters.Add("param_Type", 125);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DanhMuc_TTHonNhan> DS_TTHonNhan_Search(string connectionString, DanhMuc_TTHonNhan_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.MaSta ?? "-1");
                parameters.Add("param_Type", 126);

                return db.Query<DanhMuc_TTHonNhan>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int Delete_TTHonNhan(string connectionString, DanhMuc_TTHonNhan_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 127);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int AddNew_TTHonNhan(string connectionString, DanhMuc_TTHonNhan_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.MaSta ?? "-1");
                parameters.Add("param_Type", 128);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DanhMuc_ThietBi> DS_ThietBi_Search(string connectionString, DanhMuc_ThietBi_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.DeviceName ?? "-1");
                parameters.Add("param_Type", 129);

                return db.Query<DanhMuc_ThietBi>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int AddNew_ThietBi(string connectionString, DanhMuc_ThietBi_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.MachineID);
                parameters.Add("param_Condition2", obj.DeviceName);
                parameters.Add("param_Condition3", obj.IPAddress);
                parameters.Add("param_Condition4", obj.Port);
                parameters.Add("param_Condition5", obj.AreaID);
                parameters.Add("param_Condition6", obj.IsChamAn);
                parameters.Add("param_Condition10", obj.PortSocket);
                parameters.Add("param_Condition11", obj.TenMayIn);
                parameters.Add("param_Type", 130);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int Delete_ThietBi(string connectionString, DanhMuc_ThietBi_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.MachineID);
                parameters.Add("param_Type", 131);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public DanhMuc_ThietBi Get_ThietBi(string connectionString, DanhMuc_ThietBi_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.MachineID);
                parameters.Add("param_Type", 132);

                return db.Query<DanhMuc_ThietBi>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        public int Edit_ThietBi(string connectionString, DanhMuc_ThietBi_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.MachineID);
                parameters.Add("param_Condition2", obj.DeviceName);
                parameters.Add("param_Condition3", obj.IPAddress);
                parameters.Add("param_Condition4", obj.Port);
                parameters.Add("param_Condition5", obj.AreaID);
                parameters.Add("param_Condition6", obj.IsChamAn);
                parameters.Add("param_Condition10", obj.PortSocket);
                parameters.Add("param_Condition11", obj.TenMayIn);
                parameters.Add("param_Type", 133);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DanhMuc_Quyen> DS_Quyen_Search(string connectionString, DanhMuc_Quyen_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.RightTA ?? "-1");
                parameters.Add("param_Condition7", obj.PartTA ?? "-1");
                parameters.Add("param_Type", 134);

                return db.Query<DanhMuc_Quyen>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int AddNew_Quyen(string connectionString, DanhMuc_Quyen_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.RightTA);
                parameters.Add("param_Condition3", obj.ValTA);
                parameters.Add("param_Condition7", obj.PartTA);
                parameters.Add("param_Type", 135);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int Delete_Quyen(string connectionString, DanhMuc_Quyen_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition3", obj.ValTA);
                parameters.Add("param_Type", 136);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public DanhMuc_Quyen Get_Quyen(string connectionString, DanhMuc_Quyen_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition3", obj.ValTA);
                parameters.Add("param_Type", 137);

                return db.Query<DanhMuc_Quyen>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        public int Edit_Quyen(string connectionString, DanhMuc_Quyen_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.RightTA);
                parameters.Add("param_Condition3", obj.ValTA);
                parameters.Add("param_Condition7", obj.PartTA);
                parameters.Add("param_Type", 138);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<DS_DropDownList> DS_Quyen(string connectionString)
        {
            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Type", 139);

                return sConnection.Query<DS_DropDownList>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DanhMuc_Nhom> DS_Nhom_Search(string connectionString, DanhMuc_Nhom_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.Object ?? "-1");
                parameters.Add("param_Type", 140);

                return db.Query<DanhMuc_Nhom>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int AddNew_Nhom(string connectionString, DanhMuc_Nhom_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.Object);
                parameters.Add("param_Condition3", obj.Roles.Replace(" ", ""));
                parameters.Add("param_Type", 141);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int Delete_Nhom(string connectionString, DanhMuc_Nhom_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.Object);
                parameters.Add("param_Type", 142);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public DanhMuc_Nhom Get_Nhom(string connectionString, DanhMuc_Nhom_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.Object);
                parameters.Add("param_Type", 143);

                return db.Query<DanhMuc_Nhom>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        public int Edit_Nhom(string connectionString, DanhMuc_Nhom_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.Object);
                parameters.Add("param_Condition3", obj.Roles.Replace(" ", ""));
                parameters.Add("param_Type", 144);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DS_DropDownList> DS_Nhom(string connectionString)
        {
            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Type", 145);

                return sConnection.Query<DS_DropDownList>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DS_DropDownList> DS_Ca(string connectionString)
        {
            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Type", 153);

                return sConnection.Query<DS_DropDownList>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DanhMuc_Quyen_NguoiDung> DS_Quyen_NguoiDung_Search(string connectionString, DanhMuc_Quyen_NguoiDung_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.Username ?? "-1");
                parameters.Add("param_Type", 146);

                return db.Query<DanhMuc_Quyen_NguoiDung>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int AddNew_Quyen_NguoiDung(string connectionString, DanhMuc_Quyen_NguoiDung_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.Username);
                parameters.Add("param_Condition3", obj.Object);
                parameters.Add("param_Type", 147);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int Delete_Quyen_NguoiDung(string connectionString, DanhMuc_Quyen_NguoiDung_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.Username);
                parameters.Add("param_Type", 148);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public DanhMuc_Quyen_NguoiDung Get_Quyen_NguoiDung(string connectionString, DanhMuc_Quyen_NguoiDung_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.Username);
                parameters.Add("param_Type", 149);

                return db.Query<DanhMuc_Quyen_NguoiDung>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        public int Edit_Quyen_NguoiDung(string connectionString, DanhMuc_Quyen_NguoiDung_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.Username);
                parameters.Add("param_Condition3", obj.Object);
                parameters.Add("param_Type", 150);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public DanhMuc_KhoaPhong Get_KhoaPhong(string connectionString, DanhMuc_KhoaPhong_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.STT);
                parameters.Add("param_Type", 151);

                return db.Query<DanhMuc_KhoaPhong>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        public int Edit_KhoaPhong(string connectionString, DanhMuc_KhoaPhong_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.STT);
                parameters.Add("param_Condition2", obj.KhoaP);
                parameters.Add("param_Condition3", obj.Object);
                parameters.Add("param_Condition4", obj.Code);
                parameters.Add("param_Type", 152);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DanhMuc_Quyen_KhoaPhong> DS_Quyen_KhoaPhong_Search(string connectionString, DanhMuc_Quyen_KhoaPhong_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.Dept ?? "-1");
                parameters.Add("param_Type", 153);

                return db.Query<DanhMuc_Quyen_KhoaPhong>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int AddNew_Quyen_KhoaPhong(string connectionString, DanhMuc_Quyen_KhoaPhong_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.Dept);
                parameters.Add("param_Condition3", obj.Object);
                parameters.Add("param_Type", 154);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int Delete_Quyen_KhoaPhong(string connectionString, DanhMuc_Quyen_KhoaPhong_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.Dept);
                parameters.Add("param_Type", 155);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public DanhMuc_Quyen_KhoaPhong Get_Quyen_KhoaPhong(string connectionString, DanhMuc_Quyen_KhoaPhong_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.Dept);
                parameters.Add("param_Type", 156);

                return db.Query<DanhMuc_Quyen_KhoaPhong>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        public int Edit_Quyen_KhoaPhong(string connectionString, DanhMuc_Quyen_KhoaPhong_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.Dept);
                parameters.Add("param_Condition3", obj.Object);
                parameters.Add("param_Type", 157);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DS_DropDownList> DS_Depts(string connectionString)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Type", 158);

                return db.Query<DS_DropDownList>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        #region Quyền chấm công
        public IEnumerable<QuyenChamCong> QuyenChamCong_table(string connectionString, QuyenChamCong obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_MaNhanVien", obj.USERENROLLNUMBER ?? "-1");
                parameters.Add("param_Type", 1);

                return db.Query<QuyenChamCong>("sp_QuyenChamCong", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int XoaQuyen(string connectionString, QuyenChamCong obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_ID", obj.ID);
                parameters.Add("param_MaNhanVien", obj.USERENROLLNUMBER);
                parameters.Add("param_TYPE", 2);
                return db.Execute("sp_QuyenChamCong", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int ThemQuyen(string connectionString, QuyenChamCong obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_ID", obj.ID);
                parameters.Add("param_MaNhanVien", obj.USERENROLLNUMBER);
                parameters.Add("param_TYPE", 3);
                return db.Execute("sp_QuyenChamCong", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<Department> DSKhoaPhong(string connectionString)
        {
            string q = "SELECT [ID] as STT, [Description] as KhoaP FROM [RelationDept] WHERE ID > 2 ORDER BY KhoaP";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var result = sqlConnection.Query<Department>(q);
                sqlConnection.Close();
                return result.ToList();
            }
        }
        #endregion
        public IEnumerable<DanhMuc_Config> DS_Config_Search(string connectionString, DanhMuc_Config_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.Parameter == null ? "" : obj.Parameter);
                parameters.Add("param_Type", 155);

                return db.Query<DanhMuc_Config>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int AddNew_Config(string connectionString, DanhMuc_Config_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition2", obj.Parameter);
                parameters.Add("param_Condition3", obj.ParameterID);
                parameters.Add("param_Condition4", obj.StringVal);
                parameters.Add("param_Condition8", obj.NumberVal);
                parameters.Add("param_Condition5", DateTime.ParseExact(obj.DatetimeVal, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"));
                parameters.Add("param_Type", 156);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int Delete_Config(string connectionString, DanhMuc_Config_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 157);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public DanhMuc_Config Get_Config(string connectionString, DanhMuc_Config_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Type", 158);

                return db.Query<DanhMuc_Config>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        public int Edit_Config(string connectionString, DanhMuc_Config_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", obj.ID);
                parameters.Add("param_Condition2", obj.Parameter);
                parameters.Add("param_Condition3", obj.ParameterID);
                parameters.Add("param_Condition4", obj.StringVal);
                parameters.Add("param_Condition8", obj.NumberVal);
                parameters.Add("param_Condition5", obj.DatetimeVal == null ? null : DateTime.ParseExact(obj.DatetimeVal, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"));
                parameters.Add("param_Type", 159);
                return db.Execute("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DS_DropDownList> DS_LoaiCa(string connectionString)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Type", 160);

                return db.Query<DS_DropDownList>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public IEnumerable<DanhMuc_ViTriTuyenDung> DS_ViTriTuyenDung_Search(string connectionString, DanhMuc_ViTriTuyenDung_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Des", obj.Description == null ? "-1" : obj.Description);
                parameters.Add("QType", 3);

                return db.Query<DanhMuc_ViTriTuyenDung>("sp_DM_ViTriTuyenDung", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int AddNew_ViTriTuyenDung(string connectionString, DanhMuc_ViTriTuyenDung_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Des", obj.Description);
                parameters.Add("QType", 1);
                return db.Execute("sp_DM_ViTriTuyenDung", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int Delete_ViTriTuyenDung(string connectionString, DanhMuc_ViTriTuyenDung_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Stats", 0);
                parameters.Add("ID", obj.ID);
                parameters.Add("QType", 2);
                return db.Execute("sp_DM_ViTriTuyenDung", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int UnDelete_ViTriTuyenDung(string connectionString, DanhMuc_ViTriTuyenDung_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Stats", 1);
                parameters.Add("ID", obj.ID);
                parameters.Add("QType", 2);
                return db.Execute("sp_DM_ViTriTuyenDung", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public DanhMuc_ViTriTuyenDung Get_ViTriTuyenDung(string connectionString, DanhMuc_ViTriTuyenDung_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("ID", obj.ID);
                parameters.Add("QType", 4);

                return db.Query<DanhMuc_ViTriTuyenDung>("sp_DM_ViTriTuyenDung", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        public int Edit_ViTriTuyenDung(string connectionString, DanhMuc_ViTriTuyenDung_Search obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("ID", obj.ID);
                parameters.Add("Des", obj.Description);
                parameters.Add("QType", 2);
                return db.Execute("sp_DM_ViTriTuyenDung", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}