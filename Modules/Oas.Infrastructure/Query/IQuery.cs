using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDevs.Core.Infrastructure
{
    public interface IQuery<TResult>
    {
        TResult Execute();
    }
}
