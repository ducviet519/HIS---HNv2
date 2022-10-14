using System.App.Entities;
using System.Collections.Generic;
using System.Data;

namespace System.App.Services.Interfaces
{
    public interface TrainingCourse_Interface
    {
        IEnumerable<TrainingCourse> List();

        TrainingCourse Find(int id);

        bool Update(TrainingCourse trainingCourse);

        int Insert(TrainingCourse obj);

        bool Insert(List<TrainingCourse> lstObj);

        IEnumerable<Employee> ListEmployeeNotInTrainingCourse(int id);

        IEnumerable<Employee> ListEmployeeNotInTrainingCourse(int id, int dept);

        IEnumerable<Employee> ListEmployeeInTrainingCourse(int id);

        bool UpdateClone(EmployeeInTraining employeeInTraining);

        bool UpdateDestroy(EmployeeInTraining employeeInTraining);

        DataTable ExportExcel(int courseID);

        bool UpdateSignature(int empID, int courseID, string chuky);

        Dictionary<int, string> ListDepartment();

        bool CheckExists(string username, int courseID);

        bool ImportExcel(DataTable dataTable);

        IEnumerable<Employee> ListEmployee(int dept = 0);

        IEnumerable<TrainingCourse> TrainingCourseWithEmpID(int empId);

        DataTable ExportTrainingCourseWithEmpID(int empId);
    }
}