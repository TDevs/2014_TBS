using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain.Entities;

namespace TDevs.Services
{
    public interface IGlobalService
    {
        IList<State> GetStates();
    }
}
