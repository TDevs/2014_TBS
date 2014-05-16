using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TDevs.Core.Infrastructure;
using TDevs.Domain;

namespace TDevs.Services
{
    public class TransactionHistoryService : ITransactionHistoryService
    {
        private readonly IRepository<TransactionHistory> transactionHistoryRepository;
        public TransactionHistoryService(IRepository<TransactionHistory> transactionHistoryRepository)
        {
            this.transactionHistoryRepository = transactionHistoryRepository;
        }

        public IList<TransactionHistory> Get()
        {
            return transactionHistoryRepository.Get.ToList();
        }

        public TransactionHistory Get(Guid Id)
        {
            return transactionHistoryRepository.Get.FirstOrDefault(c => c.Id == Id);
        }

        public TransactionHistory Add(TransactionHistory transactionHistory)
        {
            transactionHistoryRepository.Add(transactionHistory);
            return transactionHistory;
        }

        public TransactionHistory Update(TransactionHistory transactionHistory)
        {
            transactionHistoryRepository.Update(transactionHistory);
            return transactionHistory;
        }

        public bool Remove(Guid Id)
        {
            var transactionHistory = Get(Id);
            if (transactionHistory == null) return false;
            transactionHistoryRepository.Remove(transactionHistory);
            return true;
        }

        public List<TransactionHistory> GetListTransactionByMedallionMemberId(Guid memberId , Guid medallionId)
        {
            var list = transactionHistoryRepository.Get
                .Where(c => c.MemberId == memberId && c.MedallionId == medallionId && c.IsDeleted == false).ToList();
            return list;
        }

        public List<TransactionHistory> GetLisTransactionHistoryReport(Guid memberId, Guid medallionId)
        {
            var list = transactionHistoryRepository.Get
                .Where(c => c.MemberId == memberId && c.MedallionId == medallionId).ToList();
            return list;
        }

    }
}
