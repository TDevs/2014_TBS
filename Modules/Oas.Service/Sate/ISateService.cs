using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;
using TDevs.Domain.Entities;

namespace TDevs.Services
{
    public interface ISateService
    {
        IList<State> Get();

        State Get(Guid Id);

        State Add(State sate);

        State Update(State sate);

        bool Remove(Guid Id);
    }
}
