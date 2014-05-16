using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;

namespace TDevs.Services
{
    public interface IVehicleFeatureService
    {
        IList<VehicleFeature> Get();

        VehicleFeature Get(Guid Id);

        VehicleFeature Add(VehicleFeature vehicleFeature);

        VehicleFeature Update(VehicleFeature vehicleFeature);

        bool Remove(Guid Id);
    }
}
