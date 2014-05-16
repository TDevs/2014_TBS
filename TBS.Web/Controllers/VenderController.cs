using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDevs.Domain;
using TDevs.Services;

namespace TBS.Web.Controllers
{
    public class VenderController : Controller
    {
        private readonly IVenderService venderService;

        public VenderController(IVenderService venderService)
        {
            this.venderService = venderService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return PartialView();
        }

        public ActionResult Details()
        {
            return PartialView();
        }

        public ActionResult Modal()
        {
            return PartialView();
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        public JsonResult GetVenders()
        {
            var listVender = venderService.Get().ToList();
            return Json(listVender, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddVender(CCVendor vender)
        {
            if (vender != null)
            {
                vender.Id = Guid.NewGuid();
                venderService.Add(vender);
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        public JsonResult EditVender(CCVendor vender)
        {
            if (vender != null)
            {
                venderService.Update(vender);
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        public JsonResult DeleteVender(CCVendor vender)
        {
            try
            {
                if (vender != null)
                {
                    venderService.Remove(vender.Id);
                    return Json(true);
                }
                return Json(false);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }
    }
}