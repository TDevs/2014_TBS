using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Core.Infrastructure;
using TDevs.Domain;

namespace TDevs.Services
{
    public class AccountReceivableService : IAccountReceivableService
    {
        private readonly IRepository<AccountReceivable> accountReceivableRepository;
        public AccountReceivableService(IRepository<AccountReceivable> accountReceivableRepository)
        {
            this.accountReceivableRepository = accountReceivableRepository;
        }

        public IList<AccountReceivable> Get()
        {
            return accountReceivableRepository.Get.ToList();
        }

        public AccountReceivable Get(Guid Id)
        {
            return accountReceivableRepository.Get.FirstOrDefault(c => c.Id == Id);
        }

        public AccountReceivable Add(AccountReceivable accountReceivable)
        {
            accountReceivableRepository.Add(accountReceivable);
            return accountReceivable;
        }

        public AccountReceivable Update(AccountReceivable accountReceivable)
        {
            accountReceivableRepository.Update(accountReceivable);
            return accountReceivable;
        }

        public bool Remove(Guid Id)
        {
            var accountReceivable = Get(Id);
            if (accountReceivable == null) return false;
            accountReceivableRepository.Remove(accountReceivable);
            return true;
        }

        public AccountReceivable GetByMedallionAndMemberId(Guid memberId, Guid medallionId)
        {
            var accountReceivable = accountReceivableRepository.Get.Where(c => c.MemberId == memberId && c.MedallionId == medallionId).FirstOrDefault();
            return accountReceivable ?? new AccountReceivable();
        }
    }
}
