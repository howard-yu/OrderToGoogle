using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace APICenter.Areas.Milestone
{
    public class ReSM_Entities: DbContext
    {
        public ReSM_Entities() : base("name=ReSM")
        {
        }

        public System.Data.Entity.DbSet<APICenter.Areas.Milestone.Models.SMStation> SMStation { get; set; }
        public System.Data.Entity.DbSet<APICenter.Areas.Milestone.Models.SMCity> SMCity { get; set; }
  
    }
}