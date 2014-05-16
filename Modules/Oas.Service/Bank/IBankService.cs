using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;
using TDevs.Domain.Entities;

namespace TDevs.Services
{
    public interface IBankService
    {
        IList<Bank> Get();

        Bank Get(Guid Id);

        Bank Add(Bank bank);

        Bank Update(Bank bank);

        bool Remove(Guid Id);
    }
}
