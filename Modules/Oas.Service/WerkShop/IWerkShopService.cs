using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;

namespace TDevs.Services
{
    public interface IWerkShopService
    {
        IList<WerkShop> Get();

        WerkShop Get(Guid Id);

        WerkShop Add(WerkShop werkshop);

        WerkShop Update(WerkShop werkshop);

        bool Remove(Guid Id);
    }
}
