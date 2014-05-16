using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;

namespace TDevs.Services
{
    public interface IVehicleMakeService
    {
        IList<VehicleMake> Get();

        VehicleMake Get(Guid Id);

        VehicleMake Add(VehicleMake vehicleMake);

        VehicleMake Update(VehicleMake vehicleMake);

        bool Remove(Guid Id);
    }
}
