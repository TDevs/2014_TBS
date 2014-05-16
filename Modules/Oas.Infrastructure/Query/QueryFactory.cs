using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDevs.Domain;


namespace TDevs.Core.Infrastructure
{
    public class QueryFactory : IQueryFactory
    {
        #region Private
        private readonly IDbFactory dbContextFactory;
        private DatabaseContext dbContext;
        private DatabaseContext DbContext
        {
            get
            {
                return dbContext ?? (dbContext = dbContextFactory.Get());
            }
        }
        #endregion

        #region Constructor
        public QueryFactory(IDbFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }
        #endregion

        

    }
}
