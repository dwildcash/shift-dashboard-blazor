using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace shift_dashboard.Model
{
    [Index(nameof(Address), Name = "Index_Address", IsUnique = true)]
    public class DelegateDB
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(25)]
        public string Address { get; set; }

        [Required]
        [MaxLength(64)]
        public string PublicKey { get; set; }

        [MaxLength(24)]
        public string Vote { get; set; }

        public int Producedblocks { get; set; }
        public int Missedblocks { get; set; }
        public int Rate { get; set; }
        public int Rank { get; set; }
        public double Approval { get; set; }
        public int Productivity { get; set; }

        public ICollection<VoterDB> Voters {get; set;}
    }
}