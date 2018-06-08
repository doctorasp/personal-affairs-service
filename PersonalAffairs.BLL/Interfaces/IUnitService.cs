using PersonalAffairs.BLL.DTO;
using System.Collections.Generic;

namespace PersonalAffairs.BLL.Interfaces
{
    public interface IUnitService
    {
        void AddUnit(UnitDTO unitDTO);
        bool DeleteUnit(int unitId);
        UnitDTO GetUnitById(int id);
        void UpdateUnit(UnitDTO unitDTO);
        IEnumerable<UnitDTO> GetAllUnits();
        IEnumerable<WorkerDTO> GetAllWorkersByUnit(int unitId);
    }
}
