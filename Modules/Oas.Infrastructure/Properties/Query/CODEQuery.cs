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
    public class CODEQuery : OrderedQueryBase<CODE>
    {
        public CODEQuery(OasDbContext context, Expression<Func<CODE, bool>> predicate)
            : base(context)
        {

            OriginalQuery = context.CODEs.Where(predicate)
                                .OrderBy(t => t.CreateDate);

            Query = OriginalQuery;
        }
        public override IEnumerable<CODE> Execute()
        {
            return Query.ToList();
        }

        public override int Count()
        {
            return OriginalQuery.Count();
        }
    }
}
