using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Core.Infrastructure;
using TDevs.Domain;

namespace TDevs.Services
{
    public class SavingDepositAgentService : ISavingDepositAgentService
    {
        private readonly IRepository<SavingDepositAgent> savingDepositRepository;
        public SavingDepositAgentService(IRepository<SavingDepositAgent> savingDepositRepository)
        {
            this.savingDepositRepository = savingDepositRepository;
        }

        public IList<SavingDepositAgent> Get()
        {
            return savingDepositRepository.Get.ToList();
        }

        public SavingDepositAgent Get(Guid Id)
        {
            return savingDepositRepository.Get.FirstOrDefault(c => c.Id == Id);
        }

        public SavingDepositAgent Add(SavingDepositAgent savingDeposit)
        {
            savingDepositRepository.Add(savingDeposit);
            return savingDeposit;
        }

        public SavingDepositAgent Update(SavingDepositAgent savingDeposit)
        {
            savingDepositRepository.Update(savingDeposit);
            return savingDeposit;
        }

        public bool Remove(Guid Id)
        {
            var savingDeposit = Get(Id);
            if (savingDeposit == null) return false;
            savingDepositRepository.Remove(savingDeposit);
            return true;
        }
    }
}
