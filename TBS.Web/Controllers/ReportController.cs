using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDevs.Core.Constants;
using TDevs.Domain.ViewModel;
using TDevs.Services;

namespace TBS.Web.Controllers
{
    public class ReportController : TbsControllerBase
    {
        //
        // GET: /Report/
        private readonly IMemberService memberService;
        private readonly IMedallionService medallionService;
        private readonly ITransactionHistoryService transactionHistoryService;

        public ReportController(IMemberService memberService, IMedallionService medallionService,ITransactionHistoryService transactionHistoryService)
        {
            this.memberService = memberService;
            this.medallionService = medallionService;
            this.transactionHistoryService = transactionHistoryService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PaymemtHistoryReport()
        {
            ViewBag.ReportActive = "start active";
            return View();
        }

        public ActionResult PaymentStatusReport()
        {
            var members = memberService.Get().Where(c => c.Status == MemberStatus.Active).ToList();
            return View(members);
        }

        public ActionResult AjaxGetMemBerByStatus(string status)
        {
            var members = memberService.Get();
            if (status != MemberStatus.All)
                members = members.Where(c => c.Status == status).ToList();

            var st = "<option value='-1'>--Select member--</option>";
            if (members.Any())
            {
                st = members.Aggregate(st, (current, member) => current + string.Format("<option value='{0}'>{1}</option>", member.Id, member.Name));
            }
            return Json(st,JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxGetMedallionByMemberId(Guid memberId)
        {
            var medallions = medallionService.Get().Where(c => c.MemberId == memberId).ToList();

            var st = "";
            if (medallions.Any())
            {
                st += "<option value='-1'>--Select medallion--</option>";
                st = medallions.Aggregate(st, (current, medallion) => current + string.Format("<option value='{0}'>{1}</option>", medallion.Id, medallion.MedallionNumber));
            }
            return Json(st, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MemberInformationReport()
        {
            return View();
        }

        public ActionResult MemberContactListReport()
        {
            return View();
        }

        public ActionResult AssociationMemberSummaryReport()
        {
            return View();
        }

        public ActionResult AssociationFinancialReport()
        {
            return View();
        }

        public ActionResult PaymentHistoryVendorReport()
        {
            return View();
        }

        public ActionResult VehicleReport()
        {
            return View();
        }

        public ActionResult LateReport()
        {
            return View();
        }

        public ActionResult EndOfDateTransactionReport()
        {
            return View();
        }

        public ActionResult OutstandingBalances()
        {
            return View();
        }


        #region PaymentHistory
        [HttpPost]
        public ActionResult ShowPaymentHistory(PaymentHistoryReport parameter)
        {
            var list = transactionHistoryService.GetListTransactionByMedallionMemberId(parameter.MemberId,parameter.MedallionId);
            //member status
            if (parameter.MemberStatus.ToLower().Equals("active"))
            {
                list = list.Where(c => c.IsDeleted == false).ToList();
            }
            else if (parameter.MemberStatus.ToLower().Equals("deleted"))
            {
                list = list.Where(c => c.IsDeleted == true).ToList();
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ShowAssociationReport(DateTime? startDate, DateTime? endDate)
        {
            DateTime stDate = !startDate.HasValue || (startDate.HasValue && startDate == DateTime.MinValue)?DateTime.MinValue :startDate.Value;
            DateTime edDate = !endDate.HasValue ||(endDate.HasValue && endDate == DateTime.MinValue) ? DateTime.MinValue : endDate.Value;

            return Json(true, JsonRequestBehavior.AllowGet);          
        }

        [HttpPost]
        public ActionResult ShowAssociationMemberReport(PaymentHistoryReport parameter)
        {
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ShowLateReport(PaymentHistoryReport parameter)
        {
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ShowMemberContractListReport(PaymentHistoryReport parameter)
        {
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ShowMemberInformationReport(PaymentHistoryReport parameter)
        {
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ShowOutStandingBalanceReport()
        {
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
