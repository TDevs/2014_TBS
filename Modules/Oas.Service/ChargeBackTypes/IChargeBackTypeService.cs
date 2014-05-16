using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;

namespace TDevs.Services
{
    public interface IChargeBackTypeService
    {
        IList<ChargeBackType> Get();

        ChargeBackType Get(Guid Id);

        ChargeBackType Add(ChargeBackType chargeBackType);

        ChargeBackType Update(ChargeBackType chargeBackType);

        bool Remove(Guid Id);
    }
}
