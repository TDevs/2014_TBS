using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDevs.Domain;
using TDevs.Services;

namespace TBS.Web.Controllers
{
    public class VehicleMakeController : Controller
    {
        #region call service
        private readonly IVehicleMakeService _vehicleMakeService;
        public VehicleMakeController(IVehicleMakeService vehicleMakeService)
        {
            this._vehicleMakeService = vehicleMakeService;
        }
        #endregion

        #region action
        public ActionResult Index()
        {

            ViewBag.SettingActive = "start active";
            return View();
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        public ActionResult Edit()
        {
            return PartialView();
        }
        #endregion

        #region crud
        [HttpGet]
        public JsonResult GetVehicleMakes()
        {
            var listVehicleMake = _vehicleMakeService.Get().Select(m => new VehicleMake()
            {
                Id = m.Id,
                Name = m.Name
            }).ToList();
            return Json(listVehicleMake, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult AddVehicleMake(VehicleMake vehicleMake)
        {
            if (vehicleMake != null)
            {
                vehicleMake.Id = Guid.NewGuid();
                _vehicleMakeService.Add(vehicleMake);
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult EditVehicleMake(VehicleMake vehicleMake)
        {
            if (vehicleMake != null)
            {
                _vehicleMakeService.Update(vehicleMake);
                return Json(true);
            }
            return Json(false);
        }


        [HttpPost]
        public JsonResult DeleteVehicleMake(VehicleMake vehicleMake)
        {
            try
            {
                if (vehicleMake != null)
                {
                    _vehicleMakeService.Remove(vehicleMake.Id);
                    return Json(true);
                }
                return Json(false);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }
        #endregion
    }
}
