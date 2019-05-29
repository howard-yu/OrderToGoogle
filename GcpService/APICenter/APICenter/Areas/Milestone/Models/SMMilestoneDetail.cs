using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace APICenter.Areas.Milestone.Models
{

    [Table("SMMilestoneDetail")]
    public partial class SMMilestoneDetail
    {
        public int ID { get; set; }

        [StringLength(6)]
        public string StationID { get; set; }

        [StringLength(20)]
        public string ParentkeyValue { get; set; }

        [StringLength(20)]
        public string ContainerKey { get; set; }

        [StringLength(20)]
        public string POKey { get; set; }

        [StringLength(20)]
        public string SOKey { get; set; }

        public int? ProductLine { get; set; }

        public int? MilestoneID { get; set; }

        public DateTime? MilestoneTime { get; set; }

        [StringLength(50)]
        public string ExtraValue1 { get; set; }

        [StringLength(50)]
        public string ExtraValue2 { get; set; }

        public int? UTCOffset { get; set; }

        public int? Status { get; set; }

        public DateTime? CreatedUTCDT { get; set; }

        [StringLength(5)]
        public string CreatedBy { get; set; }

        [StringLength(5)]
        public string MilestoneCityCode { get; set; }

        [StringLength(5)]
        public string LocalCityCode { get; set; }

        public DateTime? LocalCreatedDT { get; set; }
    }
}