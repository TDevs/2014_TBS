using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;

namespace TDevs.Services
{
    public interface IVehicleModelService
    {
        IList<VehicleModel> Get();

        VehicleModel Get(Guid Id);

        VehicleModel Add(VehicleModel vehicleModel);

        VehicleModel Update(VehicleModel vehicleModel);

        bool Remove(Guid Id);
    }
}
