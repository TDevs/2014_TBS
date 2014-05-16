using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDevs.Domain;
using TDevs.Services;

namespace TBS.Web.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService companyService;

        public CompanyController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }
        //
        // GET: /Company/

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

        #region CRUD

        /// <summary>
        /// Get first company
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCompany()
        {
            var lst = companyService.Get();
            if (lst != null && lst.Count > 0)
                return Json(lst.FirstOrDefault(), JsonRequestBehavior.AllowGet);
            return Json(new Company(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveCompany(Company company)
        {
            if (company != null)
            {
                if (company.Id.Equals(Guid.Empty))
                {
                    company.Id = Guid.NewGuid();
                    companyService.Add(company);
                    return Json(true);
                }
                else
                {
                    companyService.Update(company);
                    return Json(true); 
                }
            }
            return Json(false);
        }
        #endregion

    }
}
