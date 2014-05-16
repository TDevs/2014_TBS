using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;


namespace TDevs.Services
{
    public interface IAgentVehicleService
    {
        IList<AgentVehicle> Get();

        AgentVehicle Get(Guid Id);

        AgentVehicle Add(AgentVehicle agentvehicle);

        AgentVehicle Update(AgentVehicle agentvehicle);

        bool Remove(Guid Id);

        IList<AgentVehicle> GetListAgentVehiclesByAgentId(Guid agentId);
    }
}
