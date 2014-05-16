using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Configuration;

namespace TDevs.Core.Infrastructure
{
    public class OasDbFactory : IDisposable, IDbFactory
    {

        private readonly DbProviderFactory providerFactory;
        private readonly string connectionString;
        private DatabaseContext dbContext;

        public OasDbFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public OasDbFactory(DbProviderFactory providerFactory, string connectionString)
        {
            this.providerFactory = providerFactory;
            this.connectionString = connectionString;
        }


        public DatabaseContext Get()
        {
            if (dbContext == null)
            {

                dbContext = new DatabaseContext();


                return dbContext;
            }

            return dbContext;
        }

        public void Dispose()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}

