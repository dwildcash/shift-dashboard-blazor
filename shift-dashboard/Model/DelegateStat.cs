using System;
using System.Collections.Generic;

namespace shift_dashboard.Model
{
    public class DelegateStat
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int Rank { get; set; }
        public int DelegateId { get; set; }
        public int TotalVotes { get; set; }
        public ICollection<Account> Voters { get; set; }
    }
}