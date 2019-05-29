using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;

namespace APICenter.Areas.Milestone.Models.Interface
{
    public interface IMilestoneRepository : IRepository<SMMilestoneHistory>
    {

        void Create(SMMilestoneHistory instance);

        void Update(SMMilestoneHistory instance);

        void Delete(SMMilestoneHistory instance);

        SMMilestoneHistory Get(int ID);

        IQueryable<SMMilestoneHistory> GetAll();

        void SaveChanges();
    }
}