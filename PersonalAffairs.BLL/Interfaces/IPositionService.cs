using PersonalAffairs.BLL.DTO;
using System.Collections.Generic;

namespace PersonalAffairs.BLL.Interfaces
{
    public interface IPositionService
    {
        void AddPosition(PositionDTO positionDTO);
        bool DeletePosition(int positionId);
        void UpdatePosition(PositionDTO positionDTO);
        PositionDTO GetPositionById(int id);
        IEnumerable<PositionDTO> GetAllPositions();
    }
}
