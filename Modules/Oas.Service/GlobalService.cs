using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Core.Infrastructure;
using TDevs.Domain.Entities;

namespace TDevs.Services
{
    public class GlobalService : IGlobalService
    {
        private readonly IRepository<State> stateRepository;
        public GlobalService(IRepository<State> stateRepository)
        {
            this.stateRepository = stateRepository;
        }
        public IList<Domain.Entities.State> GetStates()
        {
            var list = stateRepository.Get.ToList();
            return list;
        }
    }
}
