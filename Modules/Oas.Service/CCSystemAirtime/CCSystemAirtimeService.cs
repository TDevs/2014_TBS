using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Core.Infrastructure;
using TDevs.Domain;

namespace TDevs.Services
{
    public class CCSystemAirtimeService : ICCSystemAirtimeService
    {
        private readonly IRepository<CCSystemAirtime> ccsystemAirtimeRepository;
        public CCSystemAirtimeService(IRepository<CCSystemAirtime> ccsystemAirtimeRepository)
        {
            this.ccsystemAirtimeRepository = ccsystemAirtimeRepository;
        }

        public IList<CCSystemAirtime> Get()
        {
            return ccsystemAirtimeRepository.Get.ToList();
        }

        public CCSystemAirtime Get(Guid Id)
        {
            return ccsystemAirtimeRepository.Get.FirstOrDefault(c => c.Id == Id);
        }

        public CCSystemAirtime Add(CCSystemAirtime ccsystemAirtime)
        {
            ccsystemAirtimeRepository.Add(ccsystemAirtime);
            return ccsystemAirtime;
        }

        public CCSystemAirtime Update(CCSystemAirtime ccsystemAirtime)
        {
            ccsystemAirtimeRepository.Update(ccsystemAirtime);
            return ccsystemAirtime;
        }

        public bool Remove(Guid Id)
        {
            var ccsystemAirtime = Get(Id);
            if (ccsystemAirtime == null) return false;
            ccsystemAirtimeRepository.Remove(ccsystemAirtime);
            return true;
        }


        public CCSystemAirtime GetByMedallionAndMemberId(Guid memberId, Guid medallionId)
        {
            var ccsys = ccsystemAirtimeRepository.Get.Where(c => c.MemberId == memberId && c.MedallionId == medallionId).FirstOrDefault();
            return ccsys ?? new CCSystemAirtime();
        }
    }
}
