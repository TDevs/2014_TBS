using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TBS.Web.Utility;
using TDevs.Domain;
using TDevs.Domain.ViewModel;
using TDevs.Services;

namespace TBS.Web.Controllers
{
    public partial class MemberController : TbsControllerBase
    {
        private readonly IMemberService memberService;
        private readonly IAgentService agentService;
        private readonly IAgentVehicleService agentVehicleService;
        private readonly IContactService contactService;
        private readonly IVehicleService vehicleService;
        private readonly IMeterManufacturerService meterManufacturerService;
        private readonly IStockholderService stockholderService;
        private readonly IMedallionService medallionService;
        private readonly IStandardDuesService standardDueService;

        private readonly IVehicleMakeService _vehicleMakeService;
        private readonly IVehicleModelService _vehicleModelService;
        private readonly IModelYearInsuranceService _modelYearInsuranceService;
        private readonly IUniversalAgentRecordService _universalAgentRecordService;

        private readonly IRTAService rtaService;
        private readonly IMobilityService mobilityService;

        public MemberController(IMemberService memberService, IAgentService agentService, IContactService contactService, IMedallionService medallionService,
            IVehicleService vehicleService, IMeterManufacturerService meterManufacturerService, IStockholderService stockholderService, IAgentVehicleService agentVehicleService, IStandardDuesService standardDueService,
            IVehicleMakeService vehicleMakeService, IModelYearInsuranceService modelYearInsuranceService, IVehicleModelService vehicleModelService, IUniversalAgentRecordService _universalAgentRecordService,
            IRTAService rtaService, IMobilityService mobilityService)
        {
            this.memberService = memberService;
            this.agentService = agentService;
            this.agentVehicleService = agentVehicleService;
            this.contactService = contactService;
            this.stockholderService = stockholderService;
            this.meterManufacturerService = meterManufacturerService;
            this.medallionService = medallionService;
            this.vehicleService = vehicleService;
            this.standardDueService = standardDueService;

            this._vehicleMakeService = vehicleMakeService;
            this._modelYearInsuranceService = modelYearInsuranceService;
            this._vehicleModelService = vehicleModelService;
            this._universalAgentRecordService = _universalAgentRecordService;
            this.rtaService = rtaService;
            this.mobilityService = mobilityService;
        }

        public ActionResult NewInfo()
        {
            return PartialView();
        }

        public ActionResult EditInfo()
        {
            ViewBag.HighLightInfo = "active";
            return PartialView();
        }

        public ActionResult Search()
        {
            ViewBag.MemberActive = "start active";
            return View();
        }

        public ActionResult SearchMember()
        {
            return PartialView();
        }
        public ActionResult NewStockholder()
        {
            return PartialView();
        }

        public ActionResult EditStockholder()
        {
            return PartialView();
        }

        #region Member Info

        [HttpPost]
        public ActionResult GetMemberByAccountName(string accountNumber)
        {
            var member = memberService.Get().Where(c => c.AccountNumber.Trim().Equals(accountNumber.Trim()) && !c.IsDeleted).FirstOrDefault();
            return Json(member, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetMemberById(Guid id)
        {
            var member = memberService.Get(id);

            if (member != null)
            {
                return Json(new Member()
                {
                    AccountNumber = member.AccountNumber,
                    Address = member.Address,
                    Address1 = member.Address1,
                    AgentAddress = member.AgentAddress,
                    AgentAddress1 = member.AgentAddress1,
                    AgentCity = member.AgentCity,
                    AgentState = member.AgentState,
                    AgentZip = member.AgentZip,
                    Article = member.Article,
                    City = member.City,
                    DefaultWorkerComp = member.DefaultWorkerComp,
                    Email = member.Email,
                    Fax = member.Fax,
                    Fein = member.Fein,
                    Id = member.Id,
                    IncorporationDate = member.IncorporationDate,
                    IRIS = member.IRIS,
                    JoinedDate = member.JoinedDate,
                    LateBreaks = member.LateBreaks,
                    Name = member.Name,
                    Phone1 = member.Phone1,
                    Phone2 = member.Phone2,
                    PreferPayment = member.PreferPayment,
                    ReciepMessage = member.ReciepMessage,
                    RegisteredAgentName = member.RegisteredAgentName,
                    SSN = member.SSN,
                    State = member.State,
                    Status = member.Status,
                    UseThisAddressForBilling = member.UseThisAddressForBilling,
                    Zip = member.Zip
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetMemberList()
        {
            /*dodq add*/
            List<ResultMemberSearch> list = new List<ResultMemberSearch>();
            var listMember = memberService.Get().ToList();
            list = (from member in listMember
                    where !member.IsDeleted
                    select new ResultMemberSearch()
                    {
                        AccountNumber = member != null ? member.AccountNumber : string.Empty,
                        MemberId = member != null ? member.Id : Guid.Empty,
                        MemberName = member != null ? member.Name : string.Empty,
                    }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetAccountNumberListForAutoComplete()
        {
            var list = memberService.Get().Where(c => !c.IsDeleted).Select(x => x.AccountNumber).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateMemberInfo(Member member)
        {
            if (member != null)
            {
                member.Id = Guid.NewGuid();
                memberService.Add(member);
            }
            return Json(false);
        }

        [HttpPost]
        public ActionResult EditMemberInfo(Member member)
        {
            if (member != null)
            {
                memberService.Update(member);
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        public ActionResult DeleteMemberInfo(Member member)
        {
            try
            {
                if (member != null)
                {
                    member.IsDeleted = true;
                    memberService.Update(member);
                    return Json(true);

                }
            }
            catch
            {
                return Json(false);
            }
            return Json(false);
        }
        #endregion

        #region Stockholder
        [HttpPost]
        public ActionResult GetStockholderByMemberId(Member member)
        {
            List<Stockholder> list = new List<Stockholder>();
            if (member != null && member.Id != Guid.Empty)
            {
                var listStock = stockholderService.Get();
                list = (from a in listStock
                        where a.MemberId.Equals(member.Id)
                        select a).ToList();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateStockholder(Stockholder stockholder)
        {
            if (stockholder != null)
            {
                stockholderService.Update(stockholder);
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        public ActionResult InsertStockholder(Stockholder stockholder)
        {
            if (stockholder != null)
            {
                stockholder.Id = Guid.NewGuid();
                stockholderService.Add(stockholder);
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        public ActionResult DelStockholder(Stockholder stockholder)
        {
            if (stockholder != null)
            {
                stockholderService.Remove(stockholder.Id);
                return Json(true);
            }
            return Json(false);
        }
        #endregion

        #region search
        [HttpPost]
        public ActionResult SearchByCriterial(CriterialSearch criterial)
        {
            List<ResultMemberSearch> list = new List<ResultMemberSearch>();
            var listMember = memberService.Get().ToList();
            var listAgent = agentService.Get();
            var listContact = contactService.Get();
            var listMedallion = medallionService.Get();
            var listStandardDues = standardDueService.Get();

            if (!criterial.ViewDeleted)
            {
                listMember = (from a in listMember
                              where !a.IsDeleted
                              select a).ToList();
            }
            else
            {
                listMember = (from a in listMember
                              where a.IsDeleted
                              select a).ToList();
            }
            //result 
            list = (from member in listMember
                    join contact in listContact on member.Id equals contact.MemberId into A
                    from a in A.DefaultIfEmpty()
                    join agent in listAgent on member.Id equals agent.MemberId into B
                    from b in B.DefaultIfEmpty()
                    join medallion in listMedallion on member.Id equals medallion.MemberId into C
                    from c in C.DefaultIfEmpty()
                    join standarddue in listStandardDues on member.Id equals standarddue.MemberId into D
                    from d in D.DefaultIfEmpty()
                    select new ResultMemberSearch()
                    {
                        AccountNumber = member != null ? member.AccountNumber : string.Empty,
                        MedallionNumber = c != null ? c.MedallionNumber : string.Empty,
                        Member = new Member()
                        {
                            Id = member.Id,
                            AccountNumber = member.AccountNumber,
                            Address = member.Address,
                            Address1 = member.Address1,
                            AgentAddress = member.AgentAddress,
                            AgentAddress1 = member.AgentAddress1,
                            AgentCity = member.AgentCity,
                            AgentState = member.AgentState,
                            AgentZip = member.AgentZip,
                            Article = member.Article,
                            City = member.City,
                            DefaultWorkerComp = member.DefaultWorkerComp,
                            Email = member.Email,
                            Fax = member.Fax,
                            Fein = member.Fein,
                            IncorporationDate = member.IncorporationDate,
                            IRIS = member.IRIS,
                            JoinedDate = member.JoinedDate,
                            LateBreaks = member.LateBreaks,
                            Name = member.Name,
                            Phone1 = member.Phone1,
                            Phone2 = member.Phone2,
                            PreferPayment = member.PreferPayment,
                            ReciepMessage = member.ReciepMessage,
                            RegisteredAgentName = member.RegisteredAgentName,
                            SSN = member.SSN,
                            State = member.State,
                            Status = member.Status,
                            UseThisAddressForBilling = member.UseThisAddressForBilling,
                            Zip = member.Zip,
                            IsDeleted = member.IsDeleted
                        },
                        MemberId = member != null ? member.Id : Guid.Empty,
                        MemberName = member != null ? member.Name : string.Empty,
                        PaymentDueDate = d != null ? d.StartDate : DateTime.MinValue,
                        Phone = member != null ? member.Phone1 : string.Empty,
                        PreferPayment = member != null ? member.PreferPayment : string.Empty,
                        Status = member != null ? member.Status : string.Empty,
                        ChaufferLic = b != null ? b.ChaufferLic : string.Empty,
                        AgentName = b != null ? String.Format("{0} {1}", b.FirstName, b.LastName) : string.Empty,
                        ContactName = a != null ? String.Format("{0} {1}", a.FirstName, a.LastName) : string.Empty,
                        SSN = member != null ? member.SSN : string.Empty
                    }).ToList();

            //group to distinct
             var listgroup = from a in list
                            group a by a.MemberId into g
                            select new ResultMemberSearch()
                            {
                                AccountNumber = g != null ? g.First().AccountNumber : string.Empty,
                                MedallionNumber = g != null ? g.First().MedallionNumber : string.Empty,
                                Member = g != null ? new Member()
                                {
                                    Id = g.First().MemberId,
                                    AccountNumber = g.First().AccountNumber,
                                    Address = g.First().Member.Address,
                                    Address1 = g.First().Member.Address1,
                                    AgentAddress = g.First().Member.AgentAddress,
                                    AgentAddress1 = g.First().Member.AgentAddress1,
                                    AgentCity = g.First().Member.AgentCity,
                                    AgentState = g.First().Member.AgentState,
                                    AgentZip = g.First().Member.AgentZip,
                                    Article = g.First().Member.Article,
                                    City = g.First().Member.City,
                                    DefaultWorkerComp = g.First().Member.DefaultWorkerComp,
                                    Email = g.First().Member.Email,
                                    Fax = g.First().Member.Fax,
                                    Fein = g.First().Member.Fein,
                                    IncorporationDate = g.First().Member.IncorporationDate,
                                    IRIS = g.First().Member.IRIS,
                                    JoinedDate = g.First().Member.JoinedDate,
                                    LateBreaks = g.First().Member.LateBreaks,
                                    Name = g.First().Member.Name,
                                    Phone1 = g.First().Member.Phone1,
                                    Phone2 = g.First().Member.Phone2,
                                    PreferPayment = g.First().Member.PreferPayment,
                                    ReciepMessage = g.First().Member.ReciepMessage,
                                    RegisteredAgentName = g.First().Member.RegisteredAgentName,
                                    SSN = g.First().Member.SSN,
                                    State = g.First().Member.State,
                                    Status = g.First().Status,
                                    UseThisAddressForBilling = g.First().Member.UseThisAddressForBilling,
                                    Zip = g.First().Member.Zip,
                                    IsDeleted = g.First().Member.IsDeleted
                                } : null,
                                MemberId = g != null ? g.First().MemberId : Guid.Empty,
                                MemberName = g != null ? g.First().MemberName : string.Empty,
                                PaymentDueDate = g != null ? g.First().PaymentDueDate : DateTime.MinValue,
                                Phone = g != null ? g.First().Phone : string.Empty,
                                PreferPayment = g != null ? g.First().PreferPayment : string.Empty,
                                Status = g != null ? g.First().Status : string.Empty,
                                ChaufferLic = g != null ? g.First().ChaufferLic : string.Empty,
                                AgentName = g != null ? g.First().AgentName : string.Empty,
                                ContactName = g != null ? g.First().ContactName : string.Empty,
                                SSN = g != null ? g.First().SSN : string.Empty
                            };

            if (criterial.SearchBy == (int)EnumSearchBy.SearchByAccountNumber)
            {
                listgroup = (from a in listgroup
                        where a.AccountNumber != null && a.AccountNumber.ToUpper().Equals(criterial.Keyword.ToUpper())
                        select a).ToList();
            }
            else if (criterial.SearchBy == (int)EnumSearchBy.SearchAgentsChaufferNumber)
            {
                listgroup = (from a in listgroup
                        where a.ChaufferLic != null && a.ChaufferLic.ToUpper().Equals(criterial.Keyword.ToUpper())
                        select a).ToList();
            }
            else if (criterial.SearchBy == (int)EnumSearchBy.SearchByAgentName)
            {
                listgroup = (from a in listgroup
                        where a.AgentName != null && a.AgentName.ToUpper().Contains(criterial.Keyword.ToUpper())
                        select a).ToList();
            }
            else if (criterial.SearchBy == (int)EnumSearchBy.SearchByMedallionNumber)
            {
                listgroup = (from a in listgroup
                        where a.MedallionNumber != null && a.MedallionNumber.ToUpper().Equals(criterial.Keyword.ToUpper())
                        select a).ToList();
            }
            else if (criterial.SearchBy == (int)EnumSearchBy.SearchByMemberContactName)
            {
                listgroup = (from a in listgroup
                        where a.ContactName != null && a.ContactName.ToUpper().Contains(criterial.Keyword.ToUpper())
                        select a).ToList();
            }
            else if (criterial.SearchBy == (int)EnumSearchBy.SearchByMemberName)
            {
                listgroup = (from a in listgroup
                        where a.MemberName != null && a.MemberName.ToUpper().Contains(criterial.Keyword.ToUpper())
                        select a).ToList();
            }
            else if (criterial.SearchBy == (int)EnumSearchBy.SearchBySSN)
            {
                listgroup = (from a in listgroup
                        where a.SSN != null && a.SSN.ToUpper().Equals(criterial.Keyword.ToUpper())
                        select a).ToList();
            }
            if (!string.IsNullOrEmpty(criterial.SearchFilter))
            {
                listgroup = (from a in listgroup
                        where ((a.AccountNumber != null && a.AccountNumber.ToUpper().Contains(criterial.SearchFilter.ToUpper()))
                            || (a.MemberName != null && a.MemberName.ToUpper().Contains(criterial.SearchFilter.ToUpper()))
                            || (a.Phone != null && a.Phone.ToUpper().Contains(criterial.SearchFilter.ToUpper()))
                            || (a.Status != null && a.Status.ToUpper().Contains(criterial.SearchFilter.ToUpper()))
                            || (a.PreferPayment != null && a.PreferPayment.ToUpper().Contains(criterial.SearchFilter.ToUpper()))
                            || (a.PaymentDueDate != DateTime.MinValue && string.Format("{0:MM/dd/yyyy}", a.PaymentDueDate).Contains(criterial.SearchFilter)))
                        select a).ToList();
            }
            foreach (var item in listgroup)
            {
                if (item.PaymentDueDate.Equals(DateTime.MinValue))
                {
                    item.PaymentDueDate = null;
                }
            }

            return Json(listgroup, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SearchByAll()
        {
            List<ResultMemberSearch> list = new List<ResultMemberSearch>();
            var listMember = memberService.Get().ToList();
            var listAgent = agentService.Get();
            var listContact = contactService.Get();
            var listMedallion = medallionService.Get();
            var listStandardDues = standardDueService.Get();


            //result 
            list = (from member in listMember
                    join contact in listContact on member.Id equals contact.MemberId into A
                    from a in A.DefaultIfEmpty()
                    join agent in listAgent on member.Id equals agent.MemberId into B
                    from b in B.DefaultIfEmpty()
                    join medallion in listMedallion on member.Id equals medallion.MemberId into C
                    from c in C.DefaultIfEmpty()
                    join standarddue in listStandardDues on member.Id equals standarddue.MemberId into D
                    from d in D.DefaultIfEmpty()
                    select new ResultMemberSearch()
                    {
                        AccountNumber = member != null ? member.AccountNumber : string.Empty,
                        MedallionNumber = c != null ? c.MedallionNumber : string.Empty,
                        Member = new Member()
                        {
                            Id = member.Id,
                            AccountNumber = member.AccountNumber,
                            Address = member.Address,
                            Address1 = member.Address1,
                            AgentAddress = member.AgentAddress,
                            AgentAddress1 = member.AgentAddress1,
                            AgentCity = member.AgentCity,
                            AgentState = member.AgentState,
                            AgentZip = member.AgentZip,
                            Article = member.Article,
                            City = member.City,
                            DefaultWorkerComp = member.DefaultWorkerComp,
                            Email = member.Email,
                            Fax = member.Fax,
                            Fein = member.Fein,
                            IncorporationDate = member.IncorporationDate,
                            IRIS = member.IRIS,
                            JoinedDate = member.JoinedDate,
                            LateBreaks = member.LateBreaks,
                            Name = member.Name,
                            Phone1 = member.Phone1,
                            Phone2 = member.Phone2,
                            PreferPayment = member.PreferPayment,
                            ReciepMessage = member.ReciepMessage,
                            RegisteredAgentName = member.RegisteredAgentName,
                            SSN = member.SSN,
                            State = member.State,
                            Status = member.Status,
                            UseThisAddressForBilling = member.UseThisAddressForBilling,
                            Zip = member.Zip,
                            IsDeleted = member.IsDeleted
                        },
                        MemberId = member != null ? member.Id : Guid.Empty,
                        MemberName = member != null ? member.Name : string.Empty,
                        PaymentDueDate = d != null ? d.StartDate : DateTime.MinValue,
                        Phone = member != null ? member.Phone1 : string.Empty,
                        PreferPayment = member != null ? member.PreferPayment : string.Empty,
                        Status = member != null ? member.Status : string.Empty,
                        ChaufferLic = b != null ? b.ChaufferLic : string.Empty,
                        AgentName = b != null ? String.Format("{0} {1}", b.FirstName, b.LastName) : string.Empty,
                        ContactName = a != null ? String.Format("{0} {1}", a.FirstName, a.LastName) : string.Empty,
                        SSN = member != null ? member.SSN : string.Empty
                    }).ToList();

            foreach (var item in list)
            {
                if (item.PaymentDueDate.Equals(DateTime.MinValue))
                {
                    item.PaymentDueDate = null;
                }
            }

            //group to distinct
            var listgroup = from a in list
                            group a by a.MemberId into g
                            select new ResultMemberSearch()
                            {
                                AccountNumber = g != null ? g.First().AccountNumber : string.Empty,
                                MedallionNumber = g != null ? g.First().MedallionNumber : string.Empty,
                                Member = g!=null? new Member()
                                {
                                    Id = g.First().MemberId,
                                    AccountNumber = g.First().AccountNumber,
                                    Address = g.First().Member.Address,
                                    Address1 = g.First().Member.Address1,
                                    AgentAddress = g.First().Member.AgentAddress,
                                    AgentAddress1 = g.First().Member.AgentAddress1,
                                    AgentCity = g.First().Member.AgentCity,
                                    AgentState = g.First().Member.AgentState,
                                    AgentZip = g.First().Member.AgentZip,
                                    Article = g.First().Member.Article,
                                    City = g.First().Member.City,
                                    DefaultWorkerComp = g.First().Member.DefaultWorkerComp,
                                    Email = g.First().Member.Email,
                                    Fax = g.First().Member.Fax,
                                    Fein = g.First().Member.Fein,
                                    IncorporationDate = g.First().Member.IncorporationDate,
                                    IRIS = g.First().Member.IRIS,
                                    JoinedDate = g.First().Member.JoinedDate,
                                    LateBreaks = g.First().Member.LateBreaks,
                                    Name = g.First().Member.Name,
                                    Phone1 = g.First().Member.Phone1,
                                    Phone2 = g.First().Member.Phone2,
                                    PreferPayment = g.First().Member.PreferPayment,
                                    ReciepMessage = g.First().Member.ReciepMessage,
                                    RegisteredAgentName = g.First().Member.RegisteredAgentName,
                                    SSN = g.First().Member.SSN,
                                    State = g.First().Member.State,
                                    Status = g.First().Status,
                                    UseThisAddressForBilling = g.First().Member.UseThisAddressForBilling,
                                    Zip = g.First().Member.Zip,
                                    IsDeleted = g.First().Member.IsDeleted
                                }:null,
                                MemberId = g != null ? g.First().MemberId : Guid.Empty,
                                MemberName = g != null ? g.First().MemberName : string.Empty,
                                PaymentDueDate = g != null ? g.First().PaymentDueDate : DateTime.MinValue,
                                Phone = g != null ? g.First().Phone: string.Empty,
                                PreferPayment = g != null ? g.First().PreferPayment : string.Empty,
                                Status = g != null ? g.First().Status : string.Empty,
                                ChaufferLic = g != null ? g.First().ChaufferLic : string.Empty,
                                AgentName = g != null ? g.First().AgentName: string.Empty,
                                ContactName =g != null ? g.First().ContactName: string.Empty,
                                SSN = g != null ? g.First().SSN : string.Empty
                            };
            return Json(listgroup, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Contact
        public ActionResult NewContact()
        {
            return PartialView();
        }
        public ActionResult ListContact()
        {
            ViewBag.HighLightContact = "active";
            return PartialView();
        }
        public ActionResult EditContact()
        {
            return PartialView();
        }

        public ActionResult GetListContacts(Guid memberId)
        {
            List<Contact> listContact = new List<Contact>();
            var list = contactService.Get();
            listContact = (from a in list
                           where a.MemberId.Equals(memberId)
                           select a).ToList();
            return Json(listContact, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertContact(Contact contact)
        {
            if (contact != null)
            {
                contact.Id = Guid.NewGuid();
                contactService.Add(contact);
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        public ActionResult UpdateContact(Contact contact)
        {
            if (contact != null)
            {
                contactService.Update(contact);
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        public ActionResult DelContact(Contact contact)
        {
            try
            {
                if (contact != null)
                {
                    contactService.Remove(contact.Id);
                    return Json(true);
                }
                return Json(false);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }

        [HttpPost]
        public ActionResult UploadFileImage(HttpPostedFileBase image)
        {
            if (Request.Files[0].ContentLength > 0)
            {
                image = Request.Files[0];
                var fileName = Path.GetFileName(image.FileName);
                var path = Path.Combine(Server.MapPath("/Upload"), fileName);
                image.SaveAs(path);
                string pathReturn = ("/Upload/" + fileName);

                return Json(pathReturn, JsonRequestBehavior.AllowGet);
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Agent
        public ActionResult NewAgent()
        {
            return PartialView();
        }
        public ActionResult ListAgent()
        {
            ViewBag.HighLightAgent = "active";
            return PartialView();
        }
        public ActionResult EditAgent()
        {
            return PartialView();
        }

        public ActionResult NewAgentVehicle()
        { return PartialView(); }

        public ActionResult GetListAgents(Guid memberId)
        {
            List<ResultAgent> listAgentResult = new List<ResultAgent>();
            var listAgent = agentService.Get();
            var listVehicle = vehicleService.Get();
            var listAgentVehicle = agentVehicleService.Get();

            listAgentResult = (from agent in listAgent
                               join agentvehicle in listAgentVehicle on agent.Id equals agentvehicle.AgentId into A
                               from a in A.DefaultIfEmpty()
                               where agent.MemberId.Equals(memberId)
                               select new ResultAgent()
                               {
                                   Agent = agent != null ? agent : null,
                                   VehicleId = a != null && a.VehicleId != null ? a.VehicleId : Guid.Empty
                               }).ToList();
            listAgentResult = (from a in listAgentResult
                               join vehicle in listVehicle on a.VehicleId equals vehicle.Id into A
                               from k in A.DefaultIfEmpty()
                               select new ResultAgent()
                               {
                                   Id = a.Id,
                                   Agent = a.Agent,
                                   FirstName = a.FirstName , 
                                   ActiveDate = a.ActiveDate,
                                   ChaufferLic = a.ChaufferLic,
                                   HomePhone=a.HomePhone,
                                   LastName=a.LastName,
                                   MobilePhone=a.MobilePhone,
                                   Status=a.Status,
                                  
                                   VehicleName = k != null && k.Id != null ? k.LicenseNumber : string.Empty
                               }).ToList();
            return Json(listAgentResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertAgent(Agent agent, List<AgentVehicle> AgentVehicleList)
        {
            if (agent != null)
            {
                agent.Id = Guid.NewGuid();
                agentService.Add(agent);
                if (AgentVehicleList != null)
                {
                    foreach (var ah in AgentVehicleList)
                    {
                        ah.AgentId = agent.Id;
                        agentVehicleService.Add(ah);
                    }
                }
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        public ActionResult UpdateAgent(Agent agent, List<ResultAgentVehilce> AgentVehicleList)
        {
            if (agent != null)
            {
                agentService.Update(agent);
                if (AgentVehicleList != null)
                {
                    foreach (var ah in AgentVehicleList)
                    {
                        if (ah.IsNew)
                        {
                            ah.AgentVehicle.Id = Guid.NewGuid();
                            ah.AgentVehicle.AgentId = agent.Id;
                            agentVehicleService.Add(ah.AgentVehicle);
                        }
                    }
                }
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        public ActionResult DelAgent(Agent agent)
        {
            try
            {
                if (agent != null)
                {
                    agentService.Remove(agent.Id);
                    return Json(true);
                }
                return Json(false);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }


        [HttpPost]
        public ActionResult GetListVehicleByAgentId(Guid agentId)
        {
            List<ResultAgentVehilce> list = new List<ResultAgentVehilce>();

            var listAgentVehicle = agentVehicleService.Get();
            var listvehicle = vehicleService.Get();

            list = (from a in listAgentVehicle
                    join vehicle in listvehicle on a.VehicleId equals vehicle.Id
                    where a.AgentId.Equals(agentId)
                    select new ResultAgentVehilce()
                    {
                        AgentVehicle = a,
                        LicenseNumber = vehicle.LicenseNumber,
                        IsNew = false
                    }
           ).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DelAgentVehicle(AgentVehicle agentvehicle)
        {
            try
            {
                if (agentvehicle != null)
                {
                    agentVehicleService.Remove(agentvehicle.Id);
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

        #region Vehicle
        public ActionResult Vehicle()
        {
            ViewBag.HighLightVehicle = "active";
            return PartialView();
        }

        [HttpPost]
        public ActionResult GetAllVehicles()
        {
            var lstVehicles = vehicleService.Get();
            return Json(lstVehicles, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Get Member's Vehiceles
        /// </summary>
        /// <param name="memberId">MemberId</param>
        /// <returns>List of Vehicle</returns>
        [HttpPost]
        public JsonResult GetVehicles(Guid memberId)
        {
            var lstMembers = memberService.GetMemberVehicles(memberId);
            
            return Json(lstMembers, JsonRequestBehavior.AllowGet);
        }

        public ActionResult NewVehicle()
        {
            return PartialView();
        }


        [HttpPost]
        public ActionResult NewVehicle(Vehicle vehicle)
        {
            if (vehicle != null)
            {
                vehicle.Id = Guid.NewGuid();
                vehicleService.Add(vehicle);
                return Json(true);
            }
            return Json(false);
        }

        public ActionResult EditVehicle()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult EditVehicle(Vehicle vehicle)
        {
            vehicleService.Update(vehicle);
            return Json(true);
        }


        [HttpPost]
        public JsonResult DeleteVehicle(Vehicle vehicle)
        {
            try
            {
                return Json(vehicleService.Remove(vehicle.Id));
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }


        public JsonResult GetModelYears()
        {

            List<string> years = new List<string>();
            for (int i = DateTime.Now.Year + 1; i > DateTime.Now.Year - 25; i--)
            {
                years.Add(i.ToString());
            }
            return Json(years, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetManufactures()
        {

            var result = meterManufacturerService.Get().Select(t => new MeterManufacturer()
            {
                Description = t.Description,
                Id = t.Id
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Medallions
        public ActionResult MedallionListing()
        {
            ViewBag.HighLightMedallion = "active";
            return PartialView();
        }

        public ActionResult CreateMedallion()
        {
            return PartialView();
        }

        public ActionResult EditMedallion()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult GetMemberMedallions(Guid memberId)
        {
            var result = memberService.GetMemberMedallions(memberId).Select(t =>
                new Medallion()
                {
                    Id = t.Id,
                    MedallionNumber = t.MedallionNumber

                });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMedallions(Guid memberId)
        {
            var result = memberService.GetMedallions(memberId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMedallionsRefer(Guid memberId)
        {
            var result = memberService.GetMedallions(memberId);
            var finalresult = (from medal in result
                               group medal by medal.MedallionId into g
                               select new MemberMedallionVewModel()
                               {
                                   MedallionId = g.First().MedallionId,
                                   Balance = g.First().Balance,
                                   BillingStartDate = g.First().BillingStartDate,
                                   BillingEndDate = g.First().BillingEndDate,
                                   Collision = g.First().Collision,
                                   InsuranceSurcharge = g.First().InsuranceSurcharge,
                                   DateJoined = g.First().DateJoined,
                                   MedallionNumber = g.First().MedallionNumber,
                                   UnderServed = g.First().UnderServed
                               }).ToList();

            return Json(finalresult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddMedallion(Medallion medallion)
        {
            if (medallion != null)
            {
                medallion.Id = Guid.NewGuid();
                medallion.BillingStartDate = medallion.DateJoined;

                medallionService.Add(medallion);
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        public JsonResult EditMedallion(Medallion medallion)
        {
            try
            {
                if (medallion != null)
                {
                    medallion.BillingStartDate = medallion.DateJoined;
                    medallionService.Update(medallion);
                    return Json(true);
                }
            }
            catch
            {
                return Json(false);
            }
            return Json(false);
        }

        [HttpPost]
        public ActionResult DeleteMedallion(Guid medallionId)
        {
            try
            {
                if (medallionId != null)
                {
                    medallionService.Remove(medallionId);
                    return Json(true);
                }
            }
            catch
            {
                return Json(false);
            }
            return Json(false);
        }


        public ActionResult GetVehicleAssigned(Guid memberId)
        {
            var result = new List<MemberVehicleViewModel>();
            result = memberService.GetVehicles(memberId);
            return Json(result.ToList(), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region StandardDue
        public ActionResult StandardDueListing()
        {
            ViewBag.HighLightStandardDue = "active";
            return PartialView();
        }

        public ActionResult CreateStandardDue()
        {
            return PartialView();
        }

        public ActionResult EditStandardDue()
        {
            return PartialView();
        }


        public ActionResult GetStandardDues(Guid memberId)
        {
            var listStandardDue = standardDueService.Get().Where(c => c.MemberId == memberId).ToList();
            return Json(listStandardDue, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddStandardDue(StandardDue standardDue)
        {
            if (standardDue != null)
            {
                standardDue.Id = Guid.NewGuid();

                standardDueService.Add(standardDue);
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        public JsonResult EditStandardDue(StandardDue standardDue)
        {
            if (standardDue != null)
            {
                var item = standardDueService.Get(standardDue.Id);
                item.Dues = standardDue.Dues;
                item.StartDate = standardDue.StartDate;
                standardDueService.Update(item);
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        public ActionResult DeleteStandardDue(StandardDue standardDue)
        {
            try
            {
                if (standardDue != null)
                {
                    standardDueService.Remove(standardDue.Id);
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

        #region Universal Agent Record
        public ActionResult UniversalAgentRecord()
        {
            ViewBag.AgentActive = "start active";
            return View();
        }

        public ActionResult GetUniversalAgentList()
        {
            var result = _universalAgentRecordService.Get();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Refresh(List<UniversalAgentRecord> list)
        {
            if (list != null && list.Count > 0)
            {
                return Json(_universalAgentRecordService.BulkInsertUniversal(list), JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RTAUpload()
        {
            ViewBag.AgentActive = "start active";
            return View();
        }

        public ActionResult MobilityUpload()
        {
            ViewBag.AgentActive = "start active";
            return View();
        }

        public ActionResult GetRTAList()
        { 
            var list = rtaService.Get();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMobilityList()
        {
            var list = mobilityService.Get();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
                 
        public ActionResult RTAUploadFile(HttpPostedFileBase sheet)
        {
            if (Request.Files[0].ContentLength > 0)
            {
                sheet = Request.Files[0];
                string fileExt = Path.GetExtension(sheet.FileName);
                var fileName = String.Format("{0:MMddyyyyhhmmss}{1}", DateTime.Now, fileExt);
                var path = Path.Combine(Server.MapPath("/Upload"), fileName);
                sheet.SaveAs(path);
                string pathReturn = ("/Upload/" + fileName);
                if (fileExt.Equals(".xls") || fileExt.Equals(".xlsx") || fileExt.Equals(".XLS") || fileExt.Equals(".XLSX"))
                {
                    //after uploaded sucessed 
                    //read file excel for get data from there.
                   var list =  InsertRTA(path, fileExt);
                   if (list != null)
                   {
                     return  Json(list, JsonRequestBehavior.AllowGet);
                   }
                }
                else { throw new Exception("Extension is not support"); }
            }
            throw new Exception("File Invalid.");
             
        }


        [HttpPost]
        public ActionResult MobilityUploadFile(HttpPostedFileBase sheet)
        {
            if (Request.Files[0].ContentLength > 0)
            {
                sheet = Request.Files[0];
                string fileExt = Path.GetExtension(sheet.FileName);
                var fileName = String.Format("{0:MMddyyyyhhmmss}{1}", DateTime.Now, fileExt);
                var path = Path.Combine(Server.MapPath("/Upload"), fileName);
                sheet.SaveAs(path);
                string pathReturn = ("/Upload/" + fileName);
                if (fileExt.Equals(".xls") || fileExt.Equals(".xlsx") || fileExt.Equals(".XLS") || fileExt.Equals(".XLSX"))
                {
                    //after uploaded sucessed 
                    //read file excel for get data from there.
                   var list  = InsertMobiltiy(path, fileExt);
                   if (list != null)
                   {
                      return Json(pathReturn, JsonRequestBehavior.AllowGet);
                   }
                }
                else { throw new Exception("Extension is not support"); }               
            }

            throw new Exception("File Invalid");
        }

        private List<RTA> InsertRTA(string excelFile, string fileExt)
        {
            OleDbConnection objConn = null;
            System.Data.DataTable dt = null;

            try
            {
                // Connection String. Change the excel file to the file you
                // will search.
                String connString = string.Empty;
                if (fileExt == ".xls" || fileExt == "XLS")
                {
                    connString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source='" + excelFile + "'" + "; Extended Properties ='Excel 8.0;HDR=Yes'";
                }
                else if (fileExt == ".xlsx" || fileExt == "XLSX")
                {
                    connString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + excelFile + "; Extended Properties ='Excel 8.0;HDR=Yes'";
                }
                // Create connection object by using the preceding connection string.
                objConn = new OleDbConnection(connString);
                // Open connection with the database.
                try
                {
                    objConn.Open();
                    // Get the data table containg the schema guid.
                    dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    if (dt == null)
                    {
                        throw new Exception("File invalid!");
                    }

                    String[] excelSheets = new String[dt.Rows.Count];
                    int i = 0;

                    // Add the sheet name to the string array.
                    foreach (DataRow row in dt.Rows)
                    {
                        excelSheets[i] = row["TABLE_NAME"].ToString();
                        i++;
                    }

                    // Loop through all of the sheets if you want too...
                    if (excelSheets.Length <= 0) throw new Exception("Cannot find any sheet!");
                    for (int j = 0; j < 1; j++)
                    {
                        DataTable table = new DataTable();
                        string query = String.Format("Select * from [{0}]", excelSheets[j]);
                        OleDbDataAdapter adp = new OleDbDataAdapter(query, objConn);
                        adp.Fill(table);

                        if (table.Rows != null && table.Rows.Count > 0)
                        {
                            //get data for insert to database 
                            if (table.Columns != null && table.Columns.Count == 14)
                            {
                                List<RTA> listRTA = new List<RTA>();
                                foreach (DataRow row in table.Rows)
                                {
                                    RTA rta = new RTA();
                                    rta.Id = Guid.NewGuid();
                                    if (row[0] != null)
                                    {
                                        DateTime minValue = DateTime.MinValue;
                                        DateTime.TryParse(row[0].ToString(), out minValue);
                                        if (minValue > DateTime.MinValue)
                                        {
                                            rta.DateRun = (DateTime)row[0];
                                        }
                                    }

                                    rta.ClientName = row[1] != null ? row[1].ToString() : string.Empty;
                                    rta.LicenseNumber = row[2] != null ? row[2].ToString() : string.Empty;
                                    rta.PickupAddress = row[3] != null ? row[3].ToString() : string.Empty;
                                    if (row[4] != null)
                                    {
                                        rta.DueTime =String.Format("{0:hh:mm tt}",DateTime.Parse(row[4].ToString()));
                                    }
                                    if (row[5] != null)
                                    {
                                        rta.PUTime = String.Format("{0:hh:mm tt}", DateTime.Parse(row[5].ToString()));  
                                    }
                                    if (row[6] != null)
                                    {
                                        rta.DOTime = String.Format("{0:hh:mm tt}", DateTime.Parse(row[6].ToString()));
                                    }
                                    rta.DropOffAddress = row[7] != null ? row[7].ToString() : string.Empty;
                                    rta.Phone = row[8] != null ? row[8].ToString() : string.Empty;
                                    rta.CAB = row[9] != null ? row[9].ToString() : string.Empty;
                                    rta.CL = row[10] != null ? row[10].ToString() : string.Empty;
                                    decimal amount = 0;
                                    decimal.TryParse(row[11] != null ? row[11].ToString() : string.Empty, out amount);
                                    rta.Amount = amount;
                                    if (row[12] != null)
                                    {
                                        DateTime minValue = DateTime.MinValue;
                                        DateTime.TryParse(row[12].ToString(), out minValue);
                                        if (minValue > DateTime.MinValue)
                                        {
                                            rta.PaidDate = minValue;
                                        }
                                    }
                                    rta.Confirmed = row[13] != null ? row[13].ToString() : string.Empty;

                                    listRTA.Add(rta);
                                }

                                if (listRTA.Count > 0)
                                {
                                    //bulk insert to database
                                    rtaService.BulkInsertRTA(listRTA);
                                }
                                return listRTA;
                            }
                        }
                    }
                    return null;
                }
                catch (Exception ce)
                {
                    throw ce;
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                // Clean up.
                if (objConn != null)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

        private List<Mobility> InsertMobiltiy(string excelFile, string fileExt)
        {
            OleDbConnection objConn = null;
            System.Data.DataTable dt = null;

            try
            {
                // Connection String. Change the excel file to the file you
                // will search.
                String connString = string.Empty;
                if (fileExt == ".xls" || fileExt == "XLS")
                {
                    connString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source='" + excelFile + "'" + "; Extended Properties ='Excel 8.0;HDR=Yes'";
                }
                else if (fileExt == ".xlsx" || fileExt == "XLSX")
                {
                    connString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + excelFile + "; Extended Properties ='Excel 8.0;HDR=Yes'";
                }
                // Create connection object by using the preceding connection string.
                objConn = new OleDbConnection(connString);
                // Open connection with the database.
                try
                {
                    objConn.Open();
                    // Get the data table containg the schema guid.
                    dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    if (dt == null)
                    {
                        throw new Exception("File Invalid!");
                    }
                   
                    String[] excelSheets = new String[dt.Rows.Count];
                    int i = 0;

                    // Add the sheet name to the string array.
                    foreach (DataRow row in dt.Rows)
                    {
                        excelSheets[i] = row["TABLE_NAME"].ToString();
                        i++;
                    }
                    if (excelSheets.Length <= 0) throw new Exception("Cannot find any sheet!");
                    // Loop through all of the sheets if you want too...
                    for (int j = 0; j < excelSheets.Length; j++)
                    {
                        DataTable table = new DataTable();
                        string query = String.Format("Select * from [{0}]", excelSheets[j]);
                        OleDbDataAdapter adp = new OleDbDataAdapter(query, objConn);
                        adp.Fill(table);

                        if (table.Rows != null && table.Rows.Count > 0)
                        {
                            //get data for insert to database 
                            if (table.Columns != null && table.Columns.Count >= 17)
                            {
                                List<Mobility> listMobility = new List<Mobility>();
                                foreach (DataRow row in table.Rows)
                                {
                                    Mobility mobi = new Mobility();
                                    mobi.Id = Guid.NewGuid();
                                    if (row[0] != null)
                                    {
                                        DateTime minValue = DateTime.MinValue;
                                        DateTime.TryParse(row[0].ToString(), out minValue);
                                        if (minValue > DateTime.MinValue)
                                        {
                                            mobi.DateRun = minValue;
                                        }
                                    }

                                    mobi.ClientName = row[1] != null ? row[1].ToString() : string.Empty;
                                    mobi.LicenseNumber = row[2] != null ? row[2].ToString() : string.Empty;
                                    mobi.MD = row[3] != null ? row[3].ToString() : string.Empty;
                                    mobi.PickupAddress = row[4] != null ? row[4].ToString() : string.Empty;

                                    if (row[5] != null)
                                    {
                                        mobi.DueTime = String.Format("{0:hh:mm tt}", DateTime.Parse(row[5].ToString()));  
                                    }
                                    if (row[6] != null)
                                    {
                                        mobi.PUTime = String.Format("{0:hh:mm tt}", DateTime.Parse(row[6].ToString()));
                                    }
                                    mobi.DropOffAddress = row[7] != null ? row[7].ToString() : string.Empty;
                                    mobi.Phone = row[8] != null ? row[8].ToString() : string.Empty;
                                    if (row[9] != null)
                                    {
                                        mobi.DOTime = String.Format("{0:hh:mm tt}", DateTime.Parse(row[9].ToString()));                                   
                                    }

                                    mobi.Early = row[10] != null ? row[10].ToString() : string.Empty;
                                    mobi.Late = row[11] != null ? row[11].ToString() : string.Empty;
                                    mobi.NS = row[12] != null ? row[12].ToString() : string.Empty;
                                    mobi.Hold = row[13] != null ? row[13].ToString() : string.Empty;

                                    mobi.CAB = row[14] != null ? row[14].ToString() : string.Empty;
                                    mobi.CL = row[15] != null ? row[15].ToString() : string.Empty;


                                    decimal amount = 0;
                                    decimal.TryParse(row[16] != null ? row[16].ToString() : string.Empty, out amount);
                                    mobi.Amount = amount;

                                    listMobility.Add(mobi);
                                }

                                if (listMobility.Count > 0)
                                {
                                    //bulk insert to database
                                    mobilityService.BulkInsertMobility(listMobility);
                                }
                                return listMobility;
                            }
                        }
                    }
                    return null;
                }
                catch (Exception ce)
                {
                    throw ce;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Clean up.
                if (objConn != null)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        #endregion
    }
}