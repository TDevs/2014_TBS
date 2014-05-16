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
    public class CBS_WOsQuery : OrderedQueryBase<CBS_WOs>
    {
        public CBS_WOsQuery(OasDbContext context, Expression<Func<CBS_WOs, bool>> predicate)
            : base(context)
        {

            OriginalQuery = context.CBS_WOs.Where(predicate)
                                .OrderBy(t => t.CreateDate);

            Query = OriginalQuery;
        }
        public override IEnumerable<CBS_WOs> Execute()
        {
            return Query.ToList();
        }

        public override int Count()
        {
            return OriginalQuery.Count();
        }
    }
}
