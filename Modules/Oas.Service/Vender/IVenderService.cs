using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;

namespace TDevs.Services
{
    public interface IVenderService
    {
        IList<CCVendor> Get();

        CCVendor Get(Guid Id);

        CCVendor Add(CCVendor vender);

        CCVendor Update(CCVendor vender);

        bool Remove(Guid Id);
    }
}
