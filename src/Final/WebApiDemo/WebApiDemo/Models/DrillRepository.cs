using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace WebApiDemo.Models
{ 
    public class DrillRepository : IDrillRepository
    {
        PlaybookContext context = new PlaybookContext();

        public IQueryable<Drill> All
        {
            get { return context.Drills; }
        }

        public IQueryable<Drill> AllIncluding(params Expression<Func<Drill, object>>[] includeProperties)
        {
            IQueryable<Drill> query = context.Drills;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Drill Find(int id)
        {
            return context.Drills.Find(id);
        }

        public void InsertOrUpdate(Drill drill)
        {
            if (drill.Id == default(int)) {
                // New entity
                context.Drills.Add(drill);
            } else {
                // Existing entity
                context.Entry(drill).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var drill = context.Drills.Find(id);
            context.Drills.Remove(drill);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
        }
    }

    public interface IDrillRepository : IDisposable
    {
        IQueryable<Drill> All { get; }
        IQueryable<Drill> AllIncluding(params Expression<Func<Drill, object>>[] includeProperties);
        Drill Find(int id);
        void InsertOrUpdate(Drill drill);
        void Delete(int id);
        void Save();
    }
}