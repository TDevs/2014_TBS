using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Core.Infrastructure;
using TDevs.Domain;

namespace TDevs.Services
{
    public class CorporateClientService : ICorporateClientService
    {
        private readonly IRepository<CorporateClient> corporateClientRepository;
        public CorporateClientService(IRepository<CorporateClient> corporateClientRepository)
        {
            this.corporateClientRepository =corporateClientRepository;
        }

        public IList<CorporateClient> Get()
        {
            return corporateClientRepository.Get.ToList();
        }

        public CorporateClient Get(Guid Id)
        {
            return corporateClientRepository.Get.FirstOrDefault(c => c.Id == Id);
        }

        public CorporateClient Add(CorporateClient corporateClient)
        {
            corporateClientRepository.Add(corporateClient);
            return corporateClient;
        }

        public CorporateClient Update(CorporateClient corporateClient)
        {
            corporateClientRepository.Update(corporateClient);
            return corporateClient;
        }

        public bool Remove(Guid Id)
        {
            var corporateClient = Get(Id);
            if(corporateClient==null)return false;
            corporateClientRepository.Remove(corporateClient);
            return true;
        }
    }
}
