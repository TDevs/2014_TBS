using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Core.Infrastructure;
using TDevs.Domain;

namespace TDevs.Services
{
    public class VehicleFeatureService : IVehicleFeatureService
    {
        private readonly IRepository<VehicleFeature> _vehicleFeatureRepository;
        public VehicleFeatureService(IRepository<VehicleFeature> vehicleFeatureRepository)
        {
            this._vehicleFeatureRepository = vehicleFeatureRepository;
        }

        public IList<VehicleFeature> Get()
        {
            return _vehicleFeatureRepository.Get.ToList();
        }

        public VehicleFeature Get(Guid Id)
        {
            return _vehicleFeatureRepository.Get.FirstOrDefault(it => it.Id == Id);
        }

        public VehicleFeature Add(VehicleFeature vehicleFeature)
        {
            _vehicleFeatureRepository.Add(vehicleFeature);
            return vehicleFeature;
        }

        public VehicleFeature Update(VehicleFeature vehicleFeature)
        {
            _vehicleFeatureRepository.Update(vehicleFeature);
            return vehicleFeature;
        }

        public bool Remove(Guid Id)
        {
            var vehicleFeature = Get(Id);
            if (vehicleFeature == null) return false;
            _vehicleFeatureRepository.Remove(vehicleFeature);
            return true;
        }
    }
}
