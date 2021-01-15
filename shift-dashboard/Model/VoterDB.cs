using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace shift_dashboard.Model
{
    [Index(nameof(Address), Name = "Index_Address", IsUnique = true)]
    public class VoterDB
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
        public string Balance { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public ICollection<DelegateDB> DelegatesVote { get; set; }
    }
}