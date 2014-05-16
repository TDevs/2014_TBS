using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Core.Infrastructure;
using TDevs.Domain;

namespace TDevs.Services
{
    public class AccountReceivableAgentService : IAccountReceivableAgentService
    {
        private readonly IRepository<AccountReceivableAgent> accountReceivableAgentRepository;
        public AccountReceivableAgentService(IRepository<AccountReceivableAgent> accountReceivableAgentRepository)
        {
            this.accountReceivableAgentRepository = accountReceivableAgentRepository;
        }

        public IList<AccountReceivableAgent> Get()
        {
            return accountReceivableAgentRepository.Get.ToList();
        }

        public AccountReceivableAgent Get(Guid Id)
        {
            return accountReceivableAgentRepository.Get.FirstOrDefault(c => c.Id == Id);
        }

        public AccountReceivableAgent Add(AccountReceivableAgent accountReceivableAgent)
        {
            accountReceivableAgentRepository.Add(accountReceivableAgent);
            return accountReceivableAgent;
        }

        public AccountReceivableAgent Update(AccountReceivableAgent accountReceivableAgent)
        {
            accountReceivableAgentRepository.Update(accountReceivableAgent);
            return accountReceivableAgent;
        }

        public bool Remove(Guid Id)
        {
            var accountReceivableAgent = Get(Id);
            if (accountReceivableAgent == null) return false;
            accountReceivableAgentRepository.Remove(accountReceivableAgent);
            return true;
        }

        
    }
}
