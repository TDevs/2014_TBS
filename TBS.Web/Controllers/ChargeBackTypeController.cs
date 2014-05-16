using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDevs.Domain;
using TDevs.Services;

namespace TBS.Web.Controllers
{
    public class ChargeBackTypeController : Controller
    {
        #region call service
        private readonly IChargeBackTypeService _chargeBackTypeService;

        public ChargeBackTypeController(IChargeBackTypeService chargeBackTypeService)
        {
            this._chargeBackTypeService = chargeBackTypeService;
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
        public JsonResult GetChargeBackTypes()
        {
            var listChargeBackType = _chargeBackTypeService.Get().ToList();
            return Json(listChargeBackType, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult AddChargeBackType(ChargeBackType chargeBackType)
        {
            if (chargeBackType != null)
            {
                chargeBackType.Id = Guid.NewGuid();
                _chargeBackTypeService.Add(chargeBackType);
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult EditChargeBackType(ChargeBackType chargeBackType)
        {
            if (chargeBackType != null)
            {
                _chargeBackTypeService.Update(chargeBackType);
                return Json(true);
            }
            return Json(false);
        }


        [HttpPost]
        public JsonResult DeleteChargeBackType(ChargeBackType chargeBackType)
        {
            try
            {
                if (chargeBackType != null)
                {
                    _chargeBackTypeService.Remove(chargeBackType.Id);
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
