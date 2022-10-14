namespace System.App.Entities.HCNS
{
    public class ZK_Person
    {
        public ZK_Person()
        {
        }

        public int UserEnrollNumber { get; set; }

        public string UserFullCode { get; set; }

        public string TATitle { get; set; }

        public string UserFullName { get; set; }

        public string NgaySinh { get; set; }

        public string ChucDanh { get; set; }

        public string GioiTinh { get; set; }

        public bool VanTay { get; set; }
        public bool Face { get; set; }

        public string UserPW { get; set; }

        public string DisplayName
        {
            get { return TATitle + " " + UserFullName; }
        }
    }

    public class ZK_Person_Finger
    {
        public ZK_Person_Finger()
        {
            Flag = 0;
        }

        public int UserEnrollNumber { get; set; }

        public string UserFullName { get; set; }

        public int FingerIndex { get; set; }

        public string DataFinger { get; set; }

        public int DataLength { get; set; }

        public int Flag { get; set; }
    }

    public class ZK_Device_Info
    {
        public string IP { get; set; }

        public int AdminCount { get; set; }

        public int UserCount { get; set; }

        public int FingerCount { get; set; }

        public int RecordCount { get; set; }

        public int PasswordCount { get; set; }

        public int OptionCount { get; set; }

        public int FaceCount { get; set; }
        public int TotalUser { get; set; }
        public int TotalFinger { get; set; }
        public int TotalAtt { get; set; }
    }
}