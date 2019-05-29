using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace APICenter.Areas.Milestone.Models
{
    [Table("SMMilestoneHistory")]
    public partial class SMMilestoneHistory
    {
        public int ID { get; set; }

        [Required]
        [StringLength(6)]
        public string StationID { get; set; }

        [StringLength(20)]
        public string keyValue { get; set; }

        public int? Productline { get; set; }

        public int? MilestoneID { get; set; }

        public DateTime? MilestoneTime { get; set; }

        [StringLength(255)]
        public string IrrReason { get; set; }

        [StringLength(5)]
        public string ApprovedBy { get; set; }

        [StringLength(50)]
        public string ExtraValue1 { get; set; }

        [StringLength(50)]
        public string ExtraValue2 { get; set; }

        public int? UTCOffSet { get; set; }

        public int? Status { get; set; }

        [StringLength(5)]
        public string CreatedBy { get; set; }

        [StringLength(5)]
        public string MilestoneCityCode { get; set; }

        [Required]
        [StringLength(5)]
        public string LocalCityCode { get; set; }

        public DateTime? LocalCreatedDT { get; set; }

        public DateTime? CreatedUTCDT { get; set; }
    }
}