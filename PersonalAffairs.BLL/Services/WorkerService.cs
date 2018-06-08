using AutoMapper;
using PersonalAffairs.BLL.DTO;
using PersonalAffairs.BLL.Interfaces;
using PersonalAffairs.DAL.Entities;
using PersonalAffairs.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonalAffairs.BLL.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly IUnitOfWork unitOfWork;
        public WorkerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public void AddWorker(WorkerDTO workerDTO)
        {
            if (workerDTO == null)
                throw new ArgumentNullException();
            IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMap<WorkerDTO, Worker>()).CreateMapper();
            IMapper mapperPosition = new MapperConfiguration(cfg => cfg.CreateMap<Position, PositionDTO>()).CreateMapper();


            Worker worker = mapper.Map<WorkerDTO, Worker>(workerDTO);

            Position position = mapper.Map<PositionDTO, Position>(workerDTO.Position);
            workerDTO.Position = mapperPosition.Map<Position, PositionDTO>(position);
            unitOfWork.Workers.Create(worker);
            unitOfWork.Save();
        }

        public bool DeleteWroker(int workerId)
        {
            Worker worker = unitOfWork.Workers.Get(workerId);

            if (worker != null)
                unitOfWork.Workers.Delete(worker);
            else
                return false;

            unitOfWork.Save();
            return true;
        }


        public IEnumerable<WorkerDTO> GetAllWorkers()
        {
            IMapper mapperWorker = new MapperConfiguration(cfg => cfg.CreateMap<Worker, WorkerDTO>()).CreateMapper();
            IMapper mapperPosition = new MapperConfiguration(cfg => cfg.CreateMap<Position, PositionDTO>()).CreateMapper();
            IMapper mapperUnit = new MapperConfiguration(cfg => cfg.CreateMap<Unit, UnitDTO>()).CreateMapper();

            IEnumerable<Worker> workers = unitOfWork.Workers.GetAll();
            IEnumerable<WorkerDTO> workerDTOs = mapperWorker.Map<IEnumerable<Worker>, IEnumerable<WorkerDTO>>(workers);

            foreach(WorkerDTO workerDTO in workerDTOs)
            {
                workerDTO.Position = mapperPosition.Map<Position, PositionDTO>(workers.Where(u=>u.Id == workerDTO.Id).First().Position);
                workerDTO.Unit = mapperUnit.Map<Unit, UnitDTO>(workers.Where(u => u.Id == workerDTO.Id).First().Unit);
            }
            return workerDTOs;
        }

        public void UpdateWorker(WorkerDTO workerDTO)
        {
            IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMap<WorkerDTO, Worker>()).CreateMapper();
            Worker worker = unitOfWork.Workers.Get(workerDTO.Id);
            worker = mapper.Map<WorkerDTO, Worker>(workerDTO);
            unitOfWork.Workers.Update(worker);
            unitOfWork.Save();
        }

        public WorkerDTO GetWorkerById(int id)
        {
            IMapper mapperWorker = new MapperConfiguration(cfg => cfg.CreateMap<Worker, WorkerDTO>()).CreateMapper();

            Worker worker = unitOfWork.Workers.Get(id);

            if (worker == null)
                return null;

            WorkerDTO workerDTO = mapperWorker.Map<Worker, WorkerDTO>(worker);
            return workerDTO;
        }

    }
}
