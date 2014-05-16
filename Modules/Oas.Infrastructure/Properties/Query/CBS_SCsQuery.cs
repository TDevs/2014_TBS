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
    public class CBS_SCsQuery : OrderedQueryBase<CBS_SCs>
    {
        public CBS_SCsQuery(OasDbContext context, Expression<Func<CBS_SCs, bool>> predicate)
            : base(context)
        {

            OriginalQuery = context.CBS_SCs.Where(predicate)
                                .OrderBy(t => t.CreateDate);

            Query = OriginalQuery;
        }
        public override IEnumerable<CBS_SCs> Execute()
        {
            return Query.ToList();
        }

        public override int Count()
        {
            return OriginalQuery.Count();
        }
    }
}
