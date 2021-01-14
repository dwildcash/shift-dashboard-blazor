using System.Collections.Generic;

namespace shift_dashboard.Model
{
    public class Delegate
    {
        public string username { get; set; }
        public string address { get; set; }
        public string publicKey { get; set; }
        public string vote { get; set; }
        public int producedblocks { get; set; }
        public int missedblocks { get; set; }
        public int rate { get; set; }
        public int rank { get; set; }
        public double approval { get; set; }
        public int productivity { get; set; }
    }

    public class DelegateApiResult
    {
        public bool success { get; set; }
        public List<Delegate> delegates { get; set; }
        public int totalCount { get; set; }
    }
}