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
    public class BillService : IBillService
    {
        private readonly IRepository<Bill> billRepository;
        public BillService(IRepository<Bill> billRepository)
        {
            this.billRepository = billRepository;
        }

        public IList<Bill> Get()
        {
            return billRepository.Get.ToList();
        }

        public Bill Get(Guid Id)
        {
            return billRepository.Get.FirstOrDefault(c => c.Id == Id);
        }

        public Bill Add(Bill bill)
        {
            billRepository.Add(bill);
            return bill;
        }

        public Bill Update(Bill bill)
        {
            billRepository.Update(bill);
            return bill;
        }

        public bool Remove(Guid Id)
        {
            var bill = Get(Id);
            if (bill == null) return false;
            billRepository.Remove(bill);
            return true;
        }


        public IList<Bill> GetBillByMemberAndMedallionId(Guid memberId, Guid medallionId)
        {
            var listbill = billRepository.Get.Where(c => c.MemberId == memberId && c.MedallionId == medallionId && c.IsDeleted==false).ToList();
            return listbill;
        }
    }
}
