using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Core.Infrastructure;
using TDevs.Domain;

namespace TDevs.Services
{
    public class BankService : IBankService
    {
        private readonly IRepository<Bank> bankRepository;
        public BankService(IRepository<Bank> bankRepository)
        {
            this.bankRepository = bankRepository;
        }

        public IList<Bank> Get()
        {
            return bankRepository.Get.ToList();
        }

        public Bank Get(Guid Id)
        {
            return bankRepository.Get.FirstOrDefault(c => c.Id == Id);
        }

        public Bank Add(Bank bank)
        {
            bankRepository.Add(bank);
            return bank;
        }

        public Bank Update(Bank bank)
        {
            bankRepository.Update(bank);
            return bank;
        }

        public bool Remove(Guid Id)
        {
            var bank = Get(Id);
            if(bank==null)return false;
            bankRepository.Remove(bank);
            return true;
        }
    }
}
