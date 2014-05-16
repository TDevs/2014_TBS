using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using TDevs.Domain;
using System.Data.Common;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Migrations;
using TDevs.Domain.Entities;
using System.Data.Entity.Infrastructure;


namespace TDevs.Core.Infrastructure
{
    public class DatabaseContext : IdentityDbContext<User>
    {

        public DatabaseContext()
            : base("name=TBSContext")
        {
            Database.SetInitializer<DatabaseContext>(null);
            //this.AutomaticMigrationsEnabled = true;
            
        }

        public IDbSet<Bank> Banks { get; set; }

        public IDbSet<CCVendor> CCVendors { get; set; }

        public IDbSet<ChargeBackType> ChargeBackTypes { get; set; }

        public IDbSet<CorporateClient> CorporateClients { get; set; }

        public IDbSet<ModelYearInsurance> ModelYearInsurances { get; set; }


        public IDbSet<VehicleFeature> VehicleFeatures { get; set; }

        public IDbSet<VehicleMake> VehicleMakes { get; set; }

        public IDbSet<VehicleModel> VehicleModels { get; set; }

        public IDbSet<WerkShop> WerkShops { get; set; }

        public IDbSet<Company> Companies { get; set; }

        public IDbSet<Role> Roles { get; set; }

        public IDbSet<Agent> Agents { get; set; }

        public IDbSet<AgentVehicle> AgentVehicles { get; set; }

        public IDbSet<Member> Members { get; set; }

        public IDbSet<Medallion> Medallions { get; set; }

        public IDbSet<Contact> Contacts { get; set; }

        public IDbSet<MeterManufacturer> MeterManufactureres { get; set; }

        public IDbSet<StandardDue> StandardDues { get; set; }

        public IDbSet<Stockholder> Stockholders { get; set; }

        public IDbSet<Ticket> Tickets { get; set; }

        public IDbSet<State> Sates { get; set; }

        public IDbSet<FAQ> FAQs { get; set; }

        public IDbSet<TicketItem> TicketItems { get; set; }

        public IDbSet<Tutorial> Tutorials { get; set; }

        public IDbSet<Notification> Notifications { get; set; }

        #region Cashiering
        public IDbSet<InsuranceDeposit> InsuranceDeposits { get; set; }
        public IDbSet<CCSystemAirtime> CCSystemAirtimes { get; set; }
      
        public IDbSet<AccountReceivable> AccountReceivables { get; set; }
        public IDbSet<SavingDeposit> SavingDeposites { get; set; }
        public IDbSet<Loan> Loans { get; set; }
        public IDbSet<TransactionHistory> TransactionHistories { get; set; }
        public IDbSet<TransactionHistoryKeepTrack> TransactionHistoryKeepTracks { get; set; }
        public IDbSet<Bill> Bills { get; set; }
        #endregion

        #region Agent 
        public IDbSet<InsuranceDepositAgent> InsuranceDepositAgents{ get; set; }
        public IDbSet<AutoLoanSetupAgent> AutoLoanSetupAgents { get; set; }
        public IDbSet<AccountReceivableAgent> AccountReceivableAgents { get; set; }
        public IDbSet<SavingDepositAgent> SavingDepositAgents { get; set; }

        public IDbSet<UniversalAgentRecord> UniversalAgentRecords { get; set; }
        public IDbSet<RTA> RTAs { get; set; }
        public IDbSet<Mobility> Mobilities{ get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Remove unused conventions
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
           // modelBuilder.Conventions.Remove<IncludeMetadataConvention>();

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUser>()
                .ToTable("Users");
            modelBuilder.Entity<User>()
                .ToTable("Users");

            modelBuilder.Entity<IdentityRole>()
                .ToTable("Roles");
            modelBuilder.Entity<Role>()
                .ToTable("Roles");

            modelBuilder.Entity<IdentityUserRole>()
                .ToTable("UserRoles");




        }
    }
}
