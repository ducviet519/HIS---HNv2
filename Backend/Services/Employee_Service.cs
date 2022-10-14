using System.App.Entities;
using System.App.Entities.Common;
using System.App.Repositories;
using System.App.Services.Interfaces;
using System.Collections.Generic;

namespace System.App.Services
{
    public class Employee_Service : Employee_Interface
    {
        private readonly Employee_Repo employeeRepo;

        public Employee_Service(Employee_Repo employee)
        {
            employeeRepo = employee;
        }

        public Employee Find(int id)
        {
            return employeeRepo.Find(StaticParams.connectionStringWiseEyeWebOn, id);
        }

        public IEnumerable<EmployeeHealth> SucKhoeNhanVien(string time)
        {
            return null;
            //return employeeRepo.SucKhoeNhanVien(StaticParams.connectionOracle, time);
        }
    }
}