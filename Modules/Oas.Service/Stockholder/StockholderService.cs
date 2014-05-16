using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Core.Infrastructure;
using TDevs.Domain;
using TDevs.Domain.Entities;

namespace TDevs.Services
{
    public class StockholderService : IStockholderService
    {
        private readonly IRepository<Stockholder> stockholderRepository;
        public StockholderService(IRepository<Stockholder> stockholderRepository)
        {
            this.stockholderRepository = stockholderRepository;
        }

        public IList<Stockholder> Get()
        {
            return stockholderRepository.Get.ToList();
        }

        public Stockholder Get(Guid Id)
        {
            return stockholderRepository.Get.FirstOrDefault(c => c.Id == Id);
        }

        public Stockholder Add(Stockholder stockholder)
        {
            stockholderRepository.Add(stockholder);
            return stockholder;
        }

        public Stockholder Update(Stockholder stockholder)
        {
            stockholderRepository.Update(stockholder);
            return stockholder;
        }

        public bool Remove(Guid Id)
        {
            var stockholder = Get(Id);
            if(stockholder==null)return false;
            stockholderRepository.Remove(stockholder);
            return true;
        }
    }
}
