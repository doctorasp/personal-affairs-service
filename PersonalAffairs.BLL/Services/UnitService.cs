using AutoMapper;
using PersonalAffairs.BLL.DTO;
using PersonalAffairs.BLL.Interfaces;
using PersonalAffairs.DAL.Entities;
using PersonalAffairs.DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace PersonalAffairs.BLL.Services
{
    public class UnitService : IUnitService
    {
        private readonly IUnitOfWork unitOfWork;
        public UnitService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public void AddUnit(UnitDTO unitDTO)
        {
            if (unitDTO == null)
                throw new ArgumentNullException();
            IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMap<UnitDTO, Unit>()).CreateMapper();
            Unit unit = mapper.Map<UnitDTO, Unit>(unitDTO);
            unitOfWork.Units.Create(unit);
            unitOfWork.Save();
        }

        public bool DeleteUnit(int unitId)
        {
            Unit unit = unitOfWork.Units.Get(unitId);

            if (unit != null)
                unitOfWork.Units.Delete(unit);
            else
                return false;

            unitOfWork.Save();
            return true;
        }

        public IEnumerable<UnitDTO> GetAllUnits()
        {
            IMapper mapperUnit = new MapperConfiguration(cfg => cfg.CreateMap<Unit, UnitDTO>()).CreateMapper();

            IEnumerable<Unit> units = unitOfWork.Units.GetAll();
            IEnumerable<UnitDTO> unitDTOs = mapperUnit.Map<IEnumerable<Unit>, IEnumerable<UnitDTO>>(units);
            return unitDTOs;
        }

        public IEnumerable<WorkerDTO> GetAllWorkersByUnit(int unitId)
        {
            IMapper mapperUnit = new MapperConfiguration(cfg => cfg.CreateMap<Worker, WorkerDTO>()).CreateMapper();

            IEnumerable<Worker> workers = unitOfWork.Units.GetAllWorkersByUnit(unitId);
            IEnumerable<WorkerDTO> workersDTOs = mapperUnit.Map<IEnumerable<Worker>, IEnumerable<WorkerDTO>>(workers);
            return workersDTOs;
        }

        public UnitDTO GetUnitById(int id)
        {
            IMapper mapperWorker = new MapperConfiguration(cfg => cfg.CreateMap<Unit, UnitDTO>()).CreateMapper();

            Unit unit = unitOfWork.Units.Get(id);

            if (unit == null)
                return null;

            UnitDTO unitDTO = mapperWorker.Map<Unit, UnitDTO>(unit);
            return unitDTO;
        }

        public void UpdateUnit(UnitDTO unitDTO)
        {
            IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMap<UnitDTO, Unit>()).CreateMapper();
            Unit unit = unitOfWork.Units.Get(unitDTO.Id);
            unit = mapper.Map<UnitDTO, Unit>(unitDTO);
            unitOfWork.Units.Update(unit);
            unitOfWork.Save();
        }
    }
}
