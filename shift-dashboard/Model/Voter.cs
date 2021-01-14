using System.Collections.Generic;

namespace shift_dashboard.Model
{
    public class Account
    {
        public string username { get; set; }
        public string address { get; set; }
        public string publicKey { get; set; }
        public string balance { get; set; }
    }

    public class VoterApiResult
    {
        public bool success { get; set; }
        public List<Account> accounts { get; set; }
    }
}