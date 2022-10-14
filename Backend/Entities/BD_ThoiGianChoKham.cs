using DapperExtensions.Mapper;

namespace System.App.Entities
{
    public class BD_ThoiGianChoKham_Mapping : ClassMapper<BD_ThoiGianChoKham>
    {
        public BD_ThoiGianChoKham_Mapping()
        {
            Table("BD_ThoiGianChoKham");
            //Map(x => x.T01_TB).Ignore();
            //Map(x => x.T02_TB).Ignore();
            //Map(x => x.T03_TB).Ignore();
            //Map(x => x.T04_TB).Ignore();
            //Map(x => x.T05_TB).Ignore();
            //Map(x => x.T06_TB).Ignore();
            //Map(x => x.T07_TB).Ignore();
            //Map(x => x.T08_TB).Ignore();
            //Map(x => x.T09_TB).Ignore();
            //Map(x => x.T10_TB).Ignore();
            //Map(x => x.T11_TB).Ignore();
            //Map(x => x.T12_TB).Ignore();
            AutoMap();
        }
    }

    public class BD_ThoiGianChoKham
    {
        public int ID { get; set; }

        public int Nam { get; set; }

        public int HienTai { get; set; }

        public float T01 { get; set; }
        public int T01_TuSo { get; set; }
        public int T01_MauSo { get; set; }
        public float T01_TB { get; set; }

        public float T02 { get; set; }
        public int T02_TuSo { get; set; }
        public int T02_MauSo { get; set; }
        public float T02_TB { get; set; }

        public float T03 { get; set; }
        public int T03_TuSo { get; set; }
        public int T03_MauSo { get; set; }
        public float T03_TB { get; set; }

        public float T04 { get; set; }
        public int T04_TuSo { get; set; }
        public int T04_MauSo { get; set; }
        public float T04_TB { get; set; }

        public float T05 { get; set; }
        public int T05_TuSo { get; set; }
        public int T05_MauSo { get; set; }
        public float T05_TB { get; set; }

        public float T06 { get; set; }
        public int T06_TuSo { get; set; }
        public int T06_MauSo { get; set; }
        public float T06_TB { get; set; }

        public float T07 { get; set; }
        public int T07_TuSo { get; set; }
        public int T07_MauSo { get; set; }
        public float T07_TB { get; set; }

        public float T08 { get; set; }
        public int T08_TuSo { get; set; }
        public int T08_MauSo { get; set; }
        public float T08_TB { get; set; }

        public float T09 { get; set; }
        public int T09_TuSo { get; set; }
        public int T09_MauSo { get; set; }
        public float T09_TB { get; set; }

        public float T10 { get; set; }
        public int T10_TuSo { get; set; }
        public int T10_MauSo { get; set; }
        public float T10_TB { get; set; }

        public float T11 { get; set; }
        public int T11_TuSo { get; set; }
        public int T11_MauSo { get; set; }
        public float T11_TB { get; set; }

        public float T12 { get; set; }
        public int T12_TuSo { get; set; }
        public int T12_MauSo { get; set; }
        public float T12_TB { get; set; }
    }

    public class ThoiGianChoKham_HSoft
    {
        public float TyLe { get; set; }

        public int KH20 { get; set; }

        public int Total { get; set; }
        public float AVG_TGC { get; set; }
    }
}