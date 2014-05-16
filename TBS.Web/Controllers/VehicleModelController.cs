using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDevs.Domain;
using TDevs.Services;

namespace TBS.Web.Controllers
{
    public class VehicleModelController : Controller
    {
        #region call service
        private readonly IVehicleModelService _vehicleModelService;
        private readonly IVehicleMakeService _vehicleMakeService;
        public VehicleModelController(IVehicleModelService vehicleModelService, IVehicleMakeService vehicleMakeService)
        {
            this._vehicleModelService = vehicleModelService;
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
            var listVehicleMake = _vehicleMakeService.Get().ToList();
            return Json(listVehicleMake, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetVehicleModels()
        {
            var listVehicleMake = _vehicleMakeService.Get();
            var listVehicleModel = _vehicleModelService.Get();
            var resultLst = from lstVModel in listVehicleModel
                            join lstVMake in listVehicleMake on lstVModel.VehicleMakeId equals lstVMake.Id
                            select new VehicleModelEntity
                            {
                                Id = lstVModel.Id,
                                Name = lstVModel.Name,
                                VehicleMakeId = lstVModel.VehicleMakeId,
                                VehicleMakeName = lstVMake.Name
                            };
            return Json(resultLst.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult AddVehicleModel(VehicleModel vehicleModel)
        {
            if (vehicleModel != null)
            {
                vehicleModel.Id = Guid.NewGuid();
                _vehicleModelService.Add(vehicleModel);
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult EditVehicleModel(VehicleModel vehicleModel)
        {
            if (vehicleModel != null)
            {
                _vehicleModelService.Update(vehicleModel);
                return Json(true);
            }
            return Json(false);
        }


        [HttpPost]
        public JsonResult DeleteVehicleModel(VehicleModel vehicleModel)
        {
            try
            {
                if (vehicleModel != null)
                {
                    _vehicleModelService.Remove(vehicleModel.Id);
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

    public class VehicleModelEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid VehicleMakeId { get; set; }

        public string VehicleMakeName { get; set; }
    }
}
