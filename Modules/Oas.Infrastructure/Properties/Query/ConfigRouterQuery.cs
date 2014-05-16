using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Oas.Core.Infrastructure;
using Oas.Domain.Entities;

namespace Oas.Infrastructure.Query
{
    public class ConfigRouterQuery : OrderedQueryBase<ConfigRouter>
    {
        public ConfigRouterQuery(OasDbContext context, Expression<Func<ConfigRouter, bool>> predicate)
            : base(context)
        {

            OriginalQuery = context.ConfigRouters.Where(predicate)
                                .OrderBy(t => t.CreateDate);

            Query = OriginalQuery;
        }
        public override IEnumerable<ConfigRouter> Execute()
        {
            return Query.ToList();
        }

        public override int Count()
        {
            return OriginalQuery.Count();
        }
    }
}
