using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;

namespace TDevs.Services
{
    public interface IAccountReceivableService
    {
        IList<AccountReceivable> Get();

        AccountReceivable Get(Guid Id);

        AccountReceivable Add(AccountReceivable accountReceivable);

        AccountReceivable Update(AccountReceivable accountReceivable);

        bool Remove(Guid Id);

        AccountReceivable GetByMedallionAndMemberId(Guid memberId, Guid medallionId);
    }
}
