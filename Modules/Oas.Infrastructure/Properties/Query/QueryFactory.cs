using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oas.Domain.Entities;
using Oas.Infrastructure.Query;

namespace Oas.Core.Infrastructure
{
    public class QueryFactory : IQueryFactory
    {
        #region Private
        private readonly IOasDbFactory dbContextFactory;
        private OasDbContext dbContext;
        private OasDbContext DbContext
        {
            get
            {
                return dbContext ?? (dbContext = dbContextFactory.Get());
            }
        }
        #endregion

        #region Constructor
        public QueryFactory(IOasDbFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }
        #endregion

        #region Public Method
        public IOrderedQuery<User> CreateUserList(System.Linq.Expressions.Expression<Func<User, bool>> predicate, int start, int max)
        {
            return new UserQuery(DbContext, predicate).Page(start,max);
        }
        
        public IOrderedQuery<CODE> CreateCODEList(System.Linq.Expressions.Expression<Func<CODE, bool>> predicate, int start, int max)
        {
            return new CODEQuery(DbContext, predicate).Page(start, max);
        }

        public IOrderedQuery<CBS_WOs> CreateCBS_WOsList(System.Linq.Expressions.Expression<Func<CBS_WOs, bool>> predicate, int start, int max)
        {
            return new CBS_WOsQuery(DbContext, predicate).Page(start, max);
        }

        public IOrderedQuery<CBS_WOposts> CreateCBS_WOpostsList(System.Linq.Expressions.Expression<Func<CBS_WOposts, bool>> predicate, int start, int max)
        {
            return new CBSWOpostsQuery(DbContext, predicate).Page(start, max);
        }

        public IOrderedQuery<ConfigRouter> CreateConfigRouterList(System.Linq.Expressions.Expression<Func<ConfigRouter, bool>> predicate, int start, int max)
        {
            return new ConfigRouterQuery(DbContext, predicate).Page(start, max);
        }

        public IOrderedQuery<CBS_SCs> CreateCBS_SCsList(System.Linq.Expressions.Expression<Func<CBS_SCs, bool>> predicate, int start, int max)
        {
            return new CBS_SCsQuery(DbContext, predicate).Page(start, max);
        }

        public IOrderedQuery<CBS_SCposts> CreateCBS_SCpostsList(System.Linq.Expressions.Expression<Func<CBS_SCposts, bool>> predicate, int start, int max)
        {
            return new CBS_SCpostsQuery(DbContext, predicate).Page(start, max);
        }
        #endregion
    }
}
