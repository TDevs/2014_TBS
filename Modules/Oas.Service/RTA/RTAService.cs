using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Core.Infrastructure;
using TDevs.Domain;
namespace TDevs.Services
{
    public class RTAService : IRTAService
    {
        private readonly IRepository<RTA> companyRepository;
        public RTAService(IRepository<RTA> companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public IList<RTA> Get()
        {
            return companyRepository.Get.ToList();
        }

        public RTA Get(Guid Id)
        {
            return companyRepository.Get.FirstOrDefault(c => c.Id == Id);
        }

        public RTA Add(RTA company)
        {
            companyRepository.Add(company);
            return company;
        }

        public RTA Update(RTA company)
        {
            companyRepository.Update(company);
            return company;
        }

        public bool Remove(Guid Id)
        {
            var bank = Get(Id);
            if (bank == null) return false;
            companyRepository.Remove(bank);
            return true;
        }

        public bool BulkInsertRTA(List<RTA> list)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TBSContext"].ConnectionString;
            if (!String.IsNullOrEmpty(connectionString))
            {
                try
                {
                    var result = (from a in list select a);
                    IDataReader reader = result.AsDataReader();
                    //bulk data to db 
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        using (SqlTransaction tran = con.BeginTransaction())
                        {
                            //delete all to insert 
                            SqlBulkCopy bc = new SqlBulkCopy(con,
                            SqlBulkCopyOptions.CheckConstraints |
                            SqlBulkCopyOptions.FireTriggers |
                            SqlBulkCopyOptions.KeepNulls, tran);

                            bc.BatchSize = 1000;
                            bc.DestinationTableName = "RTAs";
                            bc.WriteToServer(reader);

                            tran.Commit();
                        }
                        con.Close();
                        return true;
                    }
                }
                catch
                { return false; }
            }
            return false;
        }
    }
}
