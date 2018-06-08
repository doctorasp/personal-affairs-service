using PersonalAffairs.DAL.EF;
using PersonalAffairs.DAL.Entities;
using PersonalAffairs.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PersonalAffairs.DAL.Repositories
{
    public class WorkerRepository : IRepository<Worker>
    {
        private readonly DatabaseContext dataContext;
        public WorkerRepository(DatabaseContext databaseContext)
        {
            this.dataContext = databaseContext;
        }
        public void Create(Worker item)
        {
            dataContext.Workers.Add(item);
        }

        public void Delete(Worker item)
        {
            dataContext.Workers.Remove(item);
        }

        public IEnumerable<Worker> Find(Func<Worker, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Worker Get(int id)
        {
            return dataContext.Workers.Where(x => x.Id == id).Include(h=>h.Position).Include(h=>h.Unit).FirstOrDefault();
        }

        public IEnumerable<Worker> GetAll()
        {
            return dataContext.Workers.Include(h=>h.Position).Include(h=>h.Unit).ToList();
        }

        public IEnumerable<Project> GetAllProjectsByWorker(int id)
        {
            return dataContext.Projects.Where(x => x.Worker.Id == id);
        }

        public IEnumerable<Worker> GetAllWorkersByUnit(int unitId)
        {
            throw new NotImplementedException();
        }

        public void Update(Worker item)
        {
            var aExists = dataContext.Workers.Find(item.Id);
            if (aExists == null)
            {
                dataContext.Workers.Add(item);
            }
            else
            {
                dataContext.Entry(aExists).State = EntityState.Detached;
                dataContext.Entry(item).State = EntityState.Modified;
            }
        }
    }
}
