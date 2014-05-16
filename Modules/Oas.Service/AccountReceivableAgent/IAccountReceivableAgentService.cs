using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;

namespace TDevs.Services
{
    public interface IAccountReceivableAgentService
    {
        IList<AccountReceivableAgent> Get();

        AccountReceivableAgent Get(Guid Id);

        AccountReceivableAgent Add(AccountReceivableAgent accountReceivableAgent);

        AccountReceivableAgent Update(AccountReceivableAgent accountReceivableAgent);

        bool Remove(Guid Id);

         
    }
}
