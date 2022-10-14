using DapperExtensions.Mapper;

namespace System.App.Entities.Mapper
{
    public class Employee_Mapper : ClassMapper<Employee>
    {
        public Employee_Mapper()
        {
            Table("UserInfExt");
            Map(x => x.CHUKY).Ignore();
            Map(x => x.NgayKy).Ignore();
            Map(x => x.Total_Course).Ignore();
            AutoMap();
        }
    }
}