using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace TDevs.Core.Infrastructure
{
    public abstract class OrderedQueryBase<TResult> : QueryBase<IEnumerable<TResult>>, IOrderedQuery<TResult>
        where TResult : class
    {
        protected IQueryable<TResult> OriginalQuery { get; set; }

        protected IQueryable<TResult> Query { get; set; }

        protected OrderedQueryBase(DatabaseContext context)
            : base(context)
        {
            OriginalQuery = context.Set<TResult>();
            Query = OriginalQuery;
        }

        protected OrderedQueryBase(DatabaseContext context, Expression<Func<TResult, bool>> predicate)
            : this(context)
        {
            OriginalQuery = context.Set<TResult>().Where(predicate);
            Query = OriginalQuery;
        }

        public virtual int Count()
        {
            return OriginalQuery.Count();
        }

        public IOrderedQuery<TResult> OrderBy<TKey>(Expression<Func<TResult, TKey>> orderBy)
        {
            Query = Query.OrderBy(orderBy);
            return this;
        }

        public IOrderedQuery<TResult> ThenBy<TKey>(Expression<Func<TResult, TKey>> orderBy)
        {
            Query = ((IOrderedQueryable<TResult>)Query).ThenBy(orderBy);
            return this;
        }

        public IOrderedQuery<TResult> OrderByDescending<TKey>(Expression<Func<TResult, TKey>> orderBy)
        {
            Query = Query.OrderByDescending(orderBy);
            return this;
        }

        public IOrderedQuery<TResult> ThenByDescending<TKey>(Expression<Func<TResult, TKey>> orderBy)
        {
            Query = ((IOrderedQueryable<TResult>)Query).ThenByDescending(orderBy);
            return this;
        }

        public IOrderedQuery<TResult> Page(int pageIndex, int pageSize)
        {
            Query = Query.Skip(pageIndex).Take(pageSize);
            return this;
        }

        public IOrderedQuery<TResult> Limit(int max)
        {
            Query = Query.Take(max);
            return this;
        }
    }
}
