using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Core.Infrastructure;
using TDevs.Domain;

namespace TDevs.Services
{
    public class ModelYearInsuranceService : IModelYearInsuranceService
    {
        private readonly IRepository<ModelYearInsurance> _modelYearInsuranceRepository;
        public ModelYearInsuranceService(IRepository<ModelYearInsurance> modelYearInsuranceRepository)
        {
            this._modelYearInsuranceRepository = modelYearInsuranceRepository;
        }

        public IList<ModelYearInsurance> Get()
        {
            return _modelYearInsuranceRepository.Get.ToList();
        }

        public ModelYearInsurance Get(Guid Id)
        {
            return _modelYearInsuranceRepository.Get.FirstOrDefault(c => c.Id == Id);
        }

        public ModelYearInsurance Add(ModelYearInsurance modelYearInsurance)
        {
            _modelYearInsuranceRepository.Add(modelYearInsurance);
            return modelYearInsurance;
        }

        public ModelYearInsurance Update(ModelYearInsurance modelYearInsurance)
        {
            _modelYearInsuranceRepository.Update(modelYearInsurance);
            return modelYearInsurance;
        }

        public bool Remove(Guid Id)
        {
            var modelYearInsurance = Get(Id);
            if (modelYearInsurance == null) return false;
            _modelYearInsuranceRepository.Remove(modelYearInsurance);
            return true;
        }
    }
}
