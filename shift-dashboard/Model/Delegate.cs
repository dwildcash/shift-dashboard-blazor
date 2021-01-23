using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace shift_dashboard.Model
{
    [Index(nameof(Address), Name = "Index_Address", IsUnique = true)]
    public class Delegate
    {

        public Delegate()
        {
            this.DelegateStats = new HashSet<DelegateStat>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [JsonProperty("username")]
        public string Username { get; set; }

        [Required]
        [MaxLength(25)]
        [JsonProperty("address")]
        public string Address { get; set; }

        [Required]
        [MaxLength(64)]
        [JsonProperty("publicKey")]
        public string PublicKey { get; set; }

        [MaxLength(24)]
        [JsonProperty("vote")]
        public string Vote { get; set; }

        [JsonProperty("producedblocks")]
        public int Producedblocks { get; set; }

        [JsonProperty("missedblocks")]
        public int Missedblocks { get; set; }

        [JsonProperty("rate")]
        public int Rate { get; set; }

        [JsonProperty("rank")]
        public int Rank { get; set; }

        [JsonProperty("approval")]
        public double Approval { get; set; }

        [JsonProperty("productivity")]
        public double Productivity { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<DelegateStat> DelegateStats { get; set; }
    }

    public class DelegateApiResult
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("delegates")]
        public List<Delegate> Delegates { get; set; }

        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }
    }
}