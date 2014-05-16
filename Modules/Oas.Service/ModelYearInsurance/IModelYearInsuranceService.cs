using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;

namespace TDevs.Services
{
    public interface IModelYearInsuranceService
    {
        IList<ModelYearInsurance> Get();

        ModelYearInsurance Get(Guid Id);

        ModelYearInsurance Add(ModelYearInsurance modelYearInsurance);

        ModelYearInsurance Update(ModelYearInsurance modelYearInsurance);

        bool Remove(Guid Id);
    }
}
