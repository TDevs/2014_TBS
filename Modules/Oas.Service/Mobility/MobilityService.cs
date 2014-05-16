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
    public class MobilityService : IMobilityService
    {
        private readonly IRepository<Mobility> mobilityRepository;
        public MobilityService(IRepository<Mobility> mobilityRepository)
        {
            this.mobilityRepository = mobilityRepository;
        }

        public IList<Mobility> Get()
        {
            return mobilityRepository.Get.ToList();
        }

        public Mobility Get(Guid Id)
        {
            return mobilityRepository.Get.FirstOrDefault(c => c.Id == Id);
        }

        public Mobility Add(Mobility mobility)
        {
            mobilityRepository.Add(mobility);
            return mobility;
        }

        public Mobility Update(Mobility mobility)
        {
            mobilityRepository.Update(mobility);
            return mobility;
        }

        public bool Remove(Guid Id)
        {
            var mobility = Get(Id);
            if (mobility == null) return false;
            mobilityRepository.Remove(mobility);
            return true;
        }

        public bool BulkInsertMobility(List<Mobility> list)
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
                            bc.DestinationTableName = "Mobilities";
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
