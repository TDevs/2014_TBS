using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TDevs.Core.Constants;
using TDevs.Domain.Entities;
using TDevs.Domain.Models;
using TDevs.Services;

namespace TBS.Web
{
    [Authorize]
    public class TbsControllerBase : Controller
    {
        private readonly IGlobalService globalService;

        public TbsControllerBase() { }
        public TbsControllerBase(IGlobalService globalService)
        {
            this.globalService = globalService;
        }


        /// <summary>
        /// Get States
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetStates()
        {
            var states = globalService.GetStates().Select(t =>
                new State()
                {
                    Id = t.Id,
                    Name = t.Name
                });

            return Json(states.ToArray());
        }

        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetFeeTypes()
        {
            var lst = new List<EnumModelView>();
            Type type = typeof(FeeTypes);
            var names = Enum.GetNames(type);
            foreach (var name in names)
            {
                var field = type.GetField(name);
                var fds = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
                foreach (DescriptionAttribute fd in fds)
                {
                    lst.Add(new EnumModelView()
                    {
                        Description = fd.Description,
                        Name = name,
                        IsChoose = false
                    });
                }
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetPaymentTypes()
        {
            var lst = new List<EnumModelView>();
            Type type = typeof(PaymentTypes);
            var names = Enum.GetNames(type);
            foreach (var name in names)
            {
                var field = type.GetField(name);
                var fds = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
                foreach (DescriptionAttribute fd in fds)
                {
                    lst.Add(new EnumModelView()
                    {
                        Description = fd.Description,
                        Name = name,
                        IsChoose = false
                    });
                }
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        protected void ResetNavigator()
        {
            ViewBag.MemberActive = string.Empty;
            ViewBag.AgentActive = string.Empty;
            ViewBag.SupportActive = string.Empty;
            ViewBag.SettingActive = string.Empty;
            ViewBag.ReportActive = string.Empty;
            ViewBag.HomeActive = string.Empty;
            ViewBag.DashboardActive = string.Empty;
        }
    }
}
