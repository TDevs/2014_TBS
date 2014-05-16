using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;
using TDevs.Domain.Entities;

namespace TDevs.Services
{
    public interface ITransactionHistoryKeepTrackService
    {
        IList<TransactionHistoryKeepTrack> Get();

        TransactionHistoryKeepTrack Get(Guid Id);

        TransactionHistoryKeepTrack Add(TransactionHistoryKeepTrack transactionHistoryKeepTrack);

        TransactionHistoryKeepTrack Update(TransactionHistoryKeepTrack transactionHistoryKeepTrack);

        bool Remove(Guid Id);
    }
    
}
