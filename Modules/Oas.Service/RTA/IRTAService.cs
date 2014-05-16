using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;

 

namespace TDevs.Services
{
    public interface IRTAService
    {
        IList<RTA> Get();

        RTA Get(Guid Id);

        RTA Add(RTA bank);

        RTA Update(RTA bank);

        bool Remove(Guid Id);

        bool BulkInsertRTA(List<RTA> list);
    }
}
