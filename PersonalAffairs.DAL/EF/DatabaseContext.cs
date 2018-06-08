using PersonalAffairs.DAL.Entities;
using System.Collections.Generic;
using System.Data.Entity;

namespace PersonalAffairs.DAL.EF
{
    public class DatabaseContext:DbContext
    {
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Project> Projects { get; set; }

        static DatabaseContext()
        {
            Database.SetInitializer<DatabaseContext>(new StoreDbInitializer());
        }
        public DatabaseContext(string connectionString):base(connectionString)
        {

        }

        public class StoreDbInitializer :  CreateDatabaseIfNotExists<DatabaseContext>
        {
            protected override void Seed(DatabaseContext db)
            {
                ICollection<Project> projectsUser1 = new List<Project>
                {
                     new Project()
                                    {
                                        Id = 1,
                                        ProjectName = "Internet Auction",
                                        ProjectPrice = 300
                                    },
                                    new Project()
                                    {
                                        Id = 2,
                                        ProjectName = "Online Shop",
                                        ProjectPrice = 300
                                    }
                 };

                db.Workers.Add(new Worker { FirstName = "Andrew", LastName = "Jonson", Experience = 1, CardNumber = 22012312,
                    Position = new Position { Name = ".NET Developer",  Price = 200, WorkingHours=34 },
                    Unit = new Unit() { Id=1, Name = "Design" },
                    Projects = projectsUser1
                });

                ICollection<Project> projectsUser2 = new List<Project>
                {
                     new Project()
                                    {
                                        Id = 3,
                                        ProjectName = "File System",
                                        ProjectPrice = 400
                                    },
                                    new Project()
                                    {
                                        Id = 4,
                                        ProjectName = "Windows XP",
                                        ProjectPrice = 3200
                                    }
                 };
                db.Workers.Add(new Worker { FirstName = "Max", LastName = "Fall", Experience= 2, CardNumber = 123131313,
                    Position = new Position { Name = "ASP.NET Developer", Price = 100, WorkingHours = 36 },
                    Unit = new Unit() {Id=2, Name = "UnitTests" },
                    Projects = projectsUser2
                });

                ICollection<Project> projectsUser3 = new List<Project>
                {
                     new Project()
                                    {
                                        Id = 5,
                                        ProjectName = "Iphone X",
                                        ProjectPrice = 1100
                                    },
                                    new Project()
                                    {
                                        Id = 6,
                                        ProjectName = "Unity3D",
                                        ProjectPrice = 3300
                                    }
                 };
                Unit unit3 = new Unit() { Id = 3, Name = "AI" };

                db.Workers.Add(new Worker { FirstName = "Ivan", LastName = "Jonson", Experience = 3, CardNumber = 4224244,
                    Position = new Position { Name = "JavaScript Developer", Price = 200, WorkingHours= 45 },
                    Unit = unit3,
                     Projects = projectsUser3
                });

                ICollection<Project> projectsUser4 = new List<Project>
                {
                     new Project()
                                    {
                                        Id = 7,
                                        ProjectName = "Windows Vista",
                                        ProjectPrice = 99
                                    },
                 };

                db.Workers.Add(new Worker
                {
                    FirstName = "Sergey",
                    LastName = "Brin",
                    Experience = 2,
                    CardNumber = 4224211,
                    Position = new Position { Name = "Architect", Price = 200, WorkingHours = 45 },
                    Unit = unit3,
                    Projects = projectsUser4
                });


                db.SaveChanges();
            }
        }
    }
}
