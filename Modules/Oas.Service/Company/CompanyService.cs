using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Core.Infrastructure;
using TDevs.Domain;
namespace TDevs.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepository<Company> companyRepository;
        public CompanyService(IRepository<Company> companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public IList<Company> Get()
        {
            return companyRepository.Get.ToList();
        }

        public Company Get(Guid Id)
        {
            return companyRepository.Get.FirstOrDefault(c => c.Id == Id);
        }

        public Company Add(Company company)
        {
            companyRepository.Add(company);
            return company;
        }

        public Company Update(Company company)
        {
            companyRepository.Update(company);
            return company;
        }

        public bool Remove(Guid Id)
        {
            var bank = Get(Id);
            if(bank==null)return false;
            companyRepository.Remove(bank);
            return true;
        }
    }
}
