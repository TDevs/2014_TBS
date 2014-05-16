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
    public class CBSWOpostsQuery : OrderedQueryBase<CBS_WOposts>
    {
        public CBSWOpostsQuery(OasDbContext context, Expression<Func<CBS_WOposts, bool>> predicate)
            : base(context)
        {

            OriginalQuery = context.CBS_WOposts.Where(predicate)
                                .OrderBy(t => t.CreateDate);

            Query = OriginalQuery;
        }
        public override IEnumerable<CBS_WOposts> Execute()
        {
            return Query.ToList();
        }

        public override int Count()
        {
            return OriginalQuery.Count();
        }
    }
}
