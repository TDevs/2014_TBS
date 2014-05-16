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
    public class TransactionHistoryKeepTrackService : ITransactionHistoryKeepTrackService
    {
        private readonly IRepository<TransactionHistoryKeepTrack> transactionHistoryKeepTrackRepository;
        public TransactionHistoryKeepTrackService(IRepository<TransactionHistoryKeepTrack> transactionHistoryKeepTrackRepository)
        {
            this.transactionHistoryKeepTrackRepository = transactionHistoryKeepTrackRepository;
        }

        public IList<TransactionHistoryKeepTrack> Get()
        {
            return transactionHistoryKeepTrackRepository.Get.ToList();
        }

        public TransactionHistoryKeepTrack Get(Guid Id)
        {
            return transactionHistoryKeepTrackRepository.Get.FirstOrDefault(c => c.Id == Id);
        }

        public TransactionHistoryKeepTrack Add(TransactionHistoryKeepTrack transactionHistoryKeepTrack)
        {
            transactionHistoryKeepTrackRepository.Add(transactionHistoryKeepTrack);
            return transactionHistoryKeepTrack;
        }

        public TransactionHistoryKeepTrack Update(TransactionHistoryKeepTrack transactionHistoryKeepTrack)
        {
            transactionHistoryKeepTrackRepository.Update(transactionHistoryKeepTrack);
            return transactionHistoryKeepTrack;
        }

        public bool Remove(Guid Id)
        {
            var transactionHistoryKeepTrack = Get(Id);
            if (transactionHistoryKeepTrack== null) return false;
            transactionHistoryKeepTrackRepository.Remove(transactionHistoryKeepTrack);
            return true;
        }
    }
}
