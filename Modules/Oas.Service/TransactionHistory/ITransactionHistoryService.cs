using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;

namespace TDevs.Services
{
    public interface ITransactionHistoryService
    {
        IList<TransactionHistory> Get();

        TransactionHistory Get(Guid Id);

        TransactionHistory Add(TransactionHistory transactionHistory);

        TransactionHistory Update(TransactionHistory transactionHistory);

        bool Remove(Guid Id);
         

        List<TransactionHistory> GetListTransactionByMedallionMemberId(Guid memberId, Guid medallionId);
        List<TransactionHistory> GetLisTransactionHistoryReport(Guid memberId, Guid medallionId);
    }
}
