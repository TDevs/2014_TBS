[assembly: WebActivator.PreApplicationStartMethod(typeof(TBS.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(TBS.Web.App_Start.NinjectWebCommon), "Stop")]

namespace TBS.Web.App_Start
{
    using System;
    using System.Data.Entity;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Mvc;
    using TDevs.Core.Infrastructure;
    using TDevs.Domain;
    using TDevs.Domain.Entities;
    using TDevs.Services;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            INinjectSettings settings = new NinjectSettings
            {
                UseReflectionBasedInjection = true,    // disable code generation for partial trust
                InjectNonPublic = false,               // disable private reflection for partial trust
                InjectParentPrivateProperties = false, // reduce magic
                LoadExtensions = false                 // reduce magic
            };

            var kernel = new StandardKernel(settings);
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<DbContext>().To<DatabaseContext>();

            #region Service
            kernel.Bind<IAccountService>().To<AccountService>();
            kernel.Bind<IBankService>().To<BankService>();
            kernel.Bind<IGlobalService>().To<GlobalService>();
            kernel.Bind<IChargeBackTypeService>().To<ChargeBackTypeService>();
            kernel.Bind<ICompanyService>().To<CompanyService>();
            kernel.Bind<IModelYearInsuranceService>().To<ModelYearInsuranceService>();
            kernel.Bind<IVehicleFeatureService>().To<VehicleFeatureService>();
            kernel.Bind<IVehicleMakeService>().To<VehicleMakeService>();
            kernel.Bind<IVehicleModelService>().To<VehicleModelService>();
            kernel.Bind<IVenderService>().To<VenderService>();
            kernel.Bind<IWerkShopService>().To<WerkShopService>();
            kernel.Bind<ICorporateClientService>().To<CorporateClientService>();

            kernel.Bind<IMemberService>().To<MemberService>();
            kernel.Bind<IStockholderService>().To<StockholderService>();
            kernel.Bind<IAgentService>().To<AgentService>();
            kernel.Bind<IAgentVehicleService>().To<AgentVehicleService>();
            kernel.Bind<IContactService>().To<ContactService>();
            kernel.Bind<IVehicleService>().To<VehicleService>();
            kernel.Bind<IMeterManufacturerService>().To<MeterManufacturerService>();
            kernel.Bind<IMedallionService>().To<MedallionService>();
            kernel.Bind<IStandardDuesService>().To<StandardDuesService>();
            kernel.Bind<ILoanService>().To<LoanService>();
            kernel.Bind<INotificationService>().To<NotificationService>();

            kernel.Bind<IInsuranceDepositService>().To<InsuranceDepositService>();
            kernel.Bind<IAccountReceivableService>().To<AccountReceivableService>();             
            kernel.Bind<ICCSystemAirtimeService>().To<CCSystemAirtimeService>();            
            kernel.Bind<ISavingDepositService>().To<SavingDepositService>();
            kernel.Bind<ITransactionHistoryService>().To<TransactionHistoryService>();
            kernel.Bind<ITransactionHistoryKeepTrackService>().To<TransactionHistoryKeepTrackService>();
            kernel.Bind<IBillService>().To<BillService>();
            kernel.Bind<ISateService>().To<SateService>();
            kernel.Bind<IGlobalService>().To<GlobalService>();

            kernel.Bind<IUniversalAgentRecordService>().To<UniversalAgentRecordService>();            
            kernel.Bind<IRTAService>().To<RTAService>();
            kernel.Bind<IMobilityService>().To<MobilityService>();
            kernel.Bind<ISupportService>().To<SupportService>();

            kernel.Bind<IInsuranceDepositAgentService>().To<InsuranceDepositAgentService>();
            kernel.Bind<IAccountReceivableAgentService>().To<AccountReceivableAgentService>();
            kernel.Bind<IAutoLoanSetupAgentService>().To<AutoLoanSetupAgentService>();
            kernel.Bind<ISavingDepositAgentService>().To<SavingDepositAgentService>();
            #endregion

            #region Repository           

            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));
            kernel.Bind(typeof(IRepository<User>)).To(typeof(Repository<User>));
            kernel.Bind(typeof(IRepository<Role>)).To(typeof(Repository<Role>));
            kernel.Bind(typeof(IRepository<IdentityRole>)).To(typeof(Repository<Role>));
            //kernel.Bind(typeof(IRepository<UserRole>)).To(typeof(Repository<UserRole>));
            //kernel.Bind(typeof(IRepository<IdentityUserRole>)).To(typeof(Repository<UserRole>));
            kernel.Bind(typeof(IRepository<Bank>)).To(typeof(Repository<Bank>));
            kernel.Bind(typeof(IRepository<CCVendor>)).To(typeof(Repository<CCVendor>));
            kernel.Bind(typeof(IRepository<ChargeBackType>)).To(typeof(Repository<ChargeBackType>));
            kernel.Bind(typeof(IRepository<CorporateClient>)).To(typeof(Repository<CorporateClient>));
            kernel.Bind(typeof(IRepository<ModelYearInsurance>)).To(typeof(Repository<ModelYearInsurance>));
            kernel.Bind(typeof(IRepository<VehicleFeature>)).To(typeof(Repository<VehicleFeature>));
            kernel.Bind(typeof(IRepository<VehicleMake>)).To(typeof(Repository<VehicleMake>));
            kernel.Bind(typeof(IRepository<VehicleModel>)).To(typeof(Repository<VehicleModel>));
            kernel.Bind(typeof(IRepository<WerkShop>)).To(typeof(Repository<WerkShop>));
            kernel.Bind(typeof(IRepository<Company>)).To(typeof(Repository<Company>));

            kernel.Bind(typeof(IRepository<Member>)).To(typeof(Repository<Member>));
            kernel.Bind(typeof(IRepository<Vehicle>)).To(typeof(Repository<Vehicle>));

            kernel.Bind(typeof(IRepository<Stockholder>)).To(typeof(Repository<Stockholder>));
            kernel.Bind(typeof(IRepository<Agent>)).To(typeof(Repository<Agent>));
            kernel.Bind(typeof(IRepository<AgentVehicle>)).To(typeof(Repository<AgentVehicle>));
            kernel.Bind(typeof(IRepository<Contact>)).To(typeof(Repository<Contact>));
            kernel.Bind(typeof(IRepository<MeterManufacturer>)).To(typeof(Repository<MeterManufacturer>));
            kernel.Bind(typeof(IRepository<Medallion>)).To(typeof(Repository<Medallion>));
            kernel.Bind(typeof(IRepository<StandardDue>)).To(typeof(Repository<StandardDue>));
            kernel.Bind(typeof(IRepository<Ticket>)).To(typeof(Repository<Ticket>));
            kernel.Bind(typeof(IRepository<TicketItem>)).To(typeof(Repository<TicketItem>));            
            kernel.Bind(typeof(IRepository<FAQ>)).To(typeof(Repository<FAQ>));
            kernel.Bind(typeof(IRepository<Tutorial>)).To(typeof(Repository<Tutorial>));
            kernel.Bind(typeof(IRepository<Notification>)).To(typeof(Repository<Notification>));

            kernel.Bind(typeof(IRepository<InsuranceDeposit>)).To(typeof(Repository<InsuranceDeposit>));
            kernel.Bind(typeof(IRepository<AccountReceivable>)).To(typeof(Repository<AccountReceivable>));
             
            kernel.Bind(typeof(IRepository<Loan>)).To(typeof(Repository<Loan>));
            kernel.Bind(typeof(IRepository<CCSystemAirtime>)).To(typeof(Repository<CCSystemAirtime>));
           
            kernel.Bind(typeof(IRepository<SavingDeposit>)).To(typeof(Repository<SavingDeposit>));
            kernel.Bind(typeof(IRepository<State>)).To(typeof(Repository<State>));
            kernel.Bind(typeof(IRepository<TransactionHistory>)).To(typeof(Repository<TransactionHistory>));
            kernel.Bind(typeof(IRepository<TransactionHistoryKeepTrack>)).To(typeof(Repository<TransactionHistoryKeepTrack>));
            kernel.Bind(typeof(IRepository<Bill>)).To(typeof(Repository<Bill>));
            kernel.Bind(typeof(IRepository<UniversalAgentRecord>)).To(typeof(Repository<UniversalAgentRecord>));
            kernel.Bind(typeof(IRepository<RTA>)).To(typeof(Repository<RTA>));
            kernel.Bind(typeof(IRepository<Mobility>)).To(typeof(Repository<Mobility>));

            kernel.Bind(typeof(IRepository<InsuranceDepositAgent>)).To(typeof(Repository<InsuranceDepositAgent>));
            kernel.Bind(typeof(IRepository<AccountReceivableAgent>)).To(typeof(Repository<AccountReceivableAgent>));
            kernel.Bind(typeof(IRepository<AutoLoanSetupAgent>)).To(typeof(Repository<AutoLoanSetupAgent>));
            kernel.Bind(typeof(IRepository<SavingDepositAgent>)).To(typeof(Repository<SavingDepositAgent>));
            #endregion

            #region Controllers

            #endregion

            #region Models
            kernel.Bind<InsuranceDeposit>().ToSelf().InRequestScope();
            kernel.Bind<AccountReceivable>().ToSelf().InRequestScope();
             
            kernel.Bind<CCSystemAirtime>().ToSelf().InRequestScope();
            
            kernel.Bind<SavingDeposit>().ToSelf().InRequestScope();

            kernel.Bind<User>().ToSelf().InRequestScope();
            kernel.Bind<Bank>().ToSelf().InRequestScope();
            kernel.Bind<CCVendor>().ToSelf().InRequestScope();
            kernel.Bind<ChargeBackType>().ToSelf().InRequestScope();
            kernel.Bind<CorporateClient>().ToSelf().InRequestScope();
            kernel.Bind<ModelYearInsurance>().ToSelf().InRequestScope();
            kernel.Bind<VehicleFeature>().ToSelf().InRequestScope();
            kernel.Bind<VehicleMake>().ToSelf().InRequestScope();
            kernel.Bind<VehicleModel>().ToSelf().InRequestScope();
            kernel.Bind<WerkShop>().ToSelf().InRequestScope();
            kernel.Bind<Company>().ToSelf().InRequestScope();

            kernel.Bind<Member>().ToSelf().InRequestScope();
            kernel.Bind<Stockholder>().ToSelf().InRequestScope();
            kernel.Bind<Agent>().ToSelf().InRequestScope();
            kernel.Bind<AgentVehicle>().ToSelf().InRequestScope();
            kernel.Bind<Contact>().ToSelf().InRequestScope();
            kernel.Bind<Vehicle>().ToSelf().InRequestScope();
            kernel.Bind<MeterManufacturer>().ToSelf().InRequestScope();
            kernel.Bind<Medallion>().ToSelf().InRequestScope();
            kernel.Bind<Ticket>().ToSelf().InRequestScope();
            kernel.Bind<StandardDue>().ToSelf().InRequestScope();
            kernel.Bind<State>().ToSelf().InRequestScope();
            kernel.Bind<TransactionHistory>().ToSelf().InRequestScope();
            kernel.Bind<TransactionHistoryKeepTrack>().ToSelf().InRequestScope();
            kernel.Bind<Bill>().ToSelf().InRequestScope();
            kernel.Bind<UniversalAgentRecord>().ToSelf().InRequestScope();

            kernel.Bind<InsuranceDepositAgent>().ToSelf().InRequestScope();
            kernel.Bind<AccountReceivableAgent>().ToSelf().InRequestScope();
            kernel.Bind<AutoLoanSetupAgent>().ToSelf().InRequestScope();            
            kernel.Bind<SavingDepositAgent>().ToSelf().InRequestScope();
            kernel.Bind<Loan>().ToSelf().InRequestScope();
            #endregion

            kernel.Load(Assembly.GetExecutingAssembly());
        }
    }
}

