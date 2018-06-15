using System.Collections.Generic;

namespace PersonalAffairs.BLL.DTO
{
    public class WorkerDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CardNumber { get; set; }
        
        public ICollection<ProjectDTO> Projects { get; set; }
        public decimal SumOfProjects { get; set; }
        public int Experience { get; set; }

        public int PositionId { get; set; }
        public int UnitId { get; set; }
        public  PositionDTO Position { get; set; }
        public  UnitDTO Unit { get; set; }
    }
}
