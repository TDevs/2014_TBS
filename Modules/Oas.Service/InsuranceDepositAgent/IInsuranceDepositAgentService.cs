using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;

namespace TDevs.Services
{
    public interface IInsuranceDepositAgentService
    {
        IList<InsuranceDepositAgent> Get();

        InsuranceDepositAgent Get(Guid Id);

        InsuranceDepositAgent Add(InsuranceDepositAgent insuranceDeposit);

        InsuranceDepositAgent Update(InsuranceDepositAgent insuranceDeposit);

        bool Remove(Guid Id);
    }
}
