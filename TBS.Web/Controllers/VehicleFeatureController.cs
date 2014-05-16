using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDevs.Domain;
using TDevs.Services;

namespace TBS.Web.Controllers
{
    public class VehicleFeatureController : Controller
    {
        #region call service
        private readonly IVehicleFeatureService _vehicleFeatureService;

        public VehicleFeatureController(IVehicleFeatureService vehicleFeatureService)
        {
            this._vehicleFeatureService = vehicleFeatureService;
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
        public JsonResult GetVehicleFeatures()
        {
            var lstVehicleFeature = _vehicleFeatureService.Get().Select(f =>
                new VehicleFeature() { Id = f.Id, Name = f.Name, Description = f.Description }).ToList();
            return Json(lstVehicleFeature, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult AddVehicleFeature(VehicleFeature vehicleFeature)
        {
            if (vehicleFeature != null)
            {
                vehicleFeature.Id = Guid.NewGuid();
                _vehicleFeatureService.Add(vehicleFeature);
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult EditVehicleFeature(VehicleFeature vehicleFeature)
        {
            if (vehicleFeature != null)
            {
                _vehicleFeatureService.Update(vehicleFeature);
                return Json(true);
            }
            return Json(false);
        }


        [HttpPost]
        public JsonResult DeleteVehicleFeature(VehicleFeature vehicleFeature)
        {
            try
            {
                if (vehicleFeature != null)
                {
                    _vehicleFeatureService.Remove(vehicleFeature.Id);
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
