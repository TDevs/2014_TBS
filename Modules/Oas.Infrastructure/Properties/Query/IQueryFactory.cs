using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Oas.Domain.Entities;


namespace Oas.Core.Infrastructure
{
    public interface IQueryFactory
    {
        IOrderedQuery<User> CreateUserList(Expression<Func<User, bool>> predicate, int start, int max);
        IOrderedQuery<CODE> CreateCODEList(Expression<Func<CODE, bool>> predicate, int start, int max);
        IOrderedQuery<CBS_WOs> CreateCBS_WOsList(Expression<Func<CBS_WOs, bool>> predicate, int start, int max);
        IOrderedQuery<CBS_WOposts> CreateCBS_WOpostsList(Expression<Func<CBS_WOposts, bool>> predicate, int start, int max);
        IOrderedQuery<ConfigRouter> CreateConfigRouterList(Expression<Func<ConfigRouter, bool>> predicate, int start, int max);
        IOrderedQuery<CBS_SCs> CreateCBS_SCsList(Expression<Func<CBS_SCs, bool>> predicate, int start, int max);
        IOrderedQuery<CBS_SCposts> CreateCBS_SCpostsList(Expression<Func<CBS_SCposts, bool>> predicate, int start, int max);
    }
}
