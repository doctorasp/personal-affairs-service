using PersonalAffairs.BLL.DTO;
using PersonalAffairs.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PersonalAffairs.WEB.Controllers
{
    public class HomeController : Controller
    {
        IWorkerService workerService;
        IPositionService positionService;
        IUnitService unitService;
        IProjectService projService;

        public HomeController(IWorkerService workerService, IPositionService positionService, IUnitService unitService, IProjectService projService)
        {
            this.workerService = workerService;
            this.positionService = positionService;
            this.unitService = unitService;
            this.projService = projService;
        }

        [HttpPost]
        public ActionResult GlobalSearch(string keyword)
        {
            if (!String.IsNullOrEmpty(keyword))
            {
                IEnumerable<WorkerDTO> workerDtos = workerService.GetAllWorkers();
                workerDtos = workerDtos.Where(s => s.FirstName.Contains(keyword) || s.LastName.Contains(keyword) || s.Position.Name.Contains(keyword) || s.Unit.Name.Contains(keyword));
                ViewBag.workers = workerDtos.ToList();

                IEnumerable<PositionDTO> posDtos = positionService.GetAllPositions();
                posDtos = posDtos.Where(s => s.Name.Contains(keyword));
                ViewBag.positions = posDtos.ToList();

                IEnumerable<UnitDTO> unitDtos = unitService.GetAllUnits();
                unitDtos = unitDtos.Where(s => s.Name.Contains(keyword));
                ViewBag.units = unitDtos.ToList();

            }
                return View("SearchResult");
        }

        public ActionResult Index(string sortOrder, string searchString, int cardNumber=0)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            IEnumerable<WorkerDTO> workerDtos = workerService.GetAllWorkers();

            if (!String.IsNullOrEmpty(searchString))
            {
                workerDtos = workerDtos.Where(s => s.FirstName.Contains(searchString)
                                       || s.LastName.Contains(searchString));
             }

            if (cardNumber!=0)
            {
                workerDtos = workerDtos.Where(s => s.CardNumber == cardNumber);
            }
            switch (sortOrder)
            {
                case "name_desc":
                    workerDtos = workerDtos.OrderByDescending(s => s.Position.Price);
                    break;
                case "LastName":
                    workerDtos = workerDtos.OrderByDescending(s => s.LastName);
                    break;
                case "Price":
                    workerDtos = workerDtos.OrderBy(s => s.Position.Price);
                    break;
                default:
                    workerDtos = workerDtos.OrderBy(s => s.FirstName);
                    break;
            }
          
            return View("Index", workerDtos);
        }
        public ActionResult AddWorker()
        {
            IEnumerable<PositionDTO> positions = positionService.GetAllPositions();
            IEnumerable<UnitDTO> units = unitService.GetAllUnits();
            ViewBag.Positions = positions.ToList();
            ViewBag.Units = units.ToList();
            return View("AddWorker");
        }

        [HttpPost]
        public ActionResult AddWorker(WorkerDTO workerDTO)
        {
            workerService.AddWorker(workerDTO);
            return RedirectToAction("Index");
        }

        public ActionResult InfoWorker(int id)
        {
            WorkerDTO workerDto = workerService.GetWorkerById(id);
            return View("WorkerInfo",  workerDto);
        }

        public ActionResult WorkerProjects(int id)
        {
            IEnumerable<ProjectDTO> projDto = projService.GetAllProjectsByWorker(id);
            return View("WorkerProjects", projDto);
        }

        [HttpGet]
        public ActionResult EditWorker(int id)
        {
            WorkerDTO workerDto = workerService.GetWorkerById(id);

            return View("EditWorker", workerDto);
        }

        [HttpPost]
        public ActionResult EditWorker(WorkerDTO workerDTO)
        {
            workerService.UpdateWorker(workerDTO);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteWorker(int id)
        {
            workerService.DeleteWroker(id);
            return RedirectToAction("Index");
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