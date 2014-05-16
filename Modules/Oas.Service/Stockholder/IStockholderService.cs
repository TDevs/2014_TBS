using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;
 


namespace TDevs.Services
{
    public interface IStockholderService
    {
        IList<Stockholder> Get();

        Stockholder Get(Guid Id);

        Stockholder Add(Stockholder stockholder);

        Stockholder Update(Stockholder stockholder);

        bool Remove(Guid Id);
    }
}
