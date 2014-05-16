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
    public class SateService : ISateService
    {
        private readonly IRepository<State> _sateTypeRepository;
        public SateService(IRepository<State> sateTypeRepository)
        {
            this._sateTypeRepository = sateTypeRepository;
        }

        public IList<State> Get()
        {
            return _sateTypeRepository.Get.ToList();
        }

        public State Get(Guid Id)
        {
            return _sateTypeRepository.Get.FirstOrDefault(it => it.Id == Id);
        }

        public State Add(State sate)
        {
            _sateTypeRepository.Add(sate);
            return sate;
        }

        public State Update(State sate)
        {
            _sateTypeRepository.Update(sate);
            return sate;
        }

        public bool Remove(Guid Id)
        {
            var sate = Get(Id);
            if (sate == null) return false;
            _sateTypeRepository.Remove(sate);
            return true;
        }
    }
}
