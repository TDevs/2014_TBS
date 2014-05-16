using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Core.Infrastructure;
using TDevs.Domain;

namespace TDevs.Services
{
    public class InsuranceDepositService : IInsuranceDepositService
    {
        private readonly IRepository<InsuranceDeposit> insuranceDepositRepository;
        public InsuranceDepositService(IRepository<InsuranceDeposit> insuranceDepositRepository)
        {
            this.insuranceDepositRepository = insuranceDepositRepository;
        }

        public IList<InsuranceDeposit> Get()
        {
            return insuranceDepositRepository.Get.ToList();
        }

        public InsuranceDeposit Get(Guid Id)
        {
            return insuranceDepositRepository.Get.FirstOrDefault(c => c.Id == Id);
        }

        public InsuranceDeposit Add(InsuranceDeposit insuranceDeposit)
        {
            insuranceDepositRepository.Add(insuranceDeposit);
            return insuranceDeposit;
        }

        public InsuranceDeposit Update(InsuranceDeposit insuranceDeposit)
        {
            insuranceDepositRepository.Update(insuranceDeposit);
            return insuranceDeposit;
        }

        public bool Remove(Guid Id)
        {
            var insuranceDeposit = Get(Id);
            if (insuranceDeposit == null) return false;
            insuranceDepositRepository.Remove(insuranceDeposit);
            return true;
        }


        public InsuranceDeposit GetByMedallionAndMemberId(Guid memberId, Guid medallionId)
        {
            var insurance = insuranceDepositRepository.Get.Where(c => c.MemberId == memberId && c.MedallionId == medallionId).FirstOrDefault();
            return insurance;
        }
    }
}
