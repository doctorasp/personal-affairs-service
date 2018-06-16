using AutoMapper;
using PersonalAffairs.BLL.DTO;
using PersonalAffairs.BLL.Interfaces;
using PersonalAffairs.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalAffairs.WEB.Controllers
{

    public class PositionController : Controller
    {
        IPositionService positionService;
        public PositionController(IPositionService positionService)
        {
            this.positionService = positionService;
        }
        // GET: Position
        public ActionResult Index()
        {
            IEnumerable<PositionDTO> posDtos = positionService.GetAllPositions();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PositionDTO, PositionViewModel>()).CreateMapper();
            var positions = mapper.Map<IEnumerable<PositionDTO>, IEnumerable<PositionViewModel>>(posDtos);
            return View("Index", positions);
        }


        public ActionResult AddPosition()
        {
            return View("AddPosition");
        }


        public ActionResult InfoPosition(int id)
        {
            PositionDTO posDto = positionService.GetPositionById(id);
            return View("PositionInfo", posDto);
        }

        [HttpGet]
        public ActionResult EditPosition(int id)
        {
            PositionDTO posDto = positionService.GetPositionById(id);

            return View("EditPosition", posDto);
        }

        [HttpPost]
        public ActionResult EditPosition(PositionDTO positionDTO)
        {
            positionService.UpdatePosition(positionDTO);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult AddPosition(PositionDTO positionDTO)
        {
            positionService.AddPosition(positionDTO);

            return RedirectToAction("Index");
        }

        public ActionResult DeletePosition(int id)
        {
            positionService.DeletePosition(id);
            return RedirectToAction("Index");
        }
    }
}