namespace PersonalAffairs.DAL.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public decimal ProjectPrice { get; set; }
        public virtual Worker Worker { get; set; }
        public int WorkerId { get; set; }
    }
}
