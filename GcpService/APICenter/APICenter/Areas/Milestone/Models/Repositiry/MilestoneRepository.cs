using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using APICenter.Areas.Milestone.Models.Interface;
using System.Data.Entity;

namespace APICenter.Areas.Milestone.Models
{
    public class MilestoneRepository : IMilestoneRepository , IDisposable
    {
        protected eChainVPContext db
        {
            get;
            private set;
        }

        public MilestoneRepository()
        {
            this.db = new eChainVPContext();
        }

        public void Create(SMMilestoneHistory instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                db.SMMilestoneHistory.Add(instance);
                this.SaveChanges();
            }
        }

        public void Update(SMMilestoneHistory instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                db.Entry(instance).State = EntityState.Modified;
                this.SaveChanges();
            }
        }

        public void Delete(SMMilestoneHistory instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                db.Entry(instance).State = EntityState.Deleted;
                this.SaveChanges();
            }
        }

        public SMMilestoneHistory Get(int ID)
        {
            return db.SMMilestoneHistory.FirstOrDefault(x => x.ID == ID);
        }

        public IQueryable<SMMilestoneHistory> GetAll()
        {
            return db.SMMilestoneHistory.OrderBy(x => x.ID);
        }

        public void SaveChanges()
        {
            this.db.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.db != null)
                {
                    this.db.Dispose();
                    this.db = null;
                }
            }
        }
    }
}