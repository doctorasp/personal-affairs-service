using Ninject.Modules;
using PersonalAffairs.BLL.Interfaces;
using PersonalAffairs.BLL.Services;

namespace PersonalAffairs.WEB.Util
{
    public class WorkerModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWorkerService>().To<WorkerService>();
            Bind<IPositionService>().To<PositionService>();
            Bind<IUnitService>().To<UnitService>();
            Bind<IProjectService>().To<ProjectService>();
        }
    }
}