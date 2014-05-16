using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;
using TDevs.Domain.Entities;

namespace TDevs.Services
{
    public interface IBillService
    {
        IList<Bill> Get();

        Bill Get(Guid Id);

        Bill Add(Bill bill);

        Bill Update(Bill bill);

        bool Remove(Guid Id);

        IList<Bill> GetBillByMemberAndMedallionId(Guid memberId, Guid medallionId);
    }
}
