using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Core.Infrastructure;
using TDevs.Domain;

namespace TDevs.Services
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly IRepository<VehicleModel> _vehicleModelRepository;
        public VehicleModelService(IRepository<VehicleModel> vehicleModelRepository)
        {
            this._vehicleModelRepository = vehicleModelRepository;
        }

        public IList<VehicleModel> Get()
        {
            return _vehicleModelRepository.Get.ToList();
        }

        public VehicleModel Get(Guid Id)
        {
            return _vehicleModelRepository.Get.FirstOrDefault(it => it.Id == Id);
        }

        public VehicleModel Add(VehicleModel vehicleModel)
        {
            _vehicleModelRepository.Add(vehicleModel);
            return vehicleModel;
        }

        public VehicleModel Update(VehicleModel vehicleModel)
        {
            _vehicleModelRepository.Update(vehicleModel);
            return vehicleModel;
        }

        public bool Remove(Guid Id)
        {
            var vehicleModel = Get(Id);
            if (vehicleModel == null) return false;
            _vehicleModelRepository.Remove(vehicleModel);
            return true;
        }
    }
}
