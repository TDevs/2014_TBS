using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDevs.Domain.Entities;
using TDevs.Services;

namespace TBS.Web.Controllers
{
    public class SateController : Controller
    {
        #region call service
        private readonly ISateService _sateService;
        public SateController(ISateService sateService)
        {
            this._sateService = sateService;
        }
        #endregion

        #region action
        public ActionResult Index()
        {
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
        public JsonResult GetSates()
        {
            var listSate = _sateService.Get().ToList();
            return Json(listSate, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult AddSate(State sate)
        {
            if (sate != null)
            {
                sate.Id = Guid.NewGuid();
                _sateService.Add(sate);
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult EditSate(State sate)
        {
            if (sate != null)
            {
                _sateService.Update(sate);
                return Json(true);
            }
            return Json(false);
        }


        [HttpPost]
        public JsonResult DeleteSate(State sate)
        {
            try
            {
                if (sate != null)
                {
                    _sateService.Remove(sate.Id);
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
