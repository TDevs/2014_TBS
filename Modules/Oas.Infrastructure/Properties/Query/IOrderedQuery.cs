using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Oas.Core.Infrastructure
{
    public interface IOrderedQuery<TResult> : IQuery<IEnumerable<TResult>>
        where TResult : class
    {
        int Count();
        IOrderedQuery<TResult> OrderBy<TKey>(Expression<Func<TResult, TKey>> orderBy);
        IOrderedQuery<TResult> ThenBy<TKey>(Expression<Func<TResult, TKey>> orderBy);
        IOrderedQuery<TResult> OrderByDescending<TKey>(Expression<Func<TResult, TKey>> orderBy);
        IOrderedQuery<TResult> ThenByDescending<TKey>(Expression<Func<TResult, TKey>> orderBy);
        IOrderedQuery<TResult> Page(int pageIndex, int pageSize);
        IOrderedQuery<TResult> Limit(int max);
    }
}
