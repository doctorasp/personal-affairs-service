using Ninject.Modules;
using PersonalAffairs.DAL.Interfaces;
using PersonalAffairs.DAL.Repositories;

namespace PersonalAffairs.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private string connectionString;
        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}
