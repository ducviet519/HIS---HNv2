using System.App.Entities;
using System.App.Entities.HCNS;
using System.Collections.Generic;
using System.Data;

namespace System.App.Services.Interfaces
{
    public interface DoiChieuSuatAn_Interface
    {
        Dictionary<int, string> ListDepartment();

        IEnumerable<DoiChieuSuatAn> DanhSachSuatAn(string dept, string tungay, string dennga);

        DataTable DanhSachSuatAnExcel(string dept, string tungay, string denngay);
        DataTable DanhSachThongKe(string dept, string tungay, string denngay, string thoidiem);
        DataTable DanhSachThongKe_Excel(string dept, string tungay, string denngay, string thoidiem);
        bool PushData(AbsentR obj, ref string errorMessage);
    }
}