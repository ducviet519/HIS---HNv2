using DapperExtensions.Mapper;

namespace System.App.Entities.Mapper
{
    public class TrainingCourse_Mapper : ClassMapper<TrainingCourse>
    {
        public TrainingCourse_Mapper()
        {
            Table("TrainingCourse");
            Map(x => x.EmployeeCode).Ignore();
            AutoMap();
        }
    }
}