using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Core.Infrastructure;
using TDevs.Domain;

namespace TDevs.Services
{
    public class MeterManufacturerService : IMeterManufacturerService
    {
        private readonly IRepository<MeterManufacturer> meterManufacturerRepository;
        public MeterManufacturerService(IRepository<MeterManufacturer> meterManufacturerRepository)
        {
            this.meterManufacturerRepository = meterManufacturerRepository;
        }

        public IList<MeterManufacturer> Get()
        {
            return meterManufacturerRepository.Get.ToList();
        }

        public MeterManufacturer Get(Guid Id)
        {
            return meterManufacturerRepository.Get.FirstOrDefault(c => c.Id == Id);
        }

        public MeterManufacturer Add(MeterManufacturer meterManufacturer)
        {
            meterManufacturerRepository.Add(meterManufacturer);
            return meterManufacturer;
        }

        public MeterManufacturer Update(MeterManufacturer meterManufacturer)
        {
            meterManufacturerRepository.Update(meterManufacturer);
            return meterManufacturer;
        }

        public bool Remove(Guid Id)
        {
            var meterManufacturer = Get(Id);
            if (meterManufacturer == null) return false;
            meterManufacturerRepository.Remove(meterManufacturer);
            return true;
        }
    }
}
