using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;
using TDevs.Domain.Entities;

namespace TDevs.Services
{
    public interface IUniversalAgentRecordService
    {
        IList<UniversalAgentRecord> Get();

        UniversalAgentRecord Get(Guid Id);

        UniversalAgentRecord Add(UniversalAgentRecord universalAgentRecord);

        UniversalAgentRecord Update(UniversalAgentRecord universalAgentRecord);

        bool Remove(Guid Id);

        bool BulkInsertUniversal(List<UniversalAgentRecord> list);
    }
}
