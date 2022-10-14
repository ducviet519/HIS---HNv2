namespace System.App.Entities.HCNS
{
    public class Absent
    {
        public Absent()
        {
            IsAdmin = false;
            Status = 0;
        }

        public int KhoaPhong { get; set; }

        public string Ten_KhoaPhong_ChamCong { get; set; }

        public int UserEnrollNumber { get; set; }

        public string UserFullCode { get; set; }

        public string UserFullName { get; set; }

        public string TimeDate { get; set; }

        public string AbsentCode { get; set; }

        public string AbsentDescription { get; set; }

        public int Workingday { get; set; }

        public int WorkingTime { get; set; }

        public string AddedTime { get; set; }

        public int Thang { get; set; }

        public int Nam { get; set; }

        public int[] Ngay { get; set; }

        public string Lydo { get; set; }

        public string UserAccount { get; set; }

        public bool IsAdmin { get; set; }

        public string ID { get; set; }

        public string ParentID { get; set; }

        public bool Used { get; set; }

        //sử dụng trong trường hợp ngày khai báo lớn hơn ngày xin nghỉ thực tế
        public bool Warning { get; set; }
        public int DiffDate { get; set; }
        //phục vụ tìm kiếm
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string process { get; set; }
        public string oldDate { get; set; }
        //
        public string UserInDepts { get; set; }
        public string LateDateDiff { get; set; }
        public int Status { get; set; }
        public int ForType { get; set; }
        public string UserReason { get; set; }
        public string CheckDateString { get; set; }
    }

    public class AbsentR
    {
        public AbsentR()
        {
            MaKP = 0;
        }
        public int UserEnrollNumber { get; set; }
        public string UserFullCode { get; set; }
        public string UserFullName { get; set; }
        public int MaKP { get; set; }
        public string KhoaPhong { get; set; }
        public string Date { get; set; }
        public string AbsentCode { get; set; }
        public string AbsentDescription { get; set; }
        public string Reason { get; set; }
        public int Status { get; set; }
        public string CreateAcc { get; set; }
        public string UpdateAcc { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public string From { get; set; }
        public string To { get; set; }
        public Absent AbsentInfo { get; set; }
    }

    public class KhaiBaoAn
    {
        public string UserEnrollNumber { get; set; }
        public string Date { get; set; }
        public string Loai { get; set; }
    }
}