using System.App.Entities;
using System.Collections.Generic;

namespace System.App.Services.Interfaces
{
    public interface Employee_Interface
    {
        IEnumerable<EmployeeHealth> SucKhoeNhanVien(string time);

        Employee Find(int id);
    }
}