namespace System.App.Entities.HCNS
{
    public class ZK_Machine
    {
        public int ID { get; set; }

        public int MachineID { get; set; }

        public string DeviceName { get; set; }

        public string IPAddress { get; set; }

        public int Port { get; set; }

        public int UserCount { get; set; }
        public int FPCount { get; set; }
        public int FaceCount { get; set; }
        public int AttLogCount { get; set; }
    }
}