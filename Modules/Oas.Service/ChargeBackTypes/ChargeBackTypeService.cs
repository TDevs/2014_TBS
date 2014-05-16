using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Core.Infrastructure;
using TDevs.Domain;

namespace TDevs.Services
{
    public class ChargeBackTypeService : IChargeBackTypeService
    {
        private readonly IRepository<ChargeBackType> chargeBackTypeRepository;
        public ChargeBackTypeService(IRepository<ChargeBackType> chargeBackTypeRepository)
        {
            this.chargeBackTypeRepository = chargeBackTypeRepository;
        }

        public IList<ChargeBackType> Get()
        {
            return chargeBackTypeRepository.Get.ToList();
        }

        public ChargeBackType Get(Guid Id)
        {
            return chargeBackTypeRepository.Get.FirstOrDefault(it => it.Id == Id);
        }

        public ChargeBackType Add(ChargeBackType chargeBackType)
        {
            chargeBackTypeRepository.Add(chargeBackType);
            return chargeBackType;
        }

        public ChargeBackType Update(ChargeBackType chargeBackType)
        {
            chargeBackTypeRepository.Update(chargeBackType);
            return chargeBackType;
        }

        public bool Remove(Guid Id)
        {
            var chargeBackType = Get(Id);
            if (chargeBackType == null) return false;
            chargeBackTypeRepository.Remove(chargeBackType);
            return true;
        }
    }
}
