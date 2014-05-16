using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDevs.Core.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        DatabaseContext Get();
    }
}
