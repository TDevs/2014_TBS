using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Core.Infrastructure
{
    public abstract class QueryBase<TResult> : IQuery<TResult>
    {
        protected readonly OasDbContext context;

        public QueryBase(OasDbContext context)
        {
            this.context = context;
        }

        public abstract TResult Execute();
    }
}
