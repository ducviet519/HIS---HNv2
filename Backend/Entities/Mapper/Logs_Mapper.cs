using DapperExtensions.Mapper;

namespace System.App.Entities.Mapper
{
    public class Logs_Mapper : ClassMapper<Logs>
    {
        public Logs_Mapper()
        {
            Table("Logs");
            AutoMap();
        }
    }
}