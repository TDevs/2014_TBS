using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;

namespace TDevs.Services
{
    public interface ISavingDepositService
    {
        IList<SavingDeposit> Get();

        SavingDeposit Get(Guid Id);

        SavingDeposit Add(SavingDeposit savingDeposit);

        SavingDeposit Update(SavingDeposit savingDeposit);

        bool Remove(Guid Id);

        SavingDeposit GetByMedallionAndMemberId(Guid memberId, Guid medallionId);
    }
}
