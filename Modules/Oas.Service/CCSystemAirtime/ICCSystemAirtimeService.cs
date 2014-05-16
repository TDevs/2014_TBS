using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;

namespace TDevs.Services
{
    public interface ICCSystemAirtimeService
    {
        IList<CCSystemAirtime> Get();

        CCSystemAirtime Get(Guid Id);

        CCSystemAirtime Add(CCSystemAirtime ccsystemAirtime);

        CCSystemAirtime Update(CCSystemAirtime ccsystemAirtime);

        bool Remove(Guid Id);

        CCSystemAirtime GetByMedallionAndMemberId(Guid memberId, Guid medallionId);
    }
}
