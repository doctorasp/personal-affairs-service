using AutoMapper;
using PersonalAffairs.BLL.DTO;
using PersonalAffairs.BLL.Interfaces;
using PersonalAffairs.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PersonalAffairs.WEB.Controllers
{
    public class UnitController : Controller
    {
        IUnitService unitService;
        IProjectService projectService;
        public UnitController(IUnitService unitService, IProjectService projectService)
        {
            this.unitService = unitService;
            this.projectService = projectService;
        }
        // GET: Unit
        public ActionResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            IEnumerable<UnitDTO> unitDtos = unitService.GetAllUnits();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UnitDTO, UnitViewModel>()).CreateMapper();
            var units = mapper.Map<IEnumerable<UnitDTO>, IEnumerable<UnitViewModel>>(unitDtos);

            return View("Index", units);
        }

            public ActionResult AddUnit()
        {
            return View("AddUnit");
        }

        public ActionResult GetAllWorkersByUnit(string sortOrder, int id)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            IEnumerable<WorkerDTO> workerDtos = unitService.GetAllWorkersByUnit(id);

            foreach(WorkerDTO w in workerDtos)
            {
                w.SumOfProjects = projectService.GetSumOfProjects(w.Id);
            }
            switch (sortOrder)
            {
                case "name_desc":
                    workerDtos = workerDtos.OrderBy(s => s.SumOfProjects);
                    break;
                case "Position":
                    workerDtos = workerDtos.OrderBy(s => s.Position.Name);
                    break;
                case "SumOfProjects":
                    workerDtos = workerDtos.OrderBy(s => s.SumOfProjects/s.Experience);
                    break;
                default:
                    workerDtos = workerDtos.OrderByDescending(s => s.SumOfProjects/ s.Experience);
                    break;
            }
            return View("Workers", workerDtos);
        }

        public ActionResult InfoUnit(int id)
        {
            UnitDTO unitDto = unitService.GetUnitById(id);
            return View("UnitInfo", unitDto);
        }

        [HttpPost]
        public ActionResult AddUnit(UnitDTO unitDTO)
        {
            unitService.AddUnit(unitDTO);

            return RedirectToAction("Index");
        }

        public ActionResult DeleteUnit(int id)
        {
            unitService.DeleteUnit(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditUnit(int id)
        {
            UnitDTO posDto = unitService.GetUnitById(id);

            return View("EditUnit", posDto);
        }

        [HttpPost]
        public ActionResult EditUnit(UnitDTO unitDTO)
        {
            unitService.UpdateUnit(unitDTO);
            return RedirectToAction("Index");
        }
    }
}