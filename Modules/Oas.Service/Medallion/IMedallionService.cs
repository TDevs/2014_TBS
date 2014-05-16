using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;
using TDevs.Domain.ViewModel;

namespace TDevs.Services
{
    public interface IMedallionService
    {
        IList<Medallion> Get();

        Medallion Get(Guid Id);

        Medallion Add(Medallion medallion);

        Medallion Update(Medallion medallion);

        bool Remove(Guid Id);

        IList<Medallion> GetMemberMedallions(Guid memberId);

        List<MedallionLoan> GetMedallionLoanByMemberId(Guid memberId);
    }
}
