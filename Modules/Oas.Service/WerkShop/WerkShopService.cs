using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Core.Infrastructure;
using TDevs.Domain;

namespace TDevs.Services
{
    public class WerkShopService : IWerkShopService
    {
        private readonly IRepository<WerkShop> werkshopRepository;
        public WerkShopService(IRepository<WerkShop> werkshopRepository)
        {
            this.werkshopRepository =werkshopRepository;
        }

        public IList<WerkShop> Get()
        {
            return werkshopRepository.Get.ToList();
        }

        public WerkShop Get(Guid Id)
        {
            return werkshopRepository.Get.FirstOrDefault(c => c.Id == Id);
        }

        public WerkShop Add(WerkShop werkshop)
        {
            werkshopRepository.Add(werkshop);
            return werkshop;
        }

        public WerkShop Update(WerkShop werkshop)
        {
            werkshopRepository.Update(werkshop);
            return werkshop;
        }

        public bool Remove(Guid Id)
        {
            var werkshop = Get(Id);
            if(werkshop==null)return false;
            werkshopRepository.Remove(werkshop);
            return true;
        }
    }
}
