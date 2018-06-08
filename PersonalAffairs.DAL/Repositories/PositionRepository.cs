using PersonalAffairs.DAL.EF;
using PersonalAffairs.DAL.Entities;
using PersonalAffairs.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PersonalAffairs.DAL.Repositories
{
    public class PositionRepository : IRepository<Position>
    {
        private readonly DatabaseContext dataContext;
        public PositionRepository(DatabaseContext databaseContext)
        {
            this.dataContext = databaseContext;
        }
        public void Create(Position item)
        {
            dataContext.Positions.Add(item);
        }

        public void Delete(Position item)
        {
            dataContext.Positions.Remove(item);
        }

        public IEnumerable<Position> Find(Func<Position, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Position Get(int id)
        {
            return dataContext.Positions.Find(id);
        }

        public IEnumerable<Position> GetAll()
        {
            return dataContext.Positions.ToList().OrderBy(x=>x.WorkingHours/x.Price);
        }

        public IEnumerable<Project> GetAllByWorker(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Project> GetAllProjectsByWorker(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Worker> GetAllWorkersByUnit(int unitId)
        {
            throw new NotImplementedException();
        }

        public void Update(Position item)
        {
            var aExists = dataContext.Positions.Find(item.Id);
            if (aExists == null)
            {
                dataContext.Positions.Add(item);
            }
            else
            {
                dataContext.Entry(aExists).State = EntityState.Detached;
                dataContext.Entry(item).State = EntityState.Modified;
            }
        }
    }
}
