using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDevs.Domain;
using TDevs.Services;

namespace TBS.Web.Controllers
{
    public class TicketListingController : Controller
    {
        #region action
        public ActionResult Index()
        {
            return View();
        }
        #endregion
    }
}
