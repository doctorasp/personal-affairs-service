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
    public class PositionService : IPositionService
    {
        private readonly IUnitOfWork unitOfWork;
        public PositionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public void AddPosition(PositionDTO positionDTO)
        {
            if (positionDTO == null)
                throw new ArgumentNullException();
            IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMap<PositionDTO, Position>()).CreateMapper();
            Position position = mapper.Map<PositionDTO, Position>(positionDTO);
            unitOfWork.Positions.Create(position);
            unitOfWork.Save();
        }

        public bool DeletePosition(int positionId)
        {
            Position position = unitOfWork.Positions.Get(positionId);

            if (position != null)
                unitOfWork.Positions.Delete(position);
            else
                return false;

            unitOfWork.Save();
            return true;
        }

        

        public IEnumerable<PositionDTO> GetAllPositions()
        {
            IMapper mapperWorker = new MapperConfiguration(cfg => cfg.CreateMap<Position, PositionDTO>()).CreateMapper();

            IEnumerable<Position> positions = unitOfWork.Positions.GetAll();
            IEnumerable<PositionDTO> positionDTOs = mapperWorker.Map<IEnumerable<Position>, IEnumerable<PositionDTO>>(positions);

            return positionDTOs;
        }


        public PositionDTO GetPositionById(int id)
        {
            IMapper mapperWorker = new MapperConfiguration(cfg => cfg.CreateMap<Position, PositionDTO>()).CreateMapper();

            Position pos = unitOfWork.Positions.Get(id);

            if (pos == null)
                return null;

            PositionDTO posDTO = mapperWorker.Map<Position, PositionDTO>(pos);
            return posDTO;
        }

        public void UpdatePosition(PositionDTO positionDTO)
        {
            IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMap<PositionDTO, Position>()).CreateMapper();
            Position position = unitOfWork.Positions.Get(positionDTO.Id);
            position = mapper.Map<PositionDTO, Position>(positionDTO);
            unitOfWork.Positions.Update(position);
            unitOfWork.Save();
        }

      
    }
}
