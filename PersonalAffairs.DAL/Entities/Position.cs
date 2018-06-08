using System.Collections.Generic;

namespace PersonalAffairs.DAL.Entities
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int WorkingHours { get; set; }
        public ICollection<Worker> Worker { get; set; }
    }
}
