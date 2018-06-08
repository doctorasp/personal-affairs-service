using PersonalAffairs.DAL.EF;
using PersonalAffairs.DAL.Entities;
using PersonalAffairs.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PersonalAffairs.DAL.Repositories
{
    public class UnitRepository : IRepository<Unit>
    {
        private readonly DatabaseContext dataContext;
        public UnitRepository(DatabaseContext databaseContext)
        {
            this.dataContext = databaseContext;
        }
        public void Create(Unit item)
        {
            dataContext.Units.Add(item);
        }

        public void Delete(Unit item)
        {
            dataContext.Units.Remove(item);
        }

        public IEnumerable<Unit> Find(Func<Unit, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Unit Get(int id)
        {
            return dataContext.Units.Find(id);
        }

        public IEnumerable<Worker> GetAllWorkersByUnit(int unitId)
        {
            return dataContext.Workers.Where(x => x.Unit.Id == unitId).Include(x=>x.Position);
        }

        public IEnumerable<Unit> GetAll()
        {
            return dataContext.Units;
        }

        public void Update(Unit item)
        {
            var aExists = dataContext.Units.Find(item.Id);
            if (aExists == null)
            {
                dataContext.Units.Add(item);
            }
            else
            {
                dataContext.Entry(aExists).State = EntityState.Detached;
                dataContext.Entry(item).State = EntityState.Modified;
            }
        }

        public IEnumerable<Project> GetAllByWorker(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Project> GetAllProjectsByWorker(int id)
        {
            throw new NotImplementedException();
        }
    }
}
