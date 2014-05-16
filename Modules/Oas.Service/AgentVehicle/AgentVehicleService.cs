using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using TDevs.Core.Infrastructure;
using TDevs.Domain;

namespace TDevs.Services
{
    public class AgentVehicleService : IAgentVehicleService
    {
        private readonly IRepository<AgentVehicle> agentvehicleRepository;
        public AgentVehicleService(IRepository<AgentVehicle> agentvehicleRepository)
        {
            this.agentvehicleRepository = agentvehicleRepository;
        }

        public IList<AgentVehicle> Get()
        {
            return agentvehicleRepository.Get.ToList();
        }

        public AgentVehicle Get(Guid Id)
        {
            return agentvehicleRepository.Get.FirstOrDefault(c => c.Id == Id);
        }

        public AgentVehicle Add(AgentVehicle agentvehicle)
        {
            agentvehicleRepository.Add(agentvehicle);
            return agentvehicle;
        }

        public AgentVehicle Update(AgentVehicle agentvehicle)
        {
            agentvehicleRepository.Update(agentvehicle);
            return agentvehicle;
        }

        public bool Remove(Guid Id)
        {
            var agentvehicle = Get(Id);
            if (agentvehicle == null) return false;
            agentvehicleRepository.Remove(agentvehicle);
            return true;
        }


        public IList<AgentVehicle> GetListAgentVehiclesByAgentId(Guid agentId)
        {
            var list = agentvehicleRepository.Get.Include(c => c.Vehicle).Where(c => c.AgentId == agentId).ToList();
            var result = (from a in list
                         select new AgentVehicle()
                         {
                             Id = a.Id,
                             AgentId = a.AgentId,
                             AgentFee = a.AgentFee,
                             VehicleId = a.VehicleId,
                             Vehicle = new Vehicle()
                             {
                                 LicenseNumber = a.Vehicle.LicenseNumber
                             }
                         }).ToList();
            return result;
        }
    }
}
