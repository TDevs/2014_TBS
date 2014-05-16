using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Core.Infrastructure;
using TDevs.Domain;

namespace TDevs.Services
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly IRepository<VehicleMake> _vehicleMakeRepository;
        public VehicleMakeService(IRepository<VehicleMake> vehicleMakeRepository)
        {
            this._vehicleMakeRepository = vehicleMakeRepository;
        }

        public IList<VehicleMake> Get()
        {
            return _vehicleMakeRepository.Get.ToList();
        }

        public VehicleMake Get(Guid Id)
        {
            return _vehicleMakeRepository.Get.FirstOrDefault(it => it.Id == Id);
        }

        public VehicleMake Add(VehicleMake vehicleMake)
        {
            _vehicleMakeRepository.Add(vehicleMake);
            return vehicleMake;
        }

        public VehicleMake Update(VehicleMake vehicleMake)
        {
            _vehicleMakeRepository.Update(vehicleMake);
            return vehicleMake;
        }

        public bool Remove(Guid Id)
        {
            var vehicleMake = Get(Id);
            if (vehicleMake == null) return false;
            _vehicleMakeRepository.Remove(vehicleMake);
            return true;
        }
    }
}
