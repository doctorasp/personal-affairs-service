using PersonalAffairs.DAL.Entities;
using PersonalAffairs.DAL.Repositories;
using System;

namespace PersonalAffairs.DAL.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IRepository<Worker> Workers { get; }
        IRepository<Position> Positions { get; }
        IRepository<Unit> Units { get; }
        IRepository<Project> Projects { get;}
        void Save();
    }
}
