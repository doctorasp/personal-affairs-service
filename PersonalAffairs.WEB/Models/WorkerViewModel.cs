using PersonalAffairs.DAL.Entities;

namespace PersonalAffairs.WEB.Models
{
    public class WorkerViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CardNumber { get; set; }
        public Position Position { get; set; }
        public Unit Unit { get; set; }
    }
}