using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Linq.Expressions;
using Oas.Core.Infrastructure;
using Oas.Domain.Entities;

namespace Oas.Core.Infrastructure
{
    public class UserQuery : OrderedQueryBase<User>
    {
        public UserQuery(OasDbContext context, Expression<Func<User, bool>> predicate)
            : base(context)
        {

            OriginalQuery = context.Users.Where(predicate)
                                .OrderBy(t => t.CreateDate);

            Query = OriginalQuery;
        }
        public override IEnumerable<User> Execute()
        {
            return Query.ToList();
        }

        public override int Count()
        {
            return OriginalQuery.Count();
        }
    }    
}
