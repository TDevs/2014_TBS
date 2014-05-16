using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDevs.Domain;
using TDevs.Services;

namespace TBS.Web.Controllers
{
    public class CorporateClientController : Controller
    {
        private readonly ICorporateClientService corporateclientService;

        public CorporateClientController(ICorporateClientService corporateclientService)
        {
            this.corporateclientService = corporateclientService;
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

        public JsonResult GetCorporateClients()
        {
            var listCorporateClients = corporateclientService.Get().ToList();
            return Json(listCorporateClients, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddCorporateClient(CorporateClient corporateclient)
        {
            if (corporateclient != null)
            {
                corporateclient.Id = Guid.NewGuid();
                corporateclientService.Add(corporateclient);
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        public JsonResult EditCorporateClient(CorporateClient corporateclient)
        {
            if (corporateclient != null)
            {
                corporateclientService.Update(corporateclient);
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        public JsonResult DeleteCorporateClient(CorporateClient corporateclient)
        {
            try
            {
                if (corporateclient != null)
                {
                    corporateclientService.Remove(corporateclient.Id);
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