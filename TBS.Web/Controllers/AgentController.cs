using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TBS.Web.Utility;
using TDevs.Domain;
using TDevs.Domain.ViewModel;
using TDevs.Services;

namespace TBS.Web.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.Required)]    
    public class AgentController : TbsControllerBase
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

        private readonly IInsuranceDepositAgentService insuranceDepositService;
        private readonly IAutoLoanSetupAgentService autoLoanSetupService;
        private readonly IAccountReceivableAgentService accountReceivableService;
        private readonly ISavingDepositAgentService savingDepositService;

        public AgentController(IMemberService memberService, IAgentService agentService, IContactService contactService, IMedallionService medallionService,
            IVehicleService vehicleService, IMeterManufacturerService meterManufacturerService, IStockholderService stockholderService, IAgentVehicleService agentVehicleService, IStandardDuesService standardDueService,
            IVehicleMakeService vehicleMakeService, IModelYearInsuranceService modelYearInsuranceService, IVehicleModelService vehicleModelService,
            IInsuranceDepositAgentService insuranceDepositService, IAutoLoanSetupAgentService autoLoanSetupService, IAccountReceivableAgentService accountReceivableService,
            ISavingDepositAgentService savingDepositService)
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

            this.insuranceDepositService = insuranceDepositService;
            this.accountReceivableService = accountReceivableService;
            this.autoLoanSetupService = autoLoanSetupService;
            this.savingDepositService = savingDepositService;
        }

        public ActionResult Search()
        {
            ResetNavigator();
            ViewBag.AgentActive = "start active";
            return View();
        }

        public ActionResult SearchAgent()
        {
            return PartialView();
        }

        public ActionResult NewAgent()
        { return PartialView(); }

        public ActionResult EditAgent()
        {
            return PartialView();
        }

        public ActionResult InsuranceDepositAgent()
        {
            ViewBag.InsuranceDeposit = "active";
            return PartialView();
        }

        public ActionResult AccountReceivableAgent()
        {
            ViewBag.AccountReceivable = "active";
            return PartialView();
        }

        public ActionResult SavingDepositAgent()
        {
            ViewBag.SavingDeposit = "active";
            return PartialView();
        }

        public ActionResult AutoLoanSetupAgent()
        {
            ViewBag.AutoLoanSetup = "active";
            return PartialView();
        }

        [HttpPost]
        public ActionResult GetAgentVehicleListByAgentId(Guid agentId)
        {
            var list = agentVehicleService.GetListAgentVehiclesByAgentId(agentId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetInsuranceDepositByAgentId(Guid agentId)
        {
            var idalist = insuranceDepositService.Get().Where(c => c.AgentId.Equals(agentId)).ToList();
            return Json(idalist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetAccountReceivableByAgentId(Guid agentId)
        {
            var idalist = accountReceivableService.Get().Where(c => c.AgentId.Equals(agentId)).ToList();
            return Json(idalist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetSavingDepositByAgentId(Guid agentId)
        {
            var idalist = savingDepositService.Get().Where(c => c.AgentId.Equals(agentId)).ToList();
            return Json(idalist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetAutoLoanSetupByAgentId(Guid agentId)
        {
            var idalist = autoLoanSetupService.Get().Where(c => c.AgentId.Equals(agentId)).ToList();
            return Json(idalist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]        
        public ActionResult SearchAgentByCriterial(AgentCriterial criterial)
        {
            var listResult = agentService.SearchAgentByCriterial(criterial);

            return Json(listResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetAgentById(Guid id)
        {
            return Json(agentService.Get(id), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult NewAgent(Agent agent, List<AgentVehicle> AgentVehicleList)
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
        public ActionResult EditAgent(Agent agent, List<ResultAgentVehilce> AgentVehicleList)
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
        public ActionResult DeleteAgent(Agent agent)
        {
            try
            {
                agent.IsDeleted = true;
                agentService.Update(agent);
                return Json(true);
            }
            catch
            {

            }
            return Json(false);
        }

        [HttpPost]
        public ActionResult CheckDuplicateAgent(string numberlicense)
        {
            bool isDuplicate = false;
            try
            {
                isDuplicate = agentService.Get().Where(c => c.DriverID.Trim().ToUpper().Equals(numberlicense.Trim().ToUpper())).FirstOrDefault() != null;
            }
            catch
            {
                isDuplicate = true;
            }
            return Json(isDuplicate, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CheckDuplicateEditAgent(string numberlicense, Guid id)
        {
            bool isDuplicate = false;
            try
            {
                isDuplicate = agentService.Get().Where(c => c.DriverID.Trim().ToUpper().Equals(numberlicense.Trim().ToUpper()) && c.Id != id).FirstOrDefault() != null;
            }
            catch
            {
                isDuplicate = true;
            }
            return Json(isDuplicate, JsonRequestBehavior.AllowGet);
        }
    }
}