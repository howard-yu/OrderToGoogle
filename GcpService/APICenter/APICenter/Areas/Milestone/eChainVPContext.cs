using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace APICenter.Areas.Milestone
{
    public class eChainVPContext : DbContext
    {
        public eChainVPContext() : base("name=Milestone")
        {
        }

        public System.Data.Entity.DbSet<APICenter.Areas.Milestone.Models.SMMilestoneDetail> SMMilestoneDetail { get; set; }
        public System.Data.Entity.DbSet<APICenter.Areas.Milestone.Models.SMMilestoneHistory> SMMilestoneHistory { get; set; }
    }
}