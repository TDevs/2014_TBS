using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;

namespace TDevs.Services
{
    public interface IMeterManufacturerService
    {
        IList<MeterManufacturer> Get();

        MeterManufacturer Get(Guid Id);

        MeterManufacturer Add(MeterManufacturer meterManufacturer);

        MeterManufacturer Update(MeterManufacturer meterManufacturer);

        bool Remove(Guid Id);
    }
}
