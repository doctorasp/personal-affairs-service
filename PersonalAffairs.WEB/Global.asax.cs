using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using PersonalAffairs.BLL.Infrastructure;
using PersonalAffairs.WEB.Util;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PersonalAffairs.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
                
            NinjectModule workerModule = new WorkerModule();
            NinjectModule serviceModule = new ServiceModule("DefaultConnection");
            var kernel = new StandardKernel(workerModule, serviceModule);
            kernel.Unbind<ModelValidatorProvider>();
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

        }
    }
}
