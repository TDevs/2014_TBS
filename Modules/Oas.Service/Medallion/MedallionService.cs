using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Core.Infrastructure;
using TDevs.Domain;
using TDevs.Domain.ViewModel;

namespace TDevs.Services
{
    public class MedallionService : IMedallionService
    {
        private readonly IRepository<Medallion> medallionRepository;
        
        private readonly IRepository<InsuranceDeposit> insuranceDepositRepository;
         
        private readonly IRepository<AccountReceivable> accountReceivableRepository;
        private readonly IRepository<Loan> loanRepository;
        private readonly IRepository<SavingDeposit> savingDepositRepository;
        private readonly IRepository<CCSystemAirtime> ccsystemAirtimeRepository;
        private readonly IRepository<TransactionHistory> transactionHistoryRepository;
        private readonly IRepository<Bill> billRepository;
        private readonly IRepository<Company> companyRepository;
       

        public MedallionService(IRepository<Medallion> medallionRepository, 
            IRepository<InsuranceDeposit> insuranceDepositRepository, 
            IRepository<AccountReceivable> accountReceivableRepository, IRepository<Loan> loanRepository, IRepository<SavingDeposit> savingDepositRepository,
            IRepository<CCSystemAirtime> ccsystemAirtimeRepository, IRepository<TransactionHistory> transactionHistoryRepository,
            IRepository<Bill> billRepository, IRepository<Company> companyRepository)
        {
            this.medallionRepository = medallionRepository;          
            this.insuranceDepositRepository = insuranceDepositRepository;          
            this.accountReceivableRepository = accountReceivableRepository;
            this.loanRepository = loanRepository;
            this.savingDepositRepository = savingDepositRepository;
            this.ccsystemAirtimeRepository = ccsystemAirtimeRepository;
            this.transactionHistoryRepository = transactionHistoryRepository;
            this.billRepository = billRepository;
            this.companyRepository = companyRepository;
        }

        public IList<Medallion> Get()
        {
            return medallionRepository.Get.ToList();
        }

        public Medallion Get(Guid Id)
        {
            return medallionRepository.Get.FirstOrDefault(c => c.Id == Id);
        }

        public Medallion Add(Medallion medallion)
        {
            medallionRepository.Add(medallion);
            return medallion;
        }

        public Medallion Update(Medallion medallion)
        {
            medallionRepository.Update(medallion);
            return medallion;
        }

        public bool Remove(Guid Id)
        {
            var medallion = Get(Id);
            if (medallion == null) return false;
            medallionRepository.Remove(medallion);
            return true;
        }

        public IList<Medallion> GetMemberMedallions(Guid memberId)
        {
            var list = medallionRepository.Get.Where(t => t.MemberId.Equals(memberId)).ToList();
            return list;
        }

        #region MedallionLoan
        public List<MedallionLoan> GetMedallionLoanByMemberId(Guid memberId)
        {
            DateTime today = DateTime.Now;

            var list = new List<MedallionLoan>();
            //get medallion 
            var medallionlist = GetMemberMedallions(memberId);
            //get transaction 
            var transactionhistory = transactionHistoryRepository.Get.Where(c => c.MemberId == memberId).ToList();
            //get list bill
            var listbill = billRepository.Get.Where(c => c.MemberId == memberId).OrderBy(c => c.NextPayment).ToList();
            //get company setting 
            var companySetting = companyRepository.Get.FirstOrDefault();

            Bill bill = null;
            //initial bill with last bill
            if (listbill != null && listbill.Count > 0)
            {
                bill = listbill.First();
            }

            foreach (Medallion med in medallionlist)
            {
                DateTime billingStartDate = med.BillingStartDate;
                DateTime billingEndDate = med.BillingEndDate;

                if (bill == null)
                {
                    bill = new Bill();
                    bill.StartDate = billingStartDate;
                    bill.EndDate = billingStartDate.AddMonths(1);
                    bill.NextPayment = bill.EndDate.AddDays(1);
                }
                else
                {
                    bill.StartDate = bill.NextPayment;
                    bill.EndDate = bill.EndDate.AddMonths(1);
                    bill.NextPayment = bill.EndDate.AddDays(1);
                }
                MedallionLoan medloan = new MedallionLoan();
                medloan.MedallionId = med.Id;
                medloan.MedallionNumber = med.MedallionNumber;

                //get insurance deposit  
                var insruance = insuranceDepositRepository.Get.Where(c => c.MedallionId == med.Id && c.MemberId == c.MemberId).FirstOrDefault();
                var ccsystem = ccsystemAirtimeRepository.Get.Where(c => c.MedallionId == med.Id && c.MemberId == c.MemberId).FirstOrDefault();
                //get loans by medallionId
                var loanlist = loanRepository.Get.Where(c => c.MemberId == memberId && c.MedallionId == med.Id && c.IsDeleted == false);
               
                var accountreceivabl = accountReceivableRepository.Get.Where(c => c.MedallionId == med.Id && c.MemberId == c.MemberId).FirstOrDefault();
                var savingdeposit = savingDepositRepository.Get.Where(c => c.MedallionId == med.Id && c.MemberId == c.MemberId).FirstOrDefault();

                //current balance             
                medloan.CurrentBalance += insruance != null ? insruance.CurrentBalance : 0;
                medloan.CurrentBalance += ccsystem != null ? ccsystem.Airtime : 0;              
                medloan.CurrentBalance += accountreceivabl != null ? accountreceivabl.CurrentBalance : 0;
                if (loanlist != null && loanlist.Count() > 0)
                {
                    foreach (Loan l in loanlist)
                    {
                        medloan.CurrentBalance += l.CurrentBalance;
                    }
                }
                //calculate late fee 
                if (companySetting != null)
                {
                    int allowLateDate = companySetting.DefaultLateBreak > 0 ? (int)companySetting.DefaultLateBreak : 0;

                    var totalDateLate = today.Subtract(bill.EndDate).Days;
                    if (totalDateLate > allowLateDate)
                    {                        
                        //calculate late fee
                        decimal feeLateOneDay = (companySetting.DefaultLateCharge / 7);
                        decimal totalFeeLate = Math.Round(feeLateOneDay * totalDateLate, 2, MidpointRounding.AwayFromZero);
                        bill.LateFees = totalFeeLate;

                        medloan.DateLates = totalDateLate;
                        medloan.Status = "Past Due";
                    }
                    else if (totalDateLate < allowLateDate && totalDateLate > 1)
                    {                       
                        medloan.Status = "Due";
                    }
                    else
                    {                        
                        medloan.Status = "Current";
                    }
                }

                medloan.PayTillDate = bill.EndDate;
                medloan.PaymentDueDate = bill.StartDate;
                
                list.Add(medloan);
            }

            return list;
        }
        #endregion

    }
}
