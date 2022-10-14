using DapperExtensions.Mapper;

namespace System.App.Entities.Mapper
{
    public class EmployeeInTraining_Mapper : ClassMapper<EmployeeInTraining>
    {
        public EmployeeInTraining_Mapper()
        {
            Table("EmployeeInTraining");
            AutoMap();
        }
    }
}