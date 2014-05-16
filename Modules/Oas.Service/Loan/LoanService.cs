using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Core.Infrastructure;
using System.Data.Entity;
using TDevs.Domain;

namespace TDevs.Services
{
    public class LoanService : ILoanService
    {
        private readonly IRepository<Loan> loanRepository;
        public LoanService(IRepository<Loan> loanRepository)
        {
            this.loanRepository = loanRepository;
        }

        public IList<Loan> Get()
        {
            return loanRepository.Get.ToList();
        }

        public Loan Get(Guid Id)
        {
            return loanRepository.Get.FirstOrDefault(c => c.Id == Id);
        }

        public Loan Add(Loan loan)
        {
            loanRepository.Add(loan);
            return loan;
        }

        public Loan Update(Loan loan)
        {
            loanRepository.Update(loan);
            return loan;
        }

        public bool Remove(Guid Id)
        {
            var loan = Get(Id);
            if (loan == null) return false;
            loanRepository.Remove(loan);
            return true;
        }


        public IList<Loan> GetListLoanByMemberId(Guid id)
        {
            var list = (loanRepository.Get
                .Include(c => c.Medallion)
                .Where(c => c.MemberId == id && c.IsDeleted == false)).ToList();
            var result = (from a in list
                         select new Loan()
                         {
                             Id = a.Id,
                             IsDeleted = a.IsDeleted,
                             MemberId = a.MemberId,
                             LoanAmount = a.LoanAmount,
                             Medallion = a.Medallion!=null ? new Medallion() { 
                             Id = a.Medallion.Id ,MedallionNumber = a.Medallion.MedallionNumber
                             }:new Medallion(),
                             StartDate = a.StartDate , 
                             EndDate = a.EndDate , 
                             TotalInterestPaid = a.TotalInterestPaid , 
                             TotalPaid = a.TotalPaid,
                             TotalPrincipalPaid=a.TotalPrincipalPaid,
                             Status = a.Status,
                             LoanName = a.LoanName
                         }).ToList();

            return result;
        }

        public IList<Loan> GetLoanListByMemberAndMedallion(Guid memberId, Guid medallionId)
        {
            var list = loanRepository.Get.Where(c => c.MemberId == memberId && medallionId == c.MedallionId && c.IsDeleted == false).ToList();
            return list;
        }
    }
}
