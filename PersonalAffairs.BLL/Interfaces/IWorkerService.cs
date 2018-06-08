using PersonalAffairs.BLL.DTO;
using System.Collections.Generic;

namespace PersonalAffairs.BLL.Interfaces
{
    public interface IWorkerService
    {
        void AddWorker(WorkerDTO workerDTO);
        bool DeleteWroker(int workerId);
        void UpdateWorker(WorkerDTO workerDTO);
        WorkerDTO GetWorkerById(int id);
        IEnumerable<WorkerDTO> GetAllWorkers();
    }
}
