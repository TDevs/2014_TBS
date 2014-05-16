using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Core.Infrastructure;
using TDevs.Domain;

namespace TDevs.Services
{
    public class InsuranceDepositAgentService : IInsuranceDepositAgentService
    {
        private readonly IRepository<InsuranceDepositAgent> insuranceDepositAgentRepository;
        public InsuranceDepositAgentService(IRepository<InsuranceDepositAgent> insuranceDepositAgentRepository)
        {
            this.insuranceDepositAgentRepository = insuranceDepositAgentRepository;
        }

        public IList<InsuranceDepositAgent> Get()
        {
            return insuranceDepositAgentRepository.Get.ToList();
        }

        public InsuranceDepositAgent Get(Guid Id)
        {
            return insuranceDepositAgentRepository.Get.FirstOrDefault(c => c.Id == Id);
        }

        public InsuranceDepositAgent Add(InsuranceDepositAgent insuranceDeposit)
        {
            insuranceDepositAgentRepository.Add(insuranceDeposit);
            return insuranceDeposit;
        }

        public InsuranceDepositAgent Update(InsuranceDepositAgent insuranceDeposit)
        {
            insuranceDepositAgentRepository.Update(insuranceDeposit);
            return insuranceDeposit;
        }

        public bool Remove(Guid Id)
        {
            var insuranceDeposit = Get(Id);
            if (insuranceDeposit == null) return false;
            insuranceDepositAgentRepository.Remove(insuranceDeposit);
            return true;
        }


         
    }
}
