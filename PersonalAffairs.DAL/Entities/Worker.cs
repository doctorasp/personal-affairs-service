using System.Collections.Generic;

namespace PersonalAffairs.DAL.Entities
{
    public class Worker
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CardNumber { get; set; }
        public int Experience { get; set; }
        public Position Position { get; set; }
        public Unit Unit { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
