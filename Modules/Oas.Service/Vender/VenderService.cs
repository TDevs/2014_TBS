using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Core.Infrastructure;
using TDevs.Domain;

namespace TDevs.Services
{
    public class VenderService : IVenderService
    {
        private readonly IRepository<CCVendor> venderRepository;
        public VenderService(IRepository<CCVendor> venderRepository)
        {
            this.venderRepository = venderRepository;
        }

        public IList<CCVendor> Get()
        {
            return venderRepository.Get.ToList();
        }

        public CCVendor Get(Guid Id)
        {
            return venderRepository.Get.FirstOrDefault(c => c.Id == Id);
        }

        public CCVendor Add(CCVendor ccVendor)
        {
            venderRepository.Add(ccVendor);
            return ccVendor;
        }

        public CCVendor Update(CCVendor ccVendor)
        {
            venderRepository.Update(ccVendor);
            return ccVendor;
        }

        public bool Remove(Guid Id)
        {
            var ccVendor = Get(Id);
            if (ccVendor == null) return false;
            venderRepository.Remove(ccVendor);
            return true;
        }
    }
}
