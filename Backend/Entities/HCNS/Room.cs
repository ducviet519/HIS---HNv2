namespace System.App.Entities.HCNS
{
    public class Room
    {
        public Room()
        {
            Department_ID = -1;
        }
        public string ID { get; set; }
        public int RoomType_ID { get; set; }
        public string DateReg { get; set; }
        public int Department_ID { get; set; }
        public string StartTime { get; set; }
        public DateTime? FStartTime { get; set; }
        public string EndTime { get; set; }
        public DateTime? FEndTime { get; set; }
        public string PurposeUsed { get; set; }
        public string UserReg { get; set; }
        public string Accessories { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public int Approve { get; set; }
        public int Level { get; set; }
        public int IT { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        //====================================
        public string DepartmentName { get; set; }
        public string UserFullName { get; set; }
        public string RoomTypeName { get; set; }
        public string DateCreatedView { get; set; }
        public string DateUpdatedView { get; set; }
    }

    public class RoomSearch
    {
        public string DateRegF { get; set; }
        public string DateRegT { get; set; }
        public int KhoaPhong { get; set; }
        public int RoomType { get; set; }
    }

    public class RoomType
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
    }
    public class RoomAccessory
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
    }
}
