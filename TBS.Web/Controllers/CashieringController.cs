using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDevs.Domain;
using TDevs.Domain.ViewModel;
using System.Data.Entity;
using TDevs.Services;

namespace TBS.Web.Controllers
{
    public class CashieringController : Controller
    {
        private readonly IInsuranceDepositService insuranceDepositService;
        private readonly ICCSystemAirtimeService ccsystemAirtimeService;

        private readonly IAccountReceivableService accountReceivableService;
        private readonly ISavingDepositService savingDepositService;
        private readonly IMedallionService medallionService;
        private readonly ITransactionHistoryService transactionHistoryService;
        private readonly IBillService billService;
        private readonly IMemberService memberService;
        private readonly ICompanyService companyService;
        private readonly ILoanService loanService;
        public CashieringController(IInsuranceDepositService insuranceDepositService, ICCSystemAirtimeService ccsystemAirtimeService,
            IAccountReceivableService accountReceivableService, ISavingDepositService savingDepositService, IMedallionService medallionService,
            ITransactionHistoryService transactionHistoryService, IBillService billService, IMemberService memberService, ICompanyService companyService, ILoanService loanService)
        {
            this.insuranceDepositService = insuranceDepositService;
            this.ccsystemAirtimeService = ccsystemAirtimeService;
            this.accountReceivableService = accountReceivableService;
            this.savingDepositService = savingDepositService;
            this.medallionService = medallionService;
            this.transactionHistoryService = transactionHistoryService;
            this.billService = billService;
            this.memberService = memberService;
            this.companyService = companyService;
            this.loanService = loanService;
        }

        #region Generate View

        public ActionResult InsuranceDeposit()
        {
            ViewBag.InsuranceDeposit = "active";
            return PartialView();
        }

        public ActionResult CCSystemAirtime()
        {
            ViewBag.CCSystemAirtime = "active";
            return PartialView();
        }

        public ActionResult MedallionLoanSetup()
        {
            ViewBag.MedallionLoanSetup = "active";
            return PartialView();
        }

        public ActionResult AutoLoanSetup()
        {
            ViewBag.AutoLoanSetup = "active";
            return PartialView();
        }

        public ActionResult AccountReceivable()
        {
            ViewBag.AccountReceivable = "active";
            return PartialView();
        }

        public ActionResult SavingDeposit()
        {
            ViewBag.SavingDeposit = "active";
            return PartialView();
        }

        public ActionResult AccountSummary()
        {
            ViewBag.AccountSummary = "active";
            return PartialView();
        }

        public ActionResult EditTransactionHistory()
        {
            return PartialView();
        }

        public ActionResult MedallionSummary()
        {
            ViewBag.AccountSummary = "active";
            return PartialView();
        }

        public ActionResult PaymentHistory() { return PartialView(); }


        public ActionResult NewCashierMember() { return PartialView(); }

        #endregion

        #region Insurance Deposit
        public ActionResult GetListInsuranceDepositByMemberId(Guid memberId)
        {
            var insuranceDepositlist = insuranceDepositService.Get().Where(c => c.MemberId.Equals(memberId)).ToList();
            return Json(insuranceDepositlist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetInsuranceDepositByMemberId(Guid memberId)
        {
            var insuranceDeposit = insuranceDepositService.Get().Where(c => c.MemberId.Equals(memberId)).FirstOrDefault();
            return Json(insuranceDeposit, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveInsuranceDeposit(InsuranceDeposit insuranceDeposit)
        {
            if (insuranceDeposit != null)
            {
                if (insuranceDeposit.Id != null && insuranceDeposit.Id != Guid.Empty)
                {
                    //case edit 
                    insuranceDepositService.Update(insuranceDeposit);
                    return Json(true);
                }
                else
                {
                    insuranceDeposit.Id = Guid.NewGuid();
                    //case insert 
                    insuranceDepositService.Add(insuranceDeposit);
                    return Json(true);
                }
            }
            return Json(false);
        }
        #endregion

        #region CCSystem Airtime
        public ActionResult GetListCCSystemAirtimeByMemberId(Guid memberId)
        {
            var ccsystemAirtime = ccsystemAirtimeService.Get().Where(c => c.MemberId.Equals(memberId));
            return Json(ccsystemAirtime, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCCSystemAirtimeByMemberId(Guid memberId)
        {
            var ccsystemAirtime = ccsystemAirtimeService.Get().Where(c => c.MemberId.Equals(memberId)).FirstOrDefault();
            return Json(ccsystemAirtime, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveCCSystemAirtime(CCSystemAirtime ccsystemAirtime)
        {
            if (ccsystemAirtime != null)
            {
                if (ccsystemAirtime.Id != null && ccsystemAirtime.Id != Guid.Empty)
                {
                    //case edit 
                    ccsystemAirtimeService.Update(ccsystemAirtime);
                    return Json(true);
                }
                else
                {
                    ccsystemAirtime.Id = Guid.NewGuid();
                    //case insert 
                    ccsystemAirtimeService.Add(ccsystemAirtime);
                    return Json(true);
                }
            }
            return Json(false);
        }
        #endregion

        #region Savings Deposit
        [HttpPost]
        public ActionResult SaveSavingDeposit(SavingDeposit savingDeposit)
        {
            if (savingDeposit != null)
            {
                if (savingDeposit.Id != null && savingDeposit.Id != Guid.Empty)
                {
                    //case edit 
                    savingDepositService.Update(savingDeposit);
                    return Json(true);
                }
                else
                {
                    savingDeposit.Id = Guid.NewGuid();
                    //case insert 
                    savingDepositService.Add(savingDeposit);
                    return Json(true);
                }
            }
            return Json(false);
        }

        public ActionResult GetListSavingDepositMemberId(Guid memberId)
        {
            var savingDeposit = savingDepositService.Get().Where(c => c.MemberId.Equals(memberId));
            return Json(savingDeposit, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSavingDepositByMemberId(Guid memberId)
        {
            var savingDeposit = savingDepositService.Get().Where(c => c.MemberId.Equals(memberId)).FirstOrDefault();
            return Json(savingDeposit, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Account Receivable
        public ActionResult GetListAccountReceivableMemberId(Guid memberId)
        {
            var accountReceivable = accountReceivableService.Get().Where(c => c.MemberId.Equals(memberId));
            return Json(accountReceivable, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAccountReceivableMemberId(Guid memberId)
        {
            var accountReceivable = accountReceivableService.Get().Where(c => c.MemberId.Equals(memberId)).FirstOrDefault();
            return Json(accountReceivable, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveAccountReceivable(AccountReceivable accountReceivable)
        {
            if (accountReceivable != null)
            {
                if (accountReceivable.Id != null && accountReceivable.Id != Guid.Empty)
                {
                    //case edit 
                    accountReceivableService.Update(accountReceivable);
                    return Json(true);
                }
                else
                {
                    accountReceivable.Id = Guid.NewGuid();
                    //case insert 
                    accountReceivableService.Add(accountReceivable);
                    return Json(true);
                }
            }
            return Json(false);
        }
        #endregion

        #region Account Summary
        /// <summary>
        ///get list medallion what is the member hire 
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public ActionResult GetMedallionSummaryListByMemberId(Guid memberId)
        {
            var listmedallionLoansetup = medallionService.GetMedallionLoanByMemberId(memberId);
            return Json(listmedallionLoansetup, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPaymentHistoryByMemberMedallionId(Guid memberId, Guid medallionId)
        {
            var list = transactionHistoryService.GetListTransactionByMedallionMemberId(memberId, medallionId);
            list = list.OrderBy(c => c.TransactionDate).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBillsByMemberandMedallionId(Guid memberId, Guid medallionId)
        {
            var Listbill = billService.GetBillByMemberAndMedallionId(memberId, medallionId);
            return Json(Listbill, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetNewBill(Guid memberId, Guid medallionId)
        {
            var today = DateTime.Now;
            var member = memberService.Get(memberId);
            var medallion = medallionService.Get(medallionId);

            //initial Object to view to New Bill
            BillMedallionViewModel medalBill = new BillMedallionViewModel();
            medalBill.Bill = new Bill();
            medalBill.Medallion = new Medallion();

            medalBill.Bill.TotalPaid = 0;
            medalBill.Bill.MedallionId = medallion.Id;
            //set value for Member
            medalBill.Member = new Member()
            {
                Id = member.Id,
                AccountNumber = member.AccountNumber,
                Name = member.Name
            };
            //set value for Medallion
            medalBill.Medallion = new Medallion()
            {
                Id = medallion.Id,
                MedallionNumber = medallion.MedallionNumber
            };

            //get all loan of a member and  medallion specificate
            var loanlist = loanService.GetLoanListByMemberAndMedallion(memberId, medallionId);
            var insuranceDeposit = insuranceDepositService.GetByMedallionAndMemberId(memberId, medallionId);
            var ccsys = ccsystemAirtimeService.GetByMedallionAndMemberId(memberId, medallionId);
            var accReceivable = accountReceivableService.GetByMedallionAndMemberId(memberId, medallionId);
            var saveDeposit = savingDepositService.GetByMedallionAndMemberId(memberId, medallionId);

            //get all balance  of loans
            medalBill.Bill.Balance = (insuranceDeposit != null ? insuranceDeposit.CurrentBalance : 0)
                + (ccsys != null ? ccsys.Airtime : 0) + (accReceivable != null ? accReceivable.CurrentBalance : 0);
            decimal calculateMonthlypay = 0;
            //get balance from loan
            if (loanlist != null && loanlist.Count > 0)
            {
                foreach (Loan l in loanlist)
                {
                    medalBill.Bill.Balance += l.CurrentBalance;
                    calculateMonthlypay += l.CalculatedMonthlyPayment;
                }
            }

            //pull data of loans to pay
            medalBill.Bill.AccountReceivableAmount = accReceivable != null ? accReceivable.MonthlyPayment : 0;
            medalBill.Bill.AssociationDueAmount = 0;
            medalBill.Bill.Loan = calculateMonthlypay;
            medalBill.Bill.CCSystemAirtimeAmount = ccsys != null ? ccsys.Airtime : 0;
            medalBill.Bill.InsuranceDepositAmount = insuranceDeposit != null ? insuranceDeposit.WeeklyPayment * 4 : 0;

            //generate receiptNumber
            string recieptNumber = String.Format("{0:yyhhmmss}", DateTime.Now);
            medalBill.Bill.RecieptNumber = int.Parse(recieptNumber);

            //get all bill what are paid
            var Listbill = billService.GetBillByMemberAndMedallionId(memberId, medallionId);
            // order by NextPayment to get last bill
            Listbill = Listbill.OrderBy(c => c.NextPayment).ToList();

            //First time to payment
            if (Listbill != null && Listbill.Count == 0)
            {
                if (medallion != null)
                {
                    medalBill.Bill.StartDate = medallion.BillingStartDate;
                    var nextPayment = today.AddMonths(1);
                    if (nextPayment > medallion.BillingEndDate)
                    {
                        nextPayment = medallion.BillingEndDate;
                        medalBill.Bill.EndDate = nextPayment;
                        medalBill.Bill.NextPayment = nextPayment;
                    }
                    else
                    {
                        medalBill.Bill.EndDate = nextPayment;
                        medalBill.Bill.NextPayment = nextPayment.AddDays(1);
                    }
                    medalBill.Bill.MemberId = memberId;
                }
            }
            //second Bill
            else
            {
                //get last bill from listbill 
                var bill = Listbill.FirstOrDefault();
                if (bill != null)
                {
                    medalBill.Bill.StartDate = bill.NextPayment;
                    var nextPayment = bill.NextPayment.AddMonths(1);
                    //check 
                    if (nextPayment > medallion.BillingEndDate)
                    {
                        medalBill.Bill.EndDate = medallion.BillingEndDate;
                        medalBill.Bill.NextPayment = medallion.BillingEndDate;
                    }
                    else
                    {
                        medalBill.Bill.EndDate = nextPayment;
                        medalBill.Bill.NextPayment = nextPayment.AddMonths(1);
                    }
                    medalBill.Bill.MemberId = memberId;
                }
            }

            //calculate late fee 
            var companySetting = companyService.Get().FirstOrDefault();
            if (companySetting != null)
            {
                int allowLateDate = companySetting.DefaultLateBreak > 0 ? (int)companySetting.DefaultLateBreak : 0;

                var totalDateLate = today.Subtract(medalBill.Bill.EndDate).Days;
                if (totalDateLate > allowLateDate)
                {
                    medalBill.Bill.StatysPastDue = "Past Due";
                    //calculate late fee
                    decimal feeLateOneDay = (companySetting.DefaultLateCharge / 7);
                    decimal totalFeeLate = Math.Round(feeLateOneDay * totalDateLate, 2, MidpointRounding.AwayFromZero);
                    medalBill.Bill.LateFees = totalFeeLate;
                }
                else if (totalDateLate < allowLateDate && totalDateLate > 1)
                {
                    medalBill.Bill.StatysPastDue = "Due";
                }
                else
                {
                    medalBill.Bill.StatysPastDue = "Current";
                }

            }
            return Json(medalBill, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get2PartSummary(Guid memberId, Guid medallionId)
        {
            TwoPart twoPart = new TwoPart()
            {
                Deposits = new List<AllDeposit>(),
                Loans = new List<AllLoan>(),
                Receivable = new List<AllReceivable>()
            };

            {
                //get medallionLoan 
                var loans = loanService.GetLoanListByMemberAndMedallion(memberId, medallionId);
                var insuranceDeposit = insuranceDepositService.GetByMedallionAndMemberId(memberId, medallionId);
                var ccsys = ccsystemAirtimeService.GetByMedallionAndMemberId(memberId, medallionId);
                var accReceivable = accountReceivableService.GetByMedallionAndMemberId(memberId, medallionId);
                var saveDeposit = savingDepositService.GetByMedallionAndMemberId(memberId, medallionId);



                if (loans != null && loans.Count > 0)
                {
                    AllLoan metricloan = new AllLoan();
                    foreach (Loan l in loans)
                    {
                        metricloan.Balance += l.CurrentBalance;
                        metricloan.InterestedPaid += l.TotalInterestPaid;
                        metricloan.Interest += l.InterestRate;
                        metricloan.PrincipalPaid += l.TotalPrincipalPaid;
                        metricloan.Loan += l.LoanAmount;
                        metricloan.TotalPaid += l.TotalPaid;
                        metricloan.Title = "Loan:";
                    }
                    twoPart.Loans.Add(metricloan);
                }
                else
                {
                    AllLoan metricMedallion = new AllLoan()
                    {
                        Balance = 0,
                        Interest = 0,
                        InterestedPaid = 0,
                        PrincipalPaid = 0,
                        Loan = 0,
                        TotalPaid = 0,
                        Title = "Loan:"
                    };
                    twoPart.Loans.Add(metricMedallion);
                }

                //Deposit 
                if (insuranceDeposit != null)
                {
                    AllDeposit metricInsur = new AllDeposit()
                    {
                        Balance = insuranceDeposit.CurrentBalance,
                        Deposit = insuranceDeposit.DepositAmount,
                        TotalPaid = insuranceDeposit.TotalPaid,
                        WeeklyPayment = insuranceDeposit.WeeklyPayment,
                        Title = "Insurance Deposit:"
                    };
                    twoPart.Deposits.Add(metricInsur);
                }
                else
                {
                    AllDeposit metricInsur = new AllDeposit()
                    {
                        Balance = 0,
                        Deposit = 0,
                        TotalPaid = 0,
                        WeeklyPayment = 0,
                        Title = "Insurance Deposit:"
                    };
                    twoPart.Deposits.Add(metricInsur);
                }

                //CCSystem Airtime 
                //if (ccsys != null)
                {
                    AllDeposit metricccsys = new AllDeposit()
                    {
                        Title = "CCSystem Airtime:"
                    };
                    twoPart.Deposits.Add(metricccsys);
                }

                //Saving Deposit 
                if (saveDeposit != null)
                {
                    AllDeposit metricsavingDeposit = new AllDeposit()
                    {
                        TotalPaid = saveDeposit.TotalPaid,
                        Title = "Saving Deposit:"
                    };
                    twoPart.Deposits.Add(metricsavingDeposit);
                }
                else
                {
                    AllDeposit metricsavingDeposit = new AllDeposit()
                    {
                        TotalPaid = 0,
                        Title = "Saving Deposit:"
                    };
                    twoPart.Deposits.Add(metricsavingDeposit);
                }
                //Account Receivable
                if (accReceivable != null)
                {
                    AllReceivable metricaccReceivable = new AllReceivable()
                    {
                        Balance = accReceivable.CurrentBalance,
                        Amount = accReceivable.AccountReceivableAmount,
                        TotalPaid = accReceivable.TotalPaid,
                        MonthlyPayment = accReceivable.MonthlyPayment,
                        Title = "Account Receivable:"
                    };
                    twoPart.Receivable.Add(metricaccReceivable);
                }
                else
                {
                    AllReceivable metricaccReceivable = new AllReceivable()
                    {
                        Balance = 0,
                        Amount = 0,
                        TotalPaid = 0,
                        MonthlyPayment = 0,
                        Title = "Account Receivable:"
                    };
                    twoPart.Receivable.Add(metricaccReceivable);
                }

            }
            return Json(twoPart, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Save New Bill with new transaction
        /// </summary>
        /// <param name="bill"></param>
        /// <param name="isZeroOut"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveNewBill(Bill bill, bool isZeroOut)
        {
            try
            {
                //save to bill 
                if (bill != null)
                {
                    bill.DateReceived = DateTime.Now;
                    Guid memberId = bill.MemberId;
                    Guid medallionId = bill.MedallionId;

                    var medallion = medallionService.Get(bill.MedallionId);
                    if (bill.Id != null && bill.Id != Guid.Empty)
                    {

                        bill.UserName = User.Identity.Name;
                        billService.Update(bill);
                    }
                    else
                    {
                        bill.Id = Guid.NewGuid();
                        bill.IsZeroOut = isZeroOut;
                        bill.UserName = User.Identity.Name;
                        billService.Add(bill);
                    }
                    //we are suspecting  just have only one for each medallion
                    var loan = loanService.GetLoanListByMemberAndMedallion(memberId, medallionId).FirstOrDefault();
                    var insuranceDeposit = insuranceDepositService.GetByMedallionAndMemberId(memberId, medallionId);
                    var ccsys = ccsystemAirtimeService.GetByMedallionAndMemberId(memberId, medallionId);
                    var accReceivable = accountReceivableService.GetByMedallionAndMemberId(memberId, medallionId);
                    var saveDeposit = savingDepositService.GetByMedallionAndMemberId(memberId, medallionId);

                    //decrease balance in loan 
                    if (loan != null && bill.Loan > 0)
                    {
                        loan.TotalPaid += bill.Loan;
                        loan.CurrentBalance -= bill.Loan;
                        //decrease for total paid interested &  principal paid
                        CalculatePaidLoan(loan, bill.Loan);

                        loanService.Update(loan);
                    }

                    //decrease balance in insurance deposit 
                    if (insuranceDeposit != null && bill.InsuranceDepositAmount > 0)
                    {
                        insuranceDeposit.TotalPaid += bill.InsuranceDepositAmount;
                        insuranceDeposit.CurrentBalance -= bill.InsuranceDepositAmount;
                        insuranceDepositService.Update(insuranceDeposit);
                    }

                    //decrease balance in accReceivable 
                    if (accReceivable != null && bill.AccountReceivableAmount > 0)
                    {
                        accReceivable.TotalPaid += bill.AccountReceivableAmount;
                        accReceivable.CurrentBalance -= bill.AccountReceivableAmount;
                        accountReceivableService.Update(accReceivable);
                    }

                    if (isZeroOut)
                    {
                        //decrease balance in saveDeposit 
                        if (saveDeposit != null && bill.TotalPaid > 0)
                        {
                            saveDeposit.TotalPaid -= bill.TotalPaid;
                            savingDepositService.Update(saveDeposit);
                        }
                    }
                    else
                    {
                        //decrease balance in saveDeposit 
                        if (saveDeposit != null && bill.SavingDepositAmount > 0)
                        {
                            saveDeposit.TotalPaid += bill.SavingDepositAmount;
                            savingDepositService.Update(saveDeposit);
                        }
                    }
                    //get balance 
                    bill.Balance = loan.CurrentBalance
                         + insuranceDeposit.CurrentBalance
                         + ccsys.Airtime
                         + accReceivable.CurrentBalance;
                    billService.Update(bill);
                    //save to transactionhistory  
                    TransactionHistory trans = new TransactionHistory()
                    {
                        Id = Guid.NewGuid(),
                        BillId = bill.Id,
                        IsZeroOut = bill.IsZeroOut,
                        DateReceived = bill.DateReceived,
                        TransactionType = bill.TransactionType,
                        AccountReceivableAmount = bill.AccountReceivableAmount,
                        AssociationDueAmount = bill.AssociationDueAmount,
                        Loan = bill.Loan,
                        Balance = bill.Balance,
                        Cash = bill.Cash,
                        CCSystemAirtimeAmount = bill.CCSystemAirtimeAmount,
                        Check = bill.Check,
                        CollsionInsuranceAmount = bill.CollsionInsuranceAmount,
                        Credit = bill.Credit,
                        CreditCardAmount = bill.CreditCardAmount,
                        CreditCardFee = bill.CreditCardFee,
                        DueDate = bill.EndDate,

                        InsuranceDepositAmount = bill.InsuranceDepositAmount,
                        InsuranceSticker = bill.InsuranceSticker,
                        InsuranceSurchargeAmount = bill.InsuranceSurchargeAmount,
                        Interval = bill.Interval,
                        IsAutoCashiering = bill.IsAutoCashiering,
                        IsDeleted = false,
                        LateFees = bill.LateFees,
                        MedallionId = bill.MedallionId,
                        MedallionNumber = medallion.MedallionNumber,
                        MemberId = bill.MemberId,
                        MiscCharge = bill.MiscCharge,
                        NextPayment = bill.NextPayment,
                        RecieptNumber = bill.RecieptNumber,
                        SavingDepositAmount = bill.SavingDepositAmount,
                        Subtotal = bill.Subtotal,
                        TotalDueAmount = bill.TotalDueAmount,
                        TotalPaidAmount = bill.TotalPaidAmount,

                        UserName = bill.UserName,
                        WerkReceivableAmount = bill.WerkReceivableAmount,
                        WorkerCompensationAmount = bill.WorkerCompensationAmount,
                        TransactionDate = bill.StartDate,
                        TotalPaid = bill.TotalPaid
                    };
                    //save to transaction history
                    transactionHistoryService.Add(trans);
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
        public ActionResult SaveEditBill(Bill bill, Guid transactionId)
        {
            try
            {
                if (bill != null && transactionId != Guid.Empty)
                {
                    var memberId = bill.MemberId;
                    var medallionId = bill.MedallionId;
                    var medallion = medallionService.Get(medallionId);
                    //get old Bill and old Transaction to compare
                    var oldBill = billService.Get(bill.Id);
                    var oldTransaction = transactionHistoryService.Get(transactionId);
                    if (oldBill != null && oldTransaction != null)
                    {
                        //get current loan 
                        var loan = loanService.GetLoanListByMemberAndMedallion(memberId, medallionId).FirstOrDefault();
                        var insuranceDeposit = insuranceDepositService.GetByMedallionAndMemberId(memberId, medallionId);
                        var ccsys = ccsystemAirtimeService.GetByMedallionAndMemberId(memberId, medallionId);
                        var accReceivable = accountReceivableService.GetByMedallionAndMemberId(memberId, medallionId);
                        var saveDeposit = savingDepositService.GetByMedallionAndMemberId(memberId, medallionId);
                        //decrease balance in medallionLoan 
                        if (loan != null && bill.Loan > 0)
                        {
                            loan.TotalPaid += bill.Loan - oldBill.Loan;
                            loan.CurrentBalance -= bill.Loan - oldBill.Loan;
                            CalculateEditPaidLoan(loan, bill, oldBill);
                            loanService.Update(loan);
                        }

                        // balance in insurance deposit 
                        if (insuranceDeposit != null && bill.InsuranceDepositAmount > 0)
                        {
                            insuranceDeposit.TotalPaid += bill.InsuranceDepositAmount - oldBill.InsuranceDepositAmount;
                            insuranceDeposit.CurrentBalance -= bill.InsuranceDepositAmount - oldBill.InsuranceDepositAmount;
                            insuranceDepositService.Update(insuranceDeposit);
                        }


                        //decrease balance in accReceivable 
                        if (accReceivable != null && bill.AccountReceivableAmount > 0)
                        {
                            accReceivable.TotalPaid += bill.AccountReceivableAmount - oldBill.AccountReceivableAmount;
                            accReceivable.CurrentBalance -= bill.AccountReceivableAmount - oldBill.AccountReceivableAmount;
                            accountReceivableService.Update(accReceivable);
                        }

                        if (!bill.IsZeroOut)
                        {
                            //decrease balance in saveDeposit 
                            if (saveDeposit != null && bill.SavingDepositAmount > 0)
                            {
                                saveDeposit.TotalPaid += bill.SavingDepositAmount - oldBill.SavingDepositAmount;
                                savingDepositService.Update(saveDeposit);
                            }
                        }
                        else
                        {
                            //decrease balance in saveDeposit 
                            if (saveDeposit != null && bill.SavingDepositAmount > 0)
                            {
                                saveDeposit.TotalPaid += -bill.SavingDepositAmount - oldBill.SavingDepositAmount;
                                savingDepositService.Update(saveDeposit);
                            }
                        }
                        oldBill = null;

                        //update bill  
                        var billUpdate = billService.Get(bill.Id);
                        billUpdate.AccountReceivableAmount = bill.AccountReceivableAmount;
                        billUpdate.AssociationDueAmount = bill.AssociationDueAmount;
                        billUpdate.Loan = bill.Loan;
                        billUpdate.Balance = bill.Balance;
                        billUpdate.Cash = bill.Cash;
                        billUpdate.CCSystemAirtimeAmount = bill.CCSystemAirtimeAmount;
                        billUpdate.Check = bill.Check;
                        billUpdate.CollsionInsuranceAmount = bill.CollsionInsuranceAmount;
                        billUpdate.Credit = billUpdate.Credit;
                        billUpdate.CreditCardAmount = bill.CreditCardAmount;
                        billUpdate.CreditCardFee = bill.CreditCardFee;
                        billUpdate.DateReceived = bill.DateReceived;
                        billUpdate.EndDate = billUpdate.EndDate;
                        billUpdate.InsuranceDepositAmount = bill.InsuranceDepositAmount;
                        billUpdate.InsuranceSticker = bill.InsuranceSticker;
                        billUpdate.InsuranceSurchargeAmount = bill.InsuranceSurchargeAmount;
                        billUpdate.Interval = bill.Interval;
                        billUpdate.IsAutoCashiering = bill.IsAutoCashiering;
                        billUpdate.IsZeroOut = bill.IsZeroOut;
                        billUpdate.MiscCharge = billUpdate.MiscCharge;
                        billUpdate.NextPayment = bill.NextPayment;
                        billUpdate.RecieptNumber = bill.RecieptNumber;
                        billUpdate.SavingDepositAmount = bill.SavingDepositAmount;
                        billUpdate.StartDate = bill.StartDate;
                        billUpdate.StatysPastDue = bill.StatysPastDue;
                        billUpdate.Subtotal = bill.Subtotal;
                        billUpdate.TotalDueAmount = bill.TotalDueAmount;
                        billUpdate.TotalPaid = bill.TotalPaid;
                        billUpdate.TotalPaidAmount = bill.TotalPaidAmount;
                        billUpdate.TransactionType = bill.TransactionType;
                        billUpdate.WerkReceivableAmount = bill.WerkReceivableAmount;
                        billUpdate.WorkerCompensationAmount = bill.WorkerCompensationAmount;

                        billService.Update(billUpdate);
                        //update transaction
                        oldTransaction.BillId = bill.Id;
                        oldTransaction.IsZeroOut = bill.IsZeroOut;
                        oldTransaction.DateReceived = bill.DateReceived;
                        oldTransaction.TransactionType = bill.TransactionType;
                        oldTransaction.AccountReceivableAmount = bill.AccountReceivableAmount;
                        oldTransaction.AssociationDueAmount = bill.AssociationDueAmount;
                        oldTransaction.Loan = bill.Loan;
                        oldTransaction.Balance = bill.Balance;
                        oldTransaction.Cash = bill.Cash;
                        oldTransaction.CCSystemAirtimeAmount = bill.CCSystemAirtimeAmount;
                        oldTransaction.Check = bill.Check;
                        oldTransaction.CollsionInsuranceAmount = bill.CollsionInsuranceAmount;
                        oldTransaction.Credit = bill.Credit;
                        oldTransaction.CreditCardAmount = bill.CreditCardAmount;
                        oldTransaction.CreditCardFee = bill.CreditCardFee;
                        oldTransaction.DueDate = bill.EndDate;

                        oldTransaction.InsuranceDepositAmount = bill.InsuranceDepositAmount;
                        oldTransaction.InsuranceSticker = bill.InsuranceSticker;
                        oldTransaction.InsuranceSurchargeAmount = bill.InsuranceSurchargeAmount;
                        oldTransaction.Interval = bill.Interval;
                        oldTransaction.IsAutoCashiering = bill.IsAutoCashiering;
                        oldTransaction.IsDeleted = false;
                        oldTransaction.LateFees = bill.LateFees;
                        oldTransaction.MedallionId = bill.MedallionId;

                        oldTransaction.MedallionNumber = medallion.MedallionNumber;
                        oldTransaction.MemberId = bill.MemberId;
                        oldTransaction.MiscCharge = bill.MiscCharge;
                        oldTransaction.NextPayment = bill.NextPayment;
                        oldTransaction.RecieptNumber = bill.RecieptNumber;
                        oldTransaction.SavingDepositAmount = bill.SavingDepositAmount;
                        oldTransaction.Subtotal = bill.Subtotal;
                        oldTransaction.TotalDueAmount = bill.TotalDueAmount;
                        oldTransaction.TotalPaidAmount = bill.TotalPaidAmount;

                        oldTransaction.UserName = bill.UserName;
                        oldTransaction.WerkReceivableAmount = bill.WerkReceivableAmount;
                        oldTransaction.WorkerCompensationAmount = bill.WorkerCompensationAmount;
                        oldTransaction.TransactionDate = bill.StartDate;
                        oldTransaction.TotalPaid = bill.TotalPaid;

                        //update transaction 
                        transactionHistoryService.Update(oldTransaction);

                        return Json(true);
                    }
                }
            }
            catch
            { return Json(false); }
            return Json(false);
        }

        /// <summary>
        /// get Bill to edit
        /// </summary>
        /// <param name="billId"></param>
        /// <returns></returns>
        public ActionResult GetEditBill(Guid billId)
        {
            //get bill by Id
            Bill bill = billService.Get(billId);

            if (bill == null) return Json(bill, JsonRequestBehavior.AllowGet);

            var memberId = bill.MemberId;
            var medallionId = bill.MedallionId;

            //get member and medallion
            var member = memberService.Get(memberId);
            var medallion = medallionService.Get(medallionId);

            BillMedallionViewModel medalBill = new BillMedallionViewModel();

            medalBill.Bill = bill;
            medalBill.Medallion = new Medallion();


            medalBill.Bill.TotalPaid = 0;
            medalBill.Bill.MedallionId = medallion.Id;
            medalBill.Member = new Member()
            {
                Id = member.Id,
                AccountNumber = member.AccountNumber,
                Name = member.Name
            };
            medalBill.Medallion = new Medallion()
            {
                Id = medallion.Id,
                MedallionNumber = medallion.MedallionNumber
            };

            return Json(medalBill, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Delete the last transaction
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        public ActionResult DeleteTransaction(Guid transactionId)
        {
            try
            {
                //get current transaction
                var transaction = transactionHistoryService.Get(transactionId);
                if (transaction != null)
                {
                    //get current bill
                    var bill = billService.Get(transaction.BillId);
                    if (bill != null)
                    {
                        var memberId = bill.MemberId;
                        var medallionId = bill.MedallionId;

                        //get current loan 
                        var loan = loanService.GetLoanListByMemberAndMedallion(memberId, medallionId).FirstOrDefault();
                        var insuranceDeposit = insuranceDepositService.GetByMedallionAndMemberId(memberId, medallionId);
                        var ccsys = ccsystemAirtimeService.GetByMedallionAndMemberId(memberId, medallionId);
                        var accReceivable = accountReceivableService.GetByMedallionAndMemberId(memberId, medallionId);
                        var saveDeposit = savingDepositService.GetByMedallionAndMemberId(memberId, medallionId);
                        //decrease balance in medallionLoan 
                        if (loan != null && bill.Loan > 0)
                        {
                            loan.TotalPaid += -bill.Loan;
                            loan.CurrentBalance -= -bill.Loan;
                            CalculatePaidRollBackLoan(loan, bill.Loan);
                            loanService.Update(loan);
                        }

                        // balance in insurance deposit 
                        if (insuranceDeposit != null && bill.InsuranceDepositAmount > 0)
                        {
                            insuranceDeposit.TotalPaid += -bill.InsuranceDepositAmount;
                            insuranceDeposit.CurrentBalance -= -bill.InsuranceDepositAmount;
                            insuranceDepositService.Update(insuranceDeposit);
                        }

                        //decrease balance in accReceivable 
                        if (accReceivable != null && bill.AccountReceivableAmount > 0)
                        {
                            accReceivable.TotalPaid += -bill.AccountReceivableAmount;
                            accReceivable.CurrentBalance -= -bill.AccountReceivableAmount;
                            accountReceivableService.Update(accReceivable);
                        }

                        //decrease balance in saveDeposit 
                        if (saveDeposit != null && bill.SavingDepositAmount > 0)
                        {
                            saveDeposit.TotalPaid += -bill.AccountReceivableAmount;
                            savingDepositService.Update(saveDeposit);
                        }
                        //delete bill
                        bill.IsDeleted = true;
                        //update bill  
                        billService.Update(bill);
                        //delete transaction 
                        transaction.IsDeleted = true;
                        transactionHistoryService.Update(transaction);
                        return Json(true);
                        //log 

                    }
                }
            }
            catch
            { return Json(false); }
            return Json(false);
        }

        [HttpPost]
        public ActionResult ReInitialize(int interval, string transactiontype, Guid memberId, Guid medallionId)
        {
            //get medallionLoan 
            if (interval > 0 && !String.IsNullOrEmpty(transactiontype))
            {
                ReInitInsuranceDeposit(interval, transactiontype, memberId, medallionId);
                ReInitLoan(interval, transactiontype, memberId, medallionId);
                ReInitAccountReceivable(interval, transactiontype, memberId, medallionId);
                return Json(true);
            }

            return Json(false);
        }

        [HttpPost]
        public ActionResult MakeCurrent(Bill bill)
        {
            if (bill != null)
            {
                DateTime nextToPay = bill.NextPayment;
                DateTime currentDate = DateTime.Now;
                int lateMonth = (int)Math.Round(currentDate.Subtract(nextToPay).TotalDays / 30);

                if (lateMonth > 0)
                {
                    var memberId = bill.MemberId;
                    var medallionId = bill.MedallionId;
                    var loan = loanService.GetLoanListByMemberAndMedallion(memberId, medallionId).FirstOrDefault();
                    var insuranceDeposit = insuranceDepositService.GetByMedallionAndMemberId(memberId, medallionId);
                    var ccsys = ccsystemAirtimeService.GetByMedallionAndMemberId(memberId, medallionId);
                    var accReceivable = accountReceivableService.GetByMedallionAndMemberId(memberId, medallionId);
                    var saveDeposit = savingDepositService.GetByMedallionAndMemberId(memberId, medallionId);
                    bill.StartDate = nextToPay;
                    bill.EndDate = nextToPay.AddMonths(lateMonth);
                    bill.NextPayment = bill.EndDate.AddDays(1);
                    bill.Loan = loan != null ? loan.CalculatedMonthlyPayment * lateMonth : 0;
                    bill.InsuranceDepositAmount = insuranceDeposit != null ? insuranceDeposit.WeeklyPayment * lateMonth : 0;
                    bill.CCSystemAirtimeAmount = ccsys != null ? ccsys.Airtime * lateMonth : 0;
                    bill.AccountReceivableAmount = accReceivable != null ? accReceivable.AccountReceivableAmount * lateMonth : 0;
                }
            }
            return Json(bill, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get Balance of Saving Deposit to use ZeroOut feature.
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ZeroOut(Bill bill)
        {
            if (bill != null)
            {
                var saveDeposit = savingDepositService.GetByMedallionAndMemberId(bill.MemberId, bill.MedallionId);
                if (saveDeposit != null)
                {
                    return Json(saveDeposit.TotalPaid, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="transactiontype"></param>
        /// <param name="memberId"></param>
        /// <param name="medallionId"></param>
        public void ReInitInsuranceDeposit(int interval, string transactiontype, Guid memberId, Guid medallionId)
        {
            var insuranceDeposit = insuranceDepositService.GetByMedallionAndMemberId(memberId, medallionId);
            var transactionList = transactionHistoryService.GetListTransactionByMedallionMemberId(memberId, medallionId);

            if (insuranceDeposit != null)
            {
                //get current balance 
                var currentBalance = insuranceDeposit.CurrentBalance;

                if (interval > 0 && !String.IsNullOrEmpty(transactiontype) && currentBalance > 0)
                {
                    if (transactiontype.Equals("M"))
                    {
                        var weeklypayment = currentBalance / (4 * interval);
                        insuranceDeposit.WeeklyPayment = weeklypayment;
                        insuranceDepositService.Update(insuranceDeposit);
                    }
                    else if (transactiontype.Equals("W"))
                    {
                        var weeklypayment = currentBalance / interval;
                        insuranceDeposit.WeeklyPayment = weeklypayment;
                        insuranceDepositService.Update(insuranceDeposit);
                    }
                }
            }

        }

        public void ReInitLoan(int interval, string transactiontype, Guid memberId, Guid medallionId)
        {
            var loan = loanService.GetLoanListByMemberAndMedallion(memberId, medallionId).FirstOrDefault();
            var transactionList = transactionHistoryService.GetListTransactionByMedallionMemberId(memberId, medallionId);
            if (loan != null)
            {
                var currentBalance = loan.CurrentBalance;
                var interestRate = loan.InterestRate;
                if (transactionList != null && transactionList.Count > 0)
                {
                    //find total month paid of member 
                    int totalMonth = 0;
                    foreach (TransactionHistory tran in transactionList)
                    {
                        var startDate = tran.TransactionDate;
                        var endDate = tran.DueDate;
                        int countmonth = (int)Math.Round(endDate.Value.Subtract(startDate.Value).TotalDays / 30);
                        totalMonth += countmonth;
                    }
                    if (totalMonth > 0)
                    {
                        int originalloanTerm = ((int)loan.LoanTerm);
                        int monthRest = originalloanTerm - totalMonth;

                        //calculate to principal balance
                        var interestPaid = loan.TotalInterestPaid;
                        var amountInterestPaid = monthRest * interestPaid;
                        var originalAmount = currentBalance - amountInterestPaid;
                        currentBalance = originalAmount;
                    }
                }
                if (interval > 0 && !String.IsNullOrEmpty(transactiontype) && currentBalance > 0)
                {
                    var loanterm = 0;
                    if (transactiontype.Equals("M"))
                    {
                        loanterm = interval;
                    }
                    else if (transactiontype.Equals("W"))
                    {
                        loanterm = interval / 4;
                    }
                    if (loanterm > 0)
                    {
                        var totalAmount = loanterm * (interestRate / 12 / 100) * currentBalance + currentBalance;
                        var monthlyPaid = totalAmount / loanterm;
                        var totalprincipalPay = currentBalance / loanterm;
                        var totalInterestPay = ((interestRate / 12 / 100) * currentBalance);


                        loan.LoanTerm = loanterm;
                        loan.CurrentBalance = totalAmount;

                        loanService.Update(loan);
                    }
                }
            }
        }

        public void ReInitAccountReceivable(int interval, string transactiontype, Guid memberId, Guid medallionId)
        {
            var accReceivable = accountReceivableService.GetByMedallionAndMemberId(memberId, medallionId);
            if (accReceivable != null)
            {
                var currentBalance = accReceivable.CurrentBalance;

                if (interval > 0 && !String.IsNullOrEmpty(transactiontype) && currentBalance > 0)
                {
                    var loanterm = 0;
                    if (transactiontype.Equals("M"))
                    {
                        loanterm = interval;
                    }
                    else if (transactiontype.Equals("W"))
                    {
                        loanterm = interval / 4;
                    }
                    if (loanterm > 0)
                    {
                        if (loanterm < 1) { loanterm = 1; }
                        var totalAmount = currentBalance;
                        var monthlyPaid = totalAmount / loanterm;
                        accReceivable.MonthlyPayment = monthlyPaid;
                        accReceivable.PaymentTermMonth = loanterm;

                        accountReceivableService.Update(accReceivable);
                    }
                }
            }
        }

        public void CalculatePaidLoan(Loan loan, decimal loanpay)
        {
            decimal loanamount = loan.LoanAmount;
            decimal totalamount = loanamount + (loan.InterestRate * loan.LoanTerm);
            decimal amountInterest = loanamount * (loan.InterestRate / 12 / 100);
            decimal amountmonthlypay = totalamount / loan.LoanTerm;
            if (loanpay > amountInterest)
            {
                loan.TotalPrincipalPaid += Math.Round(loanpay - amountInterest, 2);
                loan.TotalInterestPaid += Math.Round(amountInterest, 2);
            }

        }

        public void CalculateEditPaidLoan(Loan loan, Bill newBill, Bill oldBill)
        {
            decimal loanamount = loan.LoanAmount;
            decimal totalamount = loanamount + (loan.InterestRate * loan.LoanTerm);
            decimal amountInterest = loanamount * (loan.InterestRate / 12 / 100);
            decimal amountmonthlypay = totalamount / loan.LoanTerm;

            loan.TotalPrincipalPaid += Math.Round((newBill.Loan - amountInterest) - (oldBill.Loan - amountInterest), 2);
        }

        public void CalculatePaidRollBackLoan(Loan loan, decimal loanpay)
        {
            decimal loanamount = loan.LoanAmount;
            decimal totalamount = loanamount + (loan.InterestRate * loan.LoanTerm);
            decimal amountInterest = loanamount * (loan.InterestRate / 12 / 100);
            decimal amountmonthlypay = totalamount / loan.LoanTerm;
            if (loanpay > amountInterest)
            {
                loan.TotalPrincipalPaid -= Math.Round(loanpay - amountInterest, 2);
                loan.TotalInterestPaid -= Math.Round(amountInterest, 2);
            }

        }
        #endregion

        #region Loans
        public ActionResult LoanList()
        {
            ViewBag.Loans = "active";
            return PartialView();
        }
        public ActionResult EditLoan() { ViewBag.Loans = "active"; return PartialView(); }
        public ActionResult AddNewLoan() { ViewBag.Loans = "active"; return PartialView(); }

        [HttpPost]
        public ActionResult GetLoanListByMemberId(Guid id)
        {
            var list = loanService.GetListLoanByMemberId(id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetLoanById(Guid id)
        {
            var loan = loanService.Get(id);
            return Json(loan, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddLoan(Loan loan)
        {
            if (loan != null)
            {
                loan.Id = Guid.NewGuid();
                loanService.Add(loan);
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        public ActionResult EditLoan(Loan loan)
        {
            if (loan != null)
            {
                loanService.Update(loan);
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        public ActionResult DeleteLoan(Loan loan)
        {
            if (loan != null)
            {
                var newLoan = loanService.Get(loan.Id);
                if (newLoan != null)
                {
                    newLoan.IsDeleted = true;
                    loanService.Update(newLoan);
                    return Json(true);
                }
            }
            return Json(false);
        }

        [HttpPost]
        public ActionResult CheckDuplicateLoan(string loanname)
        {
            if (!String.IsNullOrEmpty(loanname))
            {
                bool isDup = loanService.Get().Where(c => c.LoanName.Trim().ToUpper().Equals(loanname.Trim().ToUpper()) && c.IsDeleted == false).FirstOrDefault() != null;
                return Json(isDup);
            }
            return Json(false);
        }

        [HttpPost]
        public ActionResult CheckDuplicateEditLoan(string loanname, Guid loanId)
        {
            if (!String.IsNullOrEmpty(loanname))
            {
                bool isDup = loanService.Get().Where(c => c.LoanName.Trim().ToUpper().Equals(loanname.Trim().ToUpper()) && c.IsDeleted == false && c.Id != loanId).FirstOrDefault() != null;
                return Json(isDup);
            }
            return Json(false);
        }
        #endregion
    }
}