using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Core.Infrastructure;
using TDevs.Domain;

namespace TDevs.Services
{
    public class SavingDepositService : ISavingDepositService
    {
        private readonly IRepository<SavingDeposit> savingDepositRepository;
        public SavingDepositService(IRepository<SavingDeposit> savingDepositRepository)
        {
            this.savingDepositRepository = savingDepositRepository;
        }

        public IList<SavingDeposit> Get()
        {
            return savingDepositRepository.Get.ToList();
        }

        public SavingDeposit Get(Guid Id)
        {
            return savingDepositRepository.Get.FirstOrDefault(c => c.Id == Id);
        }

        public SavingDeposit Add(SavingDeposit savingDeposit)
        {
            savingDepositRepository.Add(savingDeposit);
            return savingDeposit;
        }

        public SavingDeposit Update(SavingDeposit savingDeposit)
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


        public SavingDeposit GetByMedallionAndMemberId(Guid memberId, Guid medallionId)
        {
            var obj = savingDepositRepository.Get.Where(c => c.MemberId == memberId && c.MedallionId == medallionId).FirstOrDefault();
            return obj ?? new SavingDeposit();
        }
    }
}
