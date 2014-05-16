using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

using System.Threading.Tasks;
using TDevs.Core.Infrastructure;
using TDevs.Domain;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TDevs.Services
{
    public class UniversalAgentRecordService : IUniversalAgentRecordService
    {
        private readonly IRepository<UniversalAgentRecord> universalAgentRecordRepository;
        public UniversalAgentRecordService(IRepository<UniversalAgentRecord> universalAgentRecordRepository)
        {
            this.universalAgentRecordRepository = universalAgentRecordRepository;
        }

        public IList<UniversalAgentRecord> Get()
        {
            return universalAgentRecordRepository.Get.ToList();
        }

        public UniversalAgentRecord Get(Guid Id)
        {
            return universalAgentRecordRepository.Get.FirstOrDefault(c => c.Id == Id);
        }

        public UniversalAgentRecord Add(UniversalAgentRecord universalAgentRecord)
        {
            universalAgentRecordRepository.Add(universalAgentRecord);
            return universalAgentRecord;
        }

        public UniversalAgentRecord Update(UniversalAgentRecord universalAgentRecord)
        {
            universalAgentRecordRepository.Update(universalAgentRecord);
            return universalAgentRecord;
        }

        public bool Remove(Guid Id)
        {
            var universalAgentRecord = Get(Id);
            if (universalAgentRecord == null) return false;
            universalAgentRecordRepository.Remove(universalAgentRecord);
            return true;
        }

        public bool BulkInsertUniversal(List<UniversalAgentRecord> list)
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
                            SqlCommand cmd = new SqlCommand("Delete  from UniversalAgentRecords",con,tran);
                            int rowAffect = cmd.ExecuteNonQuery();                            
                            if (rowAffect > 0)
                            {
                                
                                SqlBulkCopy bc = new SqlBulkCopy(con,
                                SqlBulkCopyOptions.CheckConstraints |
                                SqlBulkCopyOptions.FireTriggers |
                                SqlBulkCopyOptions.KeepNulls, tran);

                                bc.BatchSize = 1000;
                                bc.DestinationTableName = "UniversalAgentRecords";
                                bc.WriteToServer(reader);
                            }
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
