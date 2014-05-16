using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Core.Infrastructure;
using TDevs.Domain;

namespace TDevs.Services
{
    public class StandardDuesService : IStandardDuesService
    {
        private readonly IRepository<StandardDue> standardDuesRepository;
        public StandardDuesService(IRepository<StandardDue> standardDuesRepository)
        {
            this.standardDuesRepository = standardDuesRepository;
        }

        public IList<StandardDue> Get()
        {
            return standardDuesRepository.Get.ToList();
        }

        public StandardDue Get(Guid Id)
        {
            return standardDuesRepository.Get.FirstOrDefault(c => c.Id == Id);
        }

        public StandardDue Add(StandardDue standardDues)
        {
            standardDuesRepository.Add(standardDues);
            return standardDues;
        }

        public StandardDue Update(StandardDue standardDues)
        {
            standardDuesRepository.Update(standardDues);
            return standardDues;
        }

        public bool Remove(Guid Id)
        {
            var standardDues = Get(Id);
            if (standardDues == null) return false;
            standardDuesRepository.Remove(standardDues);
            return true;
        }
    }
}
