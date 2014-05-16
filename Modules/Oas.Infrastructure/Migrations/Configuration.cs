namespace TDevs.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using TDevs.Core.Constants;
    using TDevs.Domain;
    using TDevs.Core.Extensions;
    using TDevs.Domain.Entities;
    using Microsoft.AspNet.Identity;
    internal sealed class Configuration : DbMigrationsConfiguration<TDevs.Core.Infrastructure.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TDevs.Core.Infrastructure.DatabaseContext context)
        {
            var userManager = new UserManager<User>(new UserStore<User>(context));
            var roleManager = new RoleManager<Role>(new RoleStore<Role>(context));

            #region Migrate Roles
            if (!roleManager.RoleExists(UserRoles.Administrator)) context.Roles.AddOrUpdate(new Role()
            {
                Name = UserRoles.Administrator,
                Description = UserRoles.Administrator,
                Predefined = UserRoles.Administrator,

                ReferenceCanEdit=true,
                AllowDashboard=true,
                //User Permission
                UserCanCreate = true,
                UserCanViewList = true,
                UserCanEdit = true,
                UserCanDelete = true,

                //Member
                ViewDeletedMembers = true,
                ViewMemberData = true,
                EditMemberData=true,
                DeleteMemberData=true,
                ViewMemberCashiering=true,
                

                //Agent
                AgentCanCreate = true,

                //Account
                AccountCanEdit = true,
                AccountCanView = true,

                //Medallion
                MedallionCanDelete = true,

                //Role
                RoleCanCreate = true,
                RoleCanDelete = true,
                RoleCanEdit = true,
                RoleCanViewList = true,

                //Report
                ReportCanMakeEndOfDayTrans = true,
                ReportCanMakeReport = true

            });

            if (!roleManager.RoleExists(UserRoles.Dispatcher)) context.Roles.AddOrUpdate(new Role() { Name = UserRoles.Dispatcher, Description = UserRoles.Dispatcher, Predefined = UserRoles.Dispatcher });
            if (!roleManager.RoleExists(UserRoles.Cashier)) context.Roles.AddOrUpdate(new Role() { Name = UserRoles.Cashier, Description = UserRoles.Cashier, Predefined = UserRoles.Cashier });
            if (!roleManager.RoleExists(UserRoles.Employee)) context.Roles.AddOrUpdate(new Role() { Name = UserRoles.Employee, Description = UserRoles.Employee, Predefined = UserRoles.Employee });

            #endregion

            #region Migrate default User
            var admin = new User()
            {
                UserName = "admin",
                FirstName = "Khoa",
                LastName = "Ho Tan",
                State = "MN",
                Zip = "75049",
                Address1 = "228 Chau Thi Vinh Te",
                City = "Quang Nam",
                Phone1 = "+01987542147",
                Active = true,
                Email = "hotankhoa.qn@gmail.com",
                CreateDate = DateTime.Now
            };

            var cashier = new User()
            {
                UserName = "cashier",
                FirstName = "Anh",
                LastName = "Vo",
                State = "MN",
                Zip = "75049",
                Address1 = "Tuyen Son",
                City = "Da Nang",
                Phone1 = "+01987542147",
                Active = true,
                Email = "voanh85@gmail.com",
                CreateDate = DateTime.Now
            };

            var dispatcher = new User()
            {
                UserName = "dispatcher",
                FirstName = "Do",
                LastName = "Duong Van",
                State = "MN",
                Zip = "75049",
                Address1 = "Nguyen Phuoc Nguyen",
                City = "Da Nang",
                Phone1 = "+01987542147",
                Active = true,
                Email = "duongquangdo2011@gmail.com",
                CreateDate = DateTime.Now
            };

            var employee = new User()
            {
                UserName = "employee",
                FirstName = "Phuong",
                LastName = "Le Thi Minh",
                State = "MN",
                Zip = "75049",
                Address1 = "Hoa Khanh",
                City = "Da Nang",
                Phone1 = "+01987542147",
                Active = true,
                Email = "phuongdue.dn@gmail.com",
                CreateDate = DateTime.Now
            };

            if (userManager.Find("admin", "abcde12345-") == null)
            {
                var resultAdmin = userManager.Create(admin, "abcde12345-");
                if (resultAdmin.Succeeded)
                    userManager.AddToRole(admin.Id, UserRoles.Administrator);
            }

            if (userManager.Find("cashier", "abcde12345-") == null)
            {
                var resultMember = userManager.Create(cashier, "abcde12345-");
                if (resultMember.Succeeded)
                    userManager.AddToRole(cashier.Id, UserRoles.Cashier);
            }

            if (userManager.Find("dispatcher", "abcde12345-") == null)
            {
                var resultContact = userManager.Create(dispatcher, "abcde12345-");
                if (resultContact.Succeeded)
                    userManager.AddToRole(dispatcher.Id, UserRoles.Dispatcher);

            }

            if (userManager.Find("employee", "abcde12345-") == null)
            {
                var resultContact = userManager.Create(employee, "abcde12345-");
                if (resultContact.Succeeded)
                    userManager.AddToRole(employee.Id, UserRoles.Employee);

            }
            #endregion

            #region Migrate company setting default
            var company = new Company()
            {
                Id = Guid.NewGuid(),
                Address1 = "228 Chau Thi Vinh Te",
                AgentAddress1 = "91 Tran Cao Van",
                AgentCity = "Da Nang",
                AgentName = "Ho Tan Khoa",
                AgentPhone = "0905746810",
                AgentState = "HC",
                AgentZip = "0511",
                AutoAssignAccountNumber = true,
                AutomaticCashiering = true,
                City = "Quang Nam",
                Comment = "Test Data",
                CreditCardFee = 1.0M,
                DefaultLateCharge = 0.01M,
                DefaultWorkComp = 0.1M,
                Email = "hotankhoa.qn@gmail.com",
                DefaultLateBreak = 0.5M,
                Name = "Company Default Settings",
                Phone1 = "0976948876",
                PaymentType = "Paypal",
                ReceiptMessage = "@!$@#",
                State = "MN",
                ActualCollisionRateSet = 50,
                ActualRateSet = 50,
                TaxiRepresentativeName = "Taxi Representative"

            };
            var exist = context.Companies.Any(c => c.Name.Equals(company.Name));
            if (exist == false)
                context.Companies.AddOrUpdate(company);
            #endregion

            #region Default Vehicle  Settings

            var feature1 = new VehicleFeature()
            {
                Id = Guid.NewGuid(),
                Name = "Feature 1",
                Description = "Test Data"
            };
            var feature2 = new VehicleFeature()
            {
                Id = Guid.NewGuid(),
                Name = "Feature 2",
                Description = "Test Data"
            };
            context.VehicleFeatures.AddOrUpdate(feature1);
            context.VehicleFeatures.AddOrUpdate(feature2);
            context.SaveChanges();

            var make1 = new VehicleMake()
            {
                Id = Guid.NewGuid(),
                Name = "Make 1"

            };

            var make2 = new VehicleMake()
            {
                Name = "Make 2",
                Id = Guid.NewGuid()
            };
            context.VehicleMakes.AddOrUpdate(make1);
            context.VehicleMakes.AddOrUpdate(make2);

            context.SaveChanges();

            var model1 = new VehicleModel()
            {
                Id = Guid.NewGuid(),
                Name = "Model 1",
                VehicleMakeId = make1.Id,
            };

            var model2 = new VehicleModel()
            {
                Id = Guid.NewGuid(),
                Name = "Model 2",
                VehicleMakeId = make2.Id,
            };

            context.VehicleModels.AddOrUpdate(model1);
            context.VehicleModels.AddOrUpdate(model2);

            context.SaveChanges();
            #endregion

            #region Setup Meter Manufacture
            var meter1 = new MeterManufacturer()
            {
                Description = "Manufacture 1"
            };
            var meter2 = new MeterManufacturer()
            {
                Id = Guid.NewGuid(),
                Description = "Manufacture 2"
            };

            var meter3 = new MeterManufacturer()
            {
                Id = Guid.NewGuid(),
                Description = "Manufacture 3"
            };

            context.MeterManufactureres.AddOrUpdate(meter1);
            context.MeterManufactureres.AddOrUpdate(meter2);
            context.MeterManufactureres.AddOrUpdate(meter3);
            context.SaveChanges();



            #endregion

            #region Setup State
            string[] states = { "MN", "KS", "TX", "LS", "LA", "NY", "OH" };
            foreach (var item in states)
            {
                var state = new State() { Name = item, Id = Guid.NewGuid() };
                context.Sates.AddOrUpdate(state);
                context.SaveChanges();
            }
            #endregion
        }
    }
}
