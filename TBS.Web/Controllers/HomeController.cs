using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDevs.Services;

namespace TBS.Web.Controllers
{
    public class HomeController : TbsControllerBase
    {
        private readonly IBankService bankService;
        private readonly IAccountService accountService;
        private readonly ISateService stateService;
        public HomeController(IBankService bankService, IAccountService accountService, ISateService stateService)
        {
            this.bankService = bankService;
            this.accountService = accountService;
            this.stateService = stateService;
        }

        [HttpPost]
        public ActionResult GetStates() {
            var stateList = stateService.Get();
            var result = (from a in stateList
                         select a.Name).Distinct().ToArray();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            ViewBag.HomeActive = "start active";
            var user = accountService.GetUserByUserName(User.Identity.Name);
            var role = accountService.GetRoles(user.Id).First();
            if (role.AllowDashboard)
                return View("_Dashboard");
            return View();
        }

        public ActionResult LockScreen()
        {
            ViewBag.IsLockScreen = true;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}