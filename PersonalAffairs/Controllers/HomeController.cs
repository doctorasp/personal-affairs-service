using System.Web.Mvc;

namespace PersonalAffairs.Controllers
{
    public class HomeController : Controller
    {
        IWorkerService workerService; 
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}