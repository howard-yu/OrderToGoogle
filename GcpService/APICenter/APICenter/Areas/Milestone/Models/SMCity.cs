using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace APICenter.Areas.Milestone.Models
{
    [Table("SMCity")]
    public partial class SMCity
    {
        [Key]
        public int HQID { get; set; }

        [Required]
        [StringLength(5)]
        public string CityCode { get; set; }

        public int CountryID { get; set; }

        public int? DistrictID { get; set; }

        public int StateID { get; set; }

        [StringLength(255)]
        public string CityName { get; set; }

        [StringLength(255)]
        public string CityLocalName { get; set; }

        [StringLength(50)]
        public string CityPhone { get; set; }

        public int? AreaID { get; set; }

        [Required]
        [StringLength(10)]
        public string Status { get; set; }

        [StringLength(6)]
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        [StringLength(6)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] Version { get; set; }

        [StringLength(5)]
        public string IATACode { get; set; }

        [StringLength(50)]
        public string CALocCode { get; set; }

        [StringLength(10)]
        public string UNCityCode { get; set; }
    }
}