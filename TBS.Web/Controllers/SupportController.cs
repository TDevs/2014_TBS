using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDevs.Domain;
using TDevs.Services;
using System.Collections;

namespace TBS.Web.Controllers
{
    public class SupportController : TbsControllerBase
    {

        private readonly ISupportService supportService;

        public SupportController(ISupportService supportService)
        {
            this.supportService = supportService;
        }
        //
        // GET: /Support/
        public ActionResult Index()
        {
            return View();
        }


        #region FAQs
        public ActionResult ListFQAs()
        {
            ViewBag.SupportActive = "start active";
            return View();
        }

        public ActionResult FAQs()
        {
            ResetNavigator();
            ViewBag.SupportActive = "start active";
            return View();
        }

        public JsonResult GetFAQs()
        {
            var results = supportService.GetFAQs();
            return Json(results, JsonRequestBehavior.AllowGet);
        }


        public ActionResult CreateFAQ()
        {
            return PartialView("FaqItem");
        }


        [HttpPost]
        public ActionResult CreateFAQ(FAQ faq)
        {
            var obj = supportService.AddFAQ(faq);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        public ActionResult EditFAQ()
        {
            return PartialView("FaqItem");
        }

        [HttpPost]
        public ActionResult EditFAQ(FAQ faq)
        {
            var obj = supportService.UpdateFAQ(faq);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteFAQ(Guid Id)
        {
            var result = supportService.RemoveFAQ(Id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion



        #region Tutorial

        public JsonResult GetTutorials()
        {
            var results = supportService.GetTutorials();
            return Json(results, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ListTutorials()
        {
            ViewBag.SupportActive = "start active";
            return View("ListTutorial");
        }

        public ActionResult Tutorials()
        {
            ViewBag.SupportActive = "start active";
            return View();
        }

        public ActionResult CreateTutorial()
        {
            return PartialView("TutorialItem");
        }

        [HttpPost]
        public ActionResult CreateTutorial(Tutorial tutorial)
        {
            tutorial.CreateBy = User.Identity.Name;
            tutorial.CreateAt = DateTime.Now;

            var obj = supportService.AddTutorial(tutorial);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditTutorial()
        {
            return PartialView("TutorialItem");
        }

        [HttpPost]
        public ActionResult EditTutorial(Tutorial tutorial)
        {
            tutorial.CreateBy = User.Identity.Name;
            tutorial.CreateAt = DateTime.Now;
            var obj = supportService.UpdateTutorial(tutorial);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteTutorial(Guid Id)
        {
            var result = supportService.RemoveTutorial(Id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region Raise Ticket

        public ActionResult RaiseTicket()
        {
            ViewBag.SupportActive = "start active";
            return View();
        }

        public ActionResult Ticket()
        {
            ViewBag.SupportActive = "start active";
            return View();
        }

        public ActionResult ViewTicketList()
        {
            return PartialView();
        }

        public ActionResult ViewTicket()
        {
            return PartialView();
        }

        public JsonResult GetRaiseTicket()
        {
            List<Ticket> lst = new List<Ticket>();
            var result = supportService.GetSupportTickets();
            foreach (var item in result)
            {
                var ticket = new Ticket()
                {
                    Id = item.Id,
                    Title = item.Title,
                    CreateBy = item.CreateBy,
                    Status = item.Status
                };

                ticket.TicketItems = new List<TicketItem>();

                if (item.TicketItems != null)
                {
                    foreach (var tItem in item.TicketItems)
                    {
                        var ticketItem = new TicketItem()
                        {
                            Id = tItem.Id,
                            SupportTicketId = tItem.SupportTicketId,
                            CreateBy = tItem.CreateBy,
                            CreateAt = tItem.CreateAt,
                            Message = tItem.Message,
                            Read = tItem.Read

                        };

                        ticket.TicketItems.Add(ticketItem);
                    }


                }

                lst.Add(ticket);

            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult PostRaiseTicket(Ticket ticket)
        {
            if (ticket != null)
            {
                ticket.Id = Guid.NewGuid();
                ticket.Status = "Open";
                ticket.CreateBy = User.Identity.Name;

                foreach (var item in ticket.TicketItems)
                {
                    item.Id = Guid.NewGuid();
                    item.SupportTicketId = ticket.Id;
                    item.CreateAt = DateTime.Now;
                    item.CreateBy = User.Identity.Name;
                }

                supportService.AddSupportTicket(ticket);
                return Json(true);
            }
            return Json(false);
        }


        [HttpPost]
        [ValidateInput(false)]
        public JsonResult PostTicketMessage(Ticket supportTicket)
        {
            if (supportTicket != null)
            {

                // UPdate Ticket Items
                if (supportTicket.TicketItems != null)
                {
                    foreach (var item in supportTicket.TicketItems)
                    {
                        item.SupportTicketId = supportTicket.Id;
                        item.CreateAt = DateTime.Now;
                        item.CreateBy = User.Identity.Name;
                    }
                }
                supportService.ReplySupportTicket(supportTicket);
                return Json(true);
            }
            return Json(false);
        }

        #endregion

    }
}