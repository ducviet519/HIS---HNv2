using System.App.Entities;
using System.App.Entities.Common;
using System.App.Entities.HCNS;
using System.App.Repositories;
using System.App.Services.Interfaces;
using System.Collections.Generic;
using System.Data;

namespace System.App.Services
{
    public class DoiChieuSuatAn_Service : DoiChieuSuatAn_Interface
    {
        private readonly DoiChieuSuatAn_Repo _doiChieuSuatAn;

        public DoiChieuSuatAn_Service()
        {
            _doiChieuSuatAn = new DoiChieuSuatAn_Repo();
        }

        public IEnumerable<DoiChieuSuatAn> DanhSachSuatAn(string dept, string tungay, string denngay)
        {
            return _doiChieuSuatAn.DanhSachSuatAn(StaticParams.connectionStringWiseEyeWebOn, dept, tungay, denngay);
        }

        public DataTable DanhSachSuatAnExcel(string dept, string tungay, string denngay)
        {
            return _doiChieuSuatAn.DanhSachSuatAnExcel(StaticParams.connectionStringWiseEyeWebOn, dept, tungay, denngay);
        }

        public DataTable DanhSachThongKe(string dept, string tungay, string denngay, string thoidiem)
        {
            return _doiChieuSuatAn.DanhSachThongKe(StaticParams.connectionStringWiseEyeWebOn, dept, tungay, denngay, thoidiem);
        }

        public DataTable DanhSachThongKe_Excel(string dept, string tungay, string denngay, string thoidiem)
        {
            return _doiChieuSuatAn.DanhSachThongKe_Excel(StaticParams.connectionStringWiseEyeWebOn, dept, tungay, denngay, thoidiem);
        }

        public Dictionary<int, string> ListDepartment()
        {
            Dictionary<int, string> listDepartment = new Dictionary<int, string>();

            var models = _doiChieuSuatAn.ListDepartment(StaticParams.connectionStringWiseEyeWebOn);

            foreach (var o in models)
            {
                listDepartment.Add(o.STT, o.KhoaP);
            }

            return listDepartment;
        }
        public bool PushData(AbsentR obj, ref string errorMessage)
        {
            try
            {
                return _doiChieuSuatAn.PushData(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}