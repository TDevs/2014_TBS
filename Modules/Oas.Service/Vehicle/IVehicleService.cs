using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;

namespace TDevs.Services
{
    public interface IVehicleService
    {
        IList<Vehicle> Get();

        Vehicle Get(Guid Id);

        IQueryable<Vehicle> GetByMember(Guid Id);

        Vehicle Add(Vehicle vehicle);

        Vehicle Update(Vehicle vehicle);

        bool Remove(Guid Id);
    }
}
