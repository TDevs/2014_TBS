using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TBS.Web.Controllers
{
    public class TheBasicController : Controller
    {
        //
        // GET: /TheBasic/

        public ActionResult Index()
        {
            return View();
        }

    }
}
