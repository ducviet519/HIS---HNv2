using System.App.Entities;
using System.App.Entities.Common;
using System.App.Repositories;
using System.App.Services.Interfaces;
using System.Collections.Generic;
using System.Data;

namespace System.App.Services
{
    public class TrainingCourse_Service : TrainingCourse_Interface
    {
        private readonly TrainingCourse_Repo trainingCourse_Repo;
        private readonly Department_Repo department_Repo;

        public TrainingCourse_Service()
        {
            trainingCourse_Repo = new TrainingCourse_Repo();
            department_Repo = new Department_Repo();
        }
        public IEnumerable<TrainingCourse> List()
        {
            return trainingCourse_Repo.List(StaticParams.connectionStringWiseEyeWebOn);
        }
        public int Insert(TrainingCourse obj)
        {
            return trainingCourse_Repo.Insert(StaticParams.connectionStringWiseEyeWebOn, obj);
        }

        public bool Insert(List<TrainingCourse> lstObj)
        {
            return trainingCourse_Repo.InsertList(StaticParams.connectionStringWiseEyeWebOn, lstObj);
        }
        public IEnumerable<Employee> ListEmployeeNotInTrainingCourse(int id)
        {
            return trainingCourse_Repo.ListEmployeeNotInTrainingCourse(StaticParams.connectionStringWiseEyeWebOn, id);
        }
        public IEnumerable<Employee> ListEmployeeInTrainingCourse(int id)
        {
            return trainingCourse_Repo.ListEmployeeInTrainingCourse(StaticParams.connectionStringWiseEyeWebOn, id);
        }

        public bool UpdateClone(EmployeeInTraining employeeInTraining)
        {
            try
            {
                if (trainingCourse_Repo.UpdateClone(StaticParams.connectionStringWiseEyeWebOn, employeeInTraining))
                {
                    trainingCourse_Repo.CounterEmployee(StaticParams.connectionStringWiseEyeWebOn, employeeInTraining.TC_ID);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateDestroy(EmployeeInTraining employeeInTraining)
        {
            try
            {
                if (trainingCourse_Repo.UpdateDestroy(StaticParams.connectionStringWiseEyeWebOn, employeeInTraining))
                {
                    trainingCourse_Repo.CounterEmployee(StaticParams.connectionStringWiseEyeWebOn, employeeInTraining.TC_ID);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public TrainingCourse Find(int id)
        {
            try
            {
                return trainingCourse_Repo.Find(StaticParams.connectionStringWiseEyeWebOn, id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Update(TrainingCourse trainingCourse)
        {
            try
            {
                if (trainingCourse_Repo.Update(StaticParams.connectionStringWiseEyeWebOn, trainingCourse))
                {
                    trainingCourse_Repo.CounterEmployee(StaticParams.connectionStringWiseEyeWebOn, trainingCourse.ID);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataTable ExportExcel(int courseID)
        {
            return trainingCourse_Repo.ExportExcel(StaticParams.connectionStringWiseEyeWebOn, courseID);
        }

        public bool UpdateSignature(int empID, int courseID, string chuky)
        {
            return trainingCourse_Repo.UpdateSignature(empID, courseID, chuky, StaticParams.connectionStringWiseEyeWebOn);
        }

        public Dictionary<int, string> ListDepartment()
        {
            Dictionary<int, string> listDepartment = new Dictionary<int, string>();

            var models = department_Repo.ListDepartment(StaticParams.connectionStringWiseEyeWebOn);

            foreach (var o in models)
            {
                listDepartment.Add(o.STT, o.KhoaP);
            }

            return listDepartment;
        }

        public IEnumerable<Employee> ListEmployeeNotInTrainingCourse(int id, int dept)
        {
            return trainingCourse_Repo.ListEmployeeNotInTrainingCourse(StaticParams.connectionStringWiseEyeWebOn, id, dept);
        }

        public bool CheckExists(string username, int courseID)
        {
            return trainingCourse_Repo.CheckExist(StaticParams.connectionStringWiseEyeWebOn, username, courseID) == 0 ? false : true;
        }

        public bool ImportExcel(DataTable dataTable)
        {
            try
            {
                List<TrainingCourse> lst = new List<TrainingCourse>();

                foreach (DataRow row in dataTable.Rows)
                {
                    if (!String.IsNullOrEmpty(row["TENKHOADAOTAO"].ToString()))
                    {
                        var trainingCourse = new TrainingCourse()
                        {
                            Type = "1",
                            Name = row["TENKHOADAOTAO"].ToString(),
                            Quality = int.Parse(row["SOLUONGHV"].ToString()),
                            DateFrom = ((DateTime)row["TU"]),
                            DateTo = ((DateTime)row["DEN"]),
                            Place = row["DIACHI"].ToString(),
                            Objects = row["DOITUONG"].ToString(),
                            Cost = row["CHIPHI"].ToString() == "Miễn phí" ? 0 : float.Parse(row["CHIPHI"].ToString()),
                            Lessions = int.Parse(row["TIETHOC"].ToString()),
                            Note = row["VANBAN"].ToString(),
                            EmployeeCode = row["MANV"].ToString()
                        };

                        lst.Add(trainingCourse);
                    }
                }

                return trainingCourse_Repo.ImportExcel(StaticParams.connectionStringWiseEyeWebOn, lst);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Employee> ListEmployee(int dept = 0)
        {
            return trainingCourse_Repo.ListEmployee(StaticParams.connectionStringWiseEyeWebOn, dept);
        }

        public IEnumerable<TrainingCourse> TrainingCourseWithEmpID(int empId)
        {
            return trainingCourse_Repo.TrainingCourseWithEmpID(StaticParams.connectionStringWiseEyeWebOn, empId);
        }

        public DataTable ExportTrainingCourseWithEmpID(int empId)
        {
            return trainingCourse_Repo.ExportTrainingCourseWithEmpID(StaticParams.connectionStringWiseEyeWebOn, empId);
        }
    }
}