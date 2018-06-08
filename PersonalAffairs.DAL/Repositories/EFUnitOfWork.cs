using PersonalAffairs.DAL.EF;
using PersonalAffairs.DAL.Entities;
using PersonalAffairs.DAL.Interfaces;
using System;

namespace PersonalAffairs.DAL.Repositories
{
   public class EFUnitOfWork : IUnitOfWork
    {
        private DatabaseContext db;

        private WorkerRepository workerRepository;
        private PositionRepository positionRepository;
        private UnitRepository unitRepository;
        private ProjectRepository projectRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new DatabaseContext(connectionString);
        }

        public IRepository<Worker> Workers
        {
            get
            {
                if (workerRepository == null)
                    workerRepository = new WorkerRepository(db);
                return workerRepository;
            }
        }

        public IRepository<Position> Positions
        {
            get
            {
                if (positionRepository == null)
                    positionRepository = new PositionRepository(db);
                return positionRepository;
            }
        }

        public IRepository<Unit> Units
        {
            get
            {
                if (unitRepository == null)
                    unitRepository = new UnitRepository(db);
                return unitRepository;
            }
        }

        public IRepository<Project> Projects
        {
            get
            {
                if (projectRepository == null)
                    projectRepository = new ProjectRepository(db);
                return projectRepository;
            }
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
