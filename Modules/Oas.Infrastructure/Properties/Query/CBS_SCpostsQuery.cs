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
    public class CBS_SCpostsQuery : OrderedQueryBase<CBS_SCposts>
    {
        public CBS_SCpostsQuery(OasDbContext context, Expression<Func<CBS_SCposts, bool>> predicate)
            : base(context)
        {

            OriginalQuery = context.CBS_SCposts.Where(predicate)
                                .OrderBy(t => t.CreateDate);

            Query = OriginalQuery;
        }
        public override IEnumerable<CBS_SCposts> Execute()
        {
            return Query.ToList();
        }

        public override int Count()
        {
            return OriginalQuery.Count();
        }
    }
}
