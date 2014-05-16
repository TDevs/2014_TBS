using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDevs.Domain.Entities;
using TDevs.Services;

namespace TBS.Web.Controllers
{
    public class RoleController : Controller
    {
        private readonly IAccountService accountService;
        public RoleController(AccountService accountService)
        {
            this.accountService = accountService;
        }
        //
        // GET: /Role/
        public ActionResult Index()
        {
            ViewBag.SettingActive = "start active";
            return View();
        }


        public ActionResult Create()
        {

            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(Role role)
        {
            accountService.CreateRole(role);
            return Json(true);
        }


        public ActionResult Edit()
        {

            return PartialView();
        }
        [HttpPost]
        public ActionResult Edit(Role role)
        {
            accountService.UpdateRole(role);
            return Json(true);
        }

        [HttpPost]
        public ActionResult Delete(Role role)
        {
            accountService.DeleteRole(role);
            return Json(true);
        }

        public JsonResult GetRoles()
        {
            var roles = accountService.GetRoles().ToList();
            return Json(roles, JsonRequestBehavior.AllowGet);
        }
    }
}