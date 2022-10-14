using DapperExtensions.Mapper;

namespace System.App.Entities.Mapper
{
    public class Department_Mapper : ClassMapper<Department>
    {
        public Department_Mapper()
        {
            Table("Depts");
            AutoMap();
        }
    }
}