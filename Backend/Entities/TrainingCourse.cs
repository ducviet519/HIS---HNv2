namespace System.App.Entities
{
    public class TrainingCourse
    {
        public int ID { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public int Lessions { get; set; }

        public string Place { get; set; }

        public string Objects { get; set; }

        public int Quality { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public float Cost { get; set; }

        public string Note { get; set; }

        public bool Status { get; set; }

        public string EmployeeCode { get; set; }
    }
}