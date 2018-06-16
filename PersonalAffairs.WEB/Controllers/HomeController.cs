using AutoMapper;
using PersonalAffairs.BLL.DTO;
using PersonalAffairs.BLL.Interfaces;
using PersonalAffairs.DAL.Entities;
using PersonalAffairs.WEB.Models;
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
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<WorkerDTO, WorkerViewModel>()).CreateMapper();
            var workers = mapper.Map<IEnumerable<WorkerDTO>, IEnumerable<WorkerViewModel>>(workerDtos);

            if (!String.IsNullOrEmpty(searchString))
            {
                workers = workers.Where(s => s.FirstName.Contains(searchString) || s.LastName.Contains(searchString));
             }

            if (cardNumber!=0)
            {
                workers = workers.Where(s => s.CardNumber == cardNumber);
            }
            switch (sortOrder)
            {
                case "name_desc":
                    workers = workers.OrderByDescending(s => s.Position.Price);
                    break;
                case "LastName":
                    workers = workers.OrderByDescending(s => s.LastName);
                    break;
                case "Price":
                    workers = workers.OrderBy(s => s.Position.Price);
                    break;
                default:
                    workers = workers.OrderBy(s => s.FirstName);
                    break;
            }
          
            return View("Index", workers);

        }
        public ActionResult AddWorker()
        {
            IEnumerable<PositionDTO> positions = positionService.GetAllPositions();
            IEnumerable<UnitDTO> units = unitService.GetAllUnits();
            ViewBag.Positions = positions.ToList();
            ViewBag.Units = units.ToList();
            return View("add-worker");
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