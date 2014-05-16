using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Core.Infrastructure;
using TDevs.Domain;

namespace TDevs.Services
{
    public class AutoLoanSetupAgentService : IAutoLoanSetupAgentService
    {
        private readonly IRepository<AutoLoanSetupAgent> autoLoanSetupAgentRepository;
        public AutoLoanSetupAgentService(IRepository<AutoLoanSetupAgent> autoLoanSetupAgentRepository)
        {
            this.autoLoanSetupAgentRepository = autoLoanSetupAgentRepository;
        }

        public IList<AutoLoanSetupAgent> Get()
        {
            return autoLoanSetupAgentRepository.Get.ToList();
        }

        public AutoLoanSetupAgent Get(Guid Id)
        {
            return autoLoanSetupAgentRepository.Get.FirstOrDefault(c => c.Id == Id);
        }

        public AutoLoanSetupAgent Add(AutoLoanSetupAgent autoLoanSetupAgent)
        {
            autoLoanSetupAgentRepository.Add(autoLoanSetupAgent);
            return autoLoanSetupAgent;
        }

        public AutoLoanSetupAgent Update(AutoLoanSetupAgent autoLoanSetupAgent)
        {
            autoLoanSetupAgentRepository.Update(autoLoanSetupAgent);
            return autoLoanSetupAgent;
        }

        public bool Remove(Guid Id)
        {
            var autoLoanSetupAgent = Get(Id);
            if (autoLoanSetupAgent == null) return false;
            autoLoanSetupAgentRepository.Remove(autoLoanSetupAgent);
            return true;
        }


       
    }
}
