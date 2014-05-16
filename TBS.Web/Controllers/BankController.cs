using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDevs.Domain;
using TDevs.Services;

namespace TBS.Web.Controllers
{
    public class BankController : Controller
    {
        private readonly IBankService bankService;

        public BankController(IBankService bankService)
        {
            this.bankService = bankService;
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

        public JsonResult GetBanks()
        {
            var listbank = bankService.Get().ToList();
            return Json(listbank, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddBank(Bank bank)
        {
            if (bank != null)
            {
                bank.Id = Guid.NewGuid();
                bankService.Add(bank);
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        public JsonResult EditBank(Bank bank)
        {
            if (bank != null)
            {
                bankService.Update(bank);
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        public JsonResult DeleteBank(Bank bank)
        {
            try
            {
                if (bank != null)
                {
                    bankService.Remove(bank.Id);
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