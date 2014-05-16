using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;

namespace TDevs.Services
{
    public interface IInsuranceDepositService
    {
        IList<InsuranceDeposit> Get();

        InsuranceDeposit Get(Guid Id);

        InsuranceDeposit Add(InsuranceDeposit insuranceDeposit);

        InsuranceDeposit Update(InsuranceDeposit insuranceDeposit);

        bool Remove(Guid Id);

        InsuranceDeposit GetByMedallionAndMemberId(Guid memberId, Guid medallionId);
        
    }
}
