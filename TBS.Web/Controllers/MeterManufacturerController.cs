using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDevs.Domain;
using TDevs.Services;

namespace TBS.Web.Controllers
{
    public class MeterManufacturerController : Controller
    {
        #region call service
        private readonly IMeterManufacturerService _meterManufacturerService;
        public MeterManufacturerController(IMeterManufacturerService meterManufacturerService)
        {
            this._meterManufacturerService = meterManufacturerService;
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
        public JsonResult GetMeterManufacturers()
        {
            var listMeterManufacturer = _meterManufacturerService.Get().ToList();
            return Json(listMeterManufacturer, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult AddMeterManufacturer(MeterManufacturer MeterManufacturer)
        {
            if (MeterManufacturer != null)
            {
                MeterManufacturer.Id = Guid.NewGuid();
                _meterManufacturerService.Add(MeterManufacturer);
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult EditMeterManufacturer(MeterManufacturer meterManufacturer)
        {
            if (meterManufacturer != null)
            {
                _meterManufacturerService.Update(meterManufacturer);
                return Json(true);
            }
            return Json(false);
        }


        [HttpPost]
        public JsonResult DeleteMeterManufacturer(MeterManufacturer meterManufacturer)
        {
            try
            {
                if (meterManufacturer != null)
                {
                    _meterManufacturerService.Remove(meterManufacturer.Id);
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
