using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;

namespace TDevs.Services
{
    public interface IAutoLoanSetupAgentService
    {
        IList<AutoLoanSetupAgent> Get();

        AutoLoanSetupAgent Get(Guid Id);

        AutoLoanSetupAgent Add(AutoLoanSetupAgent autoLoanSetupAgent);

        AutoLoanSetupAgent Update(AutoLoanSetupAgent autoLoanSetupAgent);

        bool Remove(Guid Id);
      
    }
}
