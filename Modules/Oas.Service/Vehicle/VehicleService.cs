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
    public class VehicleService : IVehicleService
    {
        private readonly IRepository<Vehicle> vehicleRepository;
        public VehicleService(IRepository<Vehicle> vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;
        }

        public IList<Vehicle> Get()
        {
            return vehicleRepository.Get.ToList();
        }

        public Vehicle Get(Guid Id)
        {
            return vehicleRepository.Get.FirstOrDefault(c => c.Id == Id);
        }

        public Vehicle Add(Vehicle vehicle)
        {
            vehicle.Id = Guid.NewGuid();
            vehicleRepository.Add(vehicle);
            return vehicle;
        }

        public Vehicle Update(Vehicle vehicle)
        {
            vehicleRepository.Update(vehicle);
            return vehicle;
        }

        public bool Remove(Guid Id)
        {
            var vehicle = Get(Id);
            if (vehicle == null) return false;
            vehicleRepository.Remove(vehicle);
            return true;
        }


        public IQueryable<Vehicle> GetByMember(Guid Id)
        {
            var lst = vehicleRepository.Get.Where(t => t.MemberId.Equals(Id)).Include(t => t.VehicleFeature)
                .Include(t => t.VehicleMake)
                .Include(t => t.MeterManufacturer)
                .Include(t => t.VehicleFeature).OrderBy(t=>t.EINNumber);
            return lst;
        }
    }
}
