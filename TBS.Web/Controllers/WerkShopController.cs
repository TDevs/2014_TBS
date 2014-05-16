using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDevs.Domain;
using TDevs.Services;

namespace TBS.Web.Controllers
{
    public class WerkShopController : Controller
    {
        private readonly IWerkShopService werkshopService;

        public WerkShopController(IWerkShopService werkshopService)
        {
            this.werkshopService = werkshopService;
        }

        public ActionResult Index()
        {
            ViewBag.SettingActive = "start active";
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

        public JsonResult GetWerkShops()
        {
            var listWerkShop = werkshopService.Get().ToList();
            return Json(listWerkShop, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddWerkShop(WerkShop werkshop)
        {
            if (werkshop != null)
            {
                werkshop.Id = Guid.NewGuid();
                werkshopService.Add(werkshop);
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        public JsonResult EditWerkShop(WerkShop werkshop)
        {
            if (werkshop != null)
            {
                werkshopService.Update(werkshop);
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        public JsonResult DeleteWerkShop(WerkShop werkshop)
        {
            try
            {
                if (werkshop != null)
                {
                    werkshopService.Remove(werkshop.Id);
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