using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;

 

namespace TDevs.Services
{
    public interface IMobilityService
    {
        IList<Mobility> Get();

        Mobility Get(Guid Id);

        Mobility Add(Mobility mobility);

        Mobility Update(Mobility mobility);

        bool Remove(Guid Id);

        bool BulkInsertMobility(List<Mobility> list);
    }
}
