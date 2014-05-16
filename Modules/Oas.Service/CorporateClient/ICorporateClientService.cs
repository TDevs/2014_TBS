using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;

namespace TDevs.Services
{
    public interface ICorporateClientService
    {
        IList<CorporateClient> Get();

        CorporateClient Get(Guid Id);

        CorporateClient Add(CorporateClient corporateClient);

        CorporateClient Update(CorporateClient corporateClient);

        bool Remove(Guid Id);
    }
}
