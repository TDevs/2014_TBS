using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TDevs.Domain;
using TDevs.Services;

namespace TBS.Web.Controllers
{
    public class ModelYearInsuranceController : Controller
    {
        #region call service
        private readonly IModelYearInsuranceService _modelYearInsuranceService;

        public ModelYearInsuranceController(IModelYearInsuranceService modelYearInsuranceService)
        {
            this._modelYearInsuranceService = modelYearInsuranceService;
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
        public JsonResult GetModelYearInsurances()
        {
            var lstModelYearInsurance = _modelYearInsuranceService.Get().ToList();
            return Json(lstModelYearInsurance, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult AddModelYearInsurance(ModelYearInsurance modelYearInsurance)
        {
            if (modelYearInsurance != null)
            {
                modelYearInsurance.Id = Guid.NewGuid();
                _modelYearInsuranceService.Add(modelYearInsurance);
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult EditModelYearInsurance(ModelYearInsurance modelYearInsurance)
        {
            if (modelYearInsurance != null)
            {
                _modelYearInsuranceService.Update(modelYearInsurance);
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        public JsonResult DeleteModelYearInsurance(ModelYearInsurance modelYearInsurance)
        {
            try
            {
                if (modelYearInsurance != null)
                {
                    _modelYearInsuranceService.Remove(modelYearInsurance.Id);
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