using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;
 

namespace TDevs.Services
{
    public interface ILoanService
    {
        IList<Loan> Get();

        Loan Get(Guid Id);

        Loan Add(Loan loan);

        Loan Update(Loan loan);

        bool Remove(Guid Id);

        IList<Loan> GetListLoanByMemberId(Guid id);
        IList<Loan> GetLoanListByMemberAndMedallion(Guid memberId, Guid medallionId);
    }
}
