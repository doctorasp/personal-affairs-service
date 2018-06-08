using PersonalAffairs.DAL.Entities;
using System;
using System.Collections.Generic;

namespace PersonalAffairs.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        IEnumerable<Worker> GetAllWorkersByUnit(int unitId);
        IEnumerable<Project> GetAllProjectsByWorker(int id);
        void Create(T item);
        void Update(T item);
        void Delete(T item);
        
    }
}
