using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;
using TDevs.Domain.ViewModel;

namespace TDevs.Services
{
    public interface IAgentService
    {
        IList<Agent> Get();

        Agent Get(Guid Id);

        Agent Add(Agent agent);

        Agent Update(Agent agent);

        bool Remove(Guid Id);

        IList<SearchAgentResult> SearchAgentByCriterial(AgentCriterial criterial);
    }
}
