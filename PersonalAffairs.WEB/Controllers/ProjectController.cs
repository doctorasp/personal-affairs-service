using PersonalAffairs.BLL.DTO;
using PersonalAffairs.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PersonalAffairs.WEB.Controllers
{
    
    public class ProjectController : Controller
    {
        IProjectService projectService;

        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        public ActionResult Index(string searchString)
        {
            IEnumerable<ProjectDTO> projDtos = projectService.GetAllProjects();
            if (!String.IsNullOrEmpty(searchString))
            {
                projDtos = projDtos.Where(s => s.ProjectName.Contains(searchString));
            }
            return View("Index", projDtos);
        }

        public ActionResult WorkerProjects(int id)
        {
            ViewBag.wId = id;
            IEnumerable<ProjectDTO> projDto = projectService.GetAllProjectsByWorker(id);
            return View("WorkerProjects", projDto);
        }

        [HttpPost]
        public ActionResult AddProject(ProjectDTO projDTO)
        {
            projectService.AddProject(projDTO);
            return RedirectToAction("WorkerProjects/"+projDTO.WorkerId);
        }
    }
}