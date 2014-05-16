using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;

namespace TDevs.Services
{
    public interface ISavingDepositAgentService
    {
        IList<SavingDepositAgent> Get();

        SavingDepositAgent Get(Guid Id);

        SavingDepositAgent Add(SavingDepositAgent savingDeposit);

        SavingDepositAgent Update(SavingDepositAgent savingDeposit);

        bool Remove(Guid Id);

 
    }
}
