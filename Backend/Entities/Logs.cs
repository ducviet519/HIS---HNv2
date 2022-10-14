namespace System.App.Entities
{
    public class Logs
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string Action { get; set; }

        public string Controller { get; set; }

        public string Data { get; set; }

        public string Message { get; set; }

        public DateTime DateCreated { get; set; }

        public string IP { get; set; }

        public string CreatedBy { get; set; }
    }
}