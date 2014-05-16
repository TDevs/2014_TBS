using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDevs.Core.Infrastructure
{
    public abstract class QueryBase<TResult> : IQuery<TResult>
    {
        protected readonly DatabaseContext context;

        public QueryBase(DatabaseContext context)
        {
            this.context = context;
        }

        public abstract TResult Execute();
    }
}
