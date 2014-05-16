using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;

namespace TDevs.Services
{
    public interface IStandardDuesService
    {
        IList<StandardDue> Get();

        StandardDue Get(Guid Id);

        StandardDue Add(StandardDue standardDues);

        StandardDue Update(StandardDue standardDues);

        bool Remove(Guid Id);
    }
}
