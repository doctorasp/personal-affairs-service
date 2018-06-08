using PersonalAffairs.DAL.EF;
using PersonalAffairs.DAL.Entities;
using PersonalAffairs.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PersonalAffairs.DAL.Repositories
{
    public class ProjectRepository : IRepository<Project>
    {
        private readonly DatabaseContext dataContext;
        public ProjectRepository(DatabaseContext databaseContext)
        {
            this.dataContext = databaseContext;
        }
        public void Create(Project item)
        {
            dataContext.Projects.Add(item);
        }

        public void Delete(Project item)
        {
            dataContext.Projects.Remove(item);
        }

        public IEnumerable<Project> Find(Func<Project, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Project Get(int id)
        {
            return dataContext.Projects.Include(h=>h.Worker).Where(x=>x.Id==id).FirstOrDefault();
        }

        public IEnumerable<Project> GetAll()
        {
            return dataContext.Projects.ToList();
        }

        public IEnumerable<Project> GetAllProjectsByWorker(int id)
        {
            return dataContext.Projects.Where(x => x.Worker.Id == id);
        }

        public IEnumerable<Worker> GetAllWorkersByUnit(int unitId)
        {
            throw new NotImplementedException();
        }

        public void Update(Project item)
        {
            dataContext.Entry(item).State = EntityState.Modified;
        }
    }
}
