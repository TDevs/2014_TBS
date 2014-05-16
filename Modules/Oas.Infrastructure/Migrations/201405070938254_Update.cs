namespace TDevs.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountReceivableAgents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AgentVehicleId = c.Guid(nullable: false),
                        AccountReceivableAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        PaymentTermMonth = c.Int(nullable: false),
                        Comments = c.String(maxLength: 400),
                        Status = c.String(maxLength: 20),
                        MonthlyPayment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AgentId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Agents", t => t.AgentId)
                .ForeignKey("dbo.AgentVehicles", t => t.AgentVehicleId)
                .Index(t => t.AgentId)
                .Index(t => t.AgentVehicleId);
            
            CreateTable(
                "dbo.Agents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MemberId = c.Guid(),
                        FirstName = c.String(maxLength: 200),
                        LastName = c.String(maxLength: 200),
                        Address = c.String(maxLength: 200),
                        Address1 = c.String(maxLength: 200),
                        City = c.String(maxLength: 200),
                        State = c.String(maxLength: 10),
                        Zip = c.String(maxLength: 5),
                        MobilePhone = c.String(maxLength: 200),
                        HomePhone = c.String(maxLength: 200),
                        Fax = c.String(maxLength: 200),
                        Email = c.String(maxLength: 200),
                        SSN = c.String(maxLength: 200),
                        Comment = c.String(maxLength: 400),
                        DateOfBirth = c.DateTime(nullable: false),
                        ChaufferLic = c.String(maxLength: 200),
                        DriveLic = c.String(maxLength: 200),
                        ContinueEducation = c.String(maxLength: 200),
                        DriverID = c.String(maxLength: 200),
                        LicenseState = c.String(maxLength: 200),
                        Status = c.String(maxLength: 200),
                        CTATAP = c.Boolean(nullable: false),
                        DayDriver = c.Boolean(nullable: false),
                        NightDriver = c.Boolean(nullable: false),
                        PictureAgent = c.String(maxLength: 400),
                        StartDate = c.DateTime(nullable: false),
                        ActiveDate = c.DateTime(nullable: false),
                        InactiveDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.MemberId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AccountNumber = c.String(maxLength: 200),
                        Name = c.String(maxLength: 200),
                        Address = c.String(maxLength: 200),
                        Address1 = c.String(maxLength: 200),
                        City = c.String(maxLength: 200),
                        State = c.String(maxLength: 10),
                        Zip = c.String(maxLength: 5),
                        Phone1 = c.String(maxLength: 200),
                        Phone2 = c.String(maxLength: 200),
                        Fax = c.String(maxLength: 200),
                        Email = c.String(maxLength: 200),
                        SSN = c.String(maxLength: 200),
                        IRIS = c.String(maxLength: 200),
                        Article = c.String(maxLength: 200),
                        Fein = c.String(maxLength: 200),
                        IncorporationDate = c.DateTime(),
                        JoinedDate = c.DateTime(),
                        Status = c.String(maxLength: 200),
                        UseThisAddressForBilling = c.Boolean(nullable: false),
                        PreferPayment = c.String(maxLength: 200),
                        LateBreaks = c.Int(nullable: false),
                        DefaultWorkerComp = c.Decimal(precision: 18, scale: 2),
                        RegisteredAgentName = c.String(maxLength: 200),
                        AgentAddress = c.String(maxLength: 200),
                        AgentAddress1 = c.String(maxLength: 200),
                        AgentCity = c.String(maxLength: 200),
                        AgentState = c.String(maxLength: 10),
                        AgentZip = c.String(maxLength: 5),
                        ReciepMessage = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StandardDues",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MemberId = c.Guid(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        Dues = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.MemberId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MemberId = c.Guid(nullable: false),
                        LicenseNumber = c.String(maxLength: 200),
                        VehicleMakeId = c.Guid(nullable: false),
                        VehicleModelId = c.Guid(nullable: false),
                        VehicleFeatureId = c.Guid(nullable: false),
                        MeterManufacturerId = c.Guid(nullable: false),
                        MedallionId = c.Guid(),
                        VINNumber = c.String(maxLength: 200),
                        InsurancePolicyNumber = c.String(maxLength: 200),
                        WorkmanCompNumber = c.String(maxLength: 200),
                        LicenseState = c.String(maxLength: 200),
                        RadioSerialNumber = c.String(maxLength: 200),
                        MeterNumber = c.String(maxLength: 200),
                        EINNumber = c.String(maxLength: 200),
                        ModelYear = c.String(maxLength: 200),
                        UnderServed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Medallions", t => t.MedallionId)
                .ForeignKey("dbo.Members", t => t.MemberId)
                .ForeignKey("dbo.MeterManufacturers", t => t.MeterManufacturerId)
                .ForeignKey("dbo.VehicleFeatures", t => t.VehicleFeatureId)
                .ForeignKey("dbo.VehicleMakes", t => t.VehicleMakeId)
                .ForeignKey("dbo.VehicleModels", t => t.VehicleModelId)
                .Index(t => t.MedallionId)
                .Index(t => t.MemberId)
                .Index(t => t.MeterManufacturerId)
                .Index(t => t.VehicleFeatureId)
                .Index(t => t.VehicleMakeId)
                .Index(t => t.VehicleModelId);
            
            CreateTable(
                "dbo.Medallions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MemberId = c.Guid(nullable: false),
                        MedallionNumber = c.String(maxLength: 200),
                        DateJoined = c.DateTime(nullable: false),
                        BillingStartDate = c.DateTime(nullable: false),
                        BillingEndDate = c.DateTime(nullable: false),
                        UnderServed = c.Boolean(nullable: false),
                        InsuranceSurcharge = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Collision = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.MemberId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.MeterManufacturers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VehicleFeatures",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VehicleMakes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VehicleModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        VehicleMakeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VehicleMakes", t => t.VehicleMakeId)
                .Index(t => t.VehicleMakeId);
            
            CreateTable(
                "dbo.AgentVehicles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AgentId = c.Guid(nullable: false),
                        VehicleId = c.Guid(nullable: false),
                        AgentFee = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Agents", t => t.AgentId)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId)
                .Index(t => t.AgentId)
                .Index(t => t.VehicleId);
            
            CreateTable(
                "dbo.AccountReceivables",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MedallionId = c.Guid(nullable: false),
                        AccountReceivableAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        PaymentTermMonth = c.Int(nullable: false),
                        Comments = c.String(maxLength: 400),
                        Status = c.String(maxLength: 20),
                        MonthlyPayment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MemberId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Medallions", t => t.MedallionId)
                .ForeignKey("dbo.Members", t => t.MemberId)
                .Index(t => t.MedallionId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.AutoLoanSetupAgents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LoanAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        InterestRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LoanTerm = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LoanTermBy = c.String(maxLength: 20),
                        Comments = c.String(maxLength: 400),
                        BankId = c.Guid(nullable: false),
                        Status = c.String(maxLength: 20),
                        CalculatedMonthlyPayment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPrincipalPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalInterestPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AgentVehicleId = c.Guid(nullable: false),
                        AgentId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Agents", t => t.AgentId)
                .ForeignKey("dbo.AgentVehicles", t => t.AgentVehicleId)
                .ForeignKey("dbo.Banks", t => t.BankId)
                .Index(t => t.AgentId)
                .Index(t => t.AgentVehicleId)
                .Index(t => t.BankId);
            
            CreateTable(
                "dbo.Banks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 200),
                        Address = c.String(maxLength: 200),
                        Address1 = c.String(maxLength: 200),
                        City = c.String(maxLength: 200),
                        State = c.String(maxLength: 10),
                        Zip = c.String(maxLength: 5),
                        Comment = c.String(maxLength: 400),
                        Phone1 = c.String(maxLength: 200),
                        Phone2 = c.String(maxLength: 200),
                        Fax = c.String(maxLength: 200),
                        Email = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MedallionId = c.Guid(nullable: false),
                        MemberId = c.Guid(nullable: false),
                        DateReceived = c.DateTime(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        NextPayment = c.DateTime(nullable: false),
                        UserName = c.String(),
                        TransactionType = c.String(maxLength: 1),
                        RecieptNumber = c.Int(nullable: false),
                        Interval = c.Int(nullable: false),
                        InsuranceSticker = c.Boolean(nullable: false),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AssociationDueAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CollsionInsuranceAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WorkerCompensationAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InsuranceSurchargeAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Loan = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WerkReceivableAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccountReceivableAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InsuranceDepositAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CCSystemAirtimeAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MiscCharge = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LateFees = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SavingDepositAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Credit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreditCardAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreditCardFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsAutoCashiering = c.Boolean(nullable: false),
                        Cash = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Check = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPaidAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalDueAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsZeroOut = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        StatysPastDue = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Medallions", t => t.MedallionId)
                .ForeignKey("dbo.Members", t => t.MemberId)
                .Index(t => t.MedallionId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.CCSystemAirtimes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MedallionId = c.Guid(nullable: false),
                        Airtime = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CCVendorId = c.Guid(nullable: false),
                        MemberId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CCVendors", t => t.CCVendorId)
                .ForeignKey("dbo.Medallions", t => t.MedallionId)
                .ForeignKey("dbo.Members", t => t.MemberId)
                .Index(t => t.CCVendorId)
                .Index(t => t.MedallionId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.CCVendors",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Address = c.String(),
                        Address1 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                        Comment = c.String(),
                        Phone1 = c.String(),
                        Phone2 = c.String(),
                        Fax = c.String(),
                        Email = c.String(),
                        Airtime = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ChargeBackTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 200),
                        TaxiRepresentativeName = c.String(maxLength: 200),
                        Address1 = c.String(maxLength: 400),
                        Address2 = c.String(maxLength: 400),
                        City = c.String(maxLength: 200),
                        State = c.String(maxLength: 40),
                        Zip = c.String(maxLength: 40),
                        SendRemittanceTo = c.String(maxLength: 200),
                        Phone1 = c.String(maxLength: 200),
                        Phone2 = c.String(maxLength: 200),
                        Fax = c.String(maxLength: 200),
                        Email = c.String(maxLength: 200),
                        URL = c.String(maxLength: 300),
                        Comment = c.String(maxLength: 400),
                        ReceiptMessage = c.String(maxLength: 400),
                        AgentName = c.String(maxLength: 200),
                        AgentAddress1 = c.String(maxLength: 200),
                        AgentAddress2 = c.String(maxLength: 200),
                        AgentCity = c.String(maxLength: 200),
                        AgentState = c.String(maxLength: 40),
                        AgentZip = c.String(maxLength: 40),
                        UseCompanyAddress = c.Boolean(nullable: false),
                        AgentPhone = c.String(),
                        AutoAssignAccountNumber = c.Boolean(nullable: false),
                        Key = c.Int(nullable: false),
                        StandardAssociationDue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StandardAgentFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DefaultLateBreak = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DefaultLateCharge = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DefaultWorkComp = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreditCardFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentType = c.String(),
                        SummaryReceipt = c.Boolean(nullable: false),
                        TwoPartReceipt = c.Boolean(nullable: false),
                        AutomaticCashiering = c.Boolean(nullable: false),
                        SeachByAccountNumber = c.Boolean(nullable: false),
                        SearchByMedallionNumber = c.Boolean(nullable: false),
                        SearchByAgentChaufferNumber = c.Boolean(nullable: false),
                        SearchBySSN = c.Boolean(nullable: false),
                        SearchByMemberName = c.Boolean(nullable: false),
                        SearchByMemberContactName = c.Boolean(nullable: false),
                        SearchByAgentLastName = c.Boolean(nullable: false),
                        Invoice_Key = c.String(maxLength: 200),
                        ActualRateSet = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ActualCollisionRateSet = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InterestRate = c.Int(nullable: false),
                        BankId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Banks", t => t.BankId)
                .Index(t => t.BankId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MemberId = c.Guid(nullable: false),
                        FirstName = c.String(maxLength: 200),
                        LastName = c.String(maxLength: 200),
                        Address = c.String(maxLength: 200),
                        Address1 = c.String(maxLength: 200),
                        City = c.String(maxLength: 200),
                        State = c.String(maxLength: 10),
                        Zip = c.String(maxLength: 5),
                        AgentAssigned = c.Boolean(nullable: false),
                        Title = c.String(maxLength: 200),
                        DateOfBirth = c.DateTime(nullable: false),
                        Comments = c.String(),
                        MobilePhone = c.String(maxLength: 200),
                        HomePhone = c.String(maxLength: 200),
                        SSN = c.String(maxLength: 200),
                        ChaufferLic = c.String(maxLength: 200),
                        DriveLic = c.String(maxLength: 200),
                        ContinueEducation = c.String(maxLength: 200),
                        IsStockholder = c.Boolean(nullable: false),
                        UseThisAddresForMemberBilling = c.Boolean(nullable: false),
                        UseThisContactAsRegisteredAgent = c.Boolean(nullable: false),
                        PictureContact = c.String(maxLength: 400),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.MemberId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.CorporateClients",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Address = c.String(),
                        Address1 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                        Comment = c.String(),
                        Phone1 = c.String(),
                        Phone2 = c.String(),
                        Fax = c.String(),
                        Email = c.String(),
                        Default = c.Boolean(nullable: false),
                        ProcessFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CompanyFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FAQs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Question = c.String(),
                        Answer = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InsuranceDepositAgents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DepositAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartDate = c.DateTime(nullable: false),
                        WeeklyPayment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comments = c.String(maxLength: 400),
                        Status = c.String(maxLength: 20),
                        AgentVehicleId = c.Guid(nullable: false),
                        AgentId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Agents", t => t.AgentId)
                .ForeignKey("dbo.AgentVehicles", t => t.AgentVehicleId)
                .Index(t => t.AgentId)
                .Index(t => t.AgentVehicleId);
            
            CreateTable(
                "dbo.InsuranceDeposits",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MedallionId = c.Guid(nullable: false),
                        DepositAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartDate = c.DateTime(nullable: false),
                        WeeklyPayment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comments = c.String(maxLength: 400),
                        Status = c.String(maxLength: 20),
                        MemberId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Medallions", t => t.MedallionId)
                .ForeignKey("dbo.Members", t => t.MemberId)
                .Index(t => t.MedallionId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.Loans",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MedallionId = c.Guid(nullable: false),
                        LoanAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        InterestRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LoanTerm = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LoanTermBy = c.String(maxLength: 20),
                        Comments = c.String(maxLength: 400),
                        BankId = c.Guid(nullable: false),
                        Status = c.String(maxLength: 20),
                        CalculatedMonthlyPayment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPrincipalPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalInterestPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MemberId = c.Guid(nullable: false),
                        LoanName = c.String(maxLength: 200),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Banks", t => t.BankId)
                .ForeignKey("dbo.Medallions", t => t.MedallionId)
                .ForeignKey("dbo.Members", t => t.MemberId)
                .Index(t => t.BankId)
                .Index(t => t.MedallionId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.Mobilities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DateRun = c.DateTime(),
                        ClientName = c.String(maxLength: 200),
                        LicenseNumber = c.String(maxLength: 200),
                        MD = c.String(maxLength: 200),
                        PickupAddress = c.String(maxLength: 400),
                        DueTime = c.String(maxLength: 200),
                        PUTime = c.String(maxLength: 200),
                        DOTime = c.String(maxLength: 200),
                        Early = c.String(maxLength: 200),
                        Late = c.String(maxLength: 200),
                        NS = c.String(maxLength: 200),
                        Hold = c.String(maxLength: 200),
                        DropOffAddress = c.String(maxLength: 400),
                        Phone = c.String(maxLength: 200),
                        CAB = c.String(maxLength: 200),
                        CL = c.String(maxLength: 200),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ModelYearInsurances",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ModelYear = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        To = c.String(),
                        CreateAt = c.DateTime(nullable: false),
                        CreateBy = c.String(),
                        Read = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Predefined = c.String(),
                        AccountCanEdit = c.Boolean(),
                        AccountCanView = c.Boolean(),
                        AgentCanCreate = c.Boolean(),
                        MedallionCanDelete = c.Boolean(),
                        ViewMemberData = c.Boolean(),
                        EditMemberData = c.Boolean(),
                        DeleteMemberData = c.Boolean(),
                        ViewMemberCashiering = c.Boolean(),
                        BillOutMember = c.Boolean(),
                        ViewDeletedMembers = c.Boolean(),
                        ReportCanMakeReport = c.Boolean(),
                        ReportCanMakeEndOfDayTrans = c.Boolean(),
                        RoleCanViewList = c.Boolean(),
                        RoleCanEdit = c.Boolean(),
                        RoleCanCreate = c.Boolean(),
                        RoleCanDelete = c.Boolean(),
                        UserCanViewList = c.Boolean(),
                        UserCanEdit = c.Boolean(),
                        UserCanCreate = c.Boolean(),
                        UserCanDelete = c.Boolean(),
                        ReferenceCanEdit = c.Boolean(),
                        AllowDashboard = c.Boolean(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        SecretQuestion = c.String(),
                        SecretAnswer = c.String(),
                        Email = c.String(),
                        Phone1 = c.String(),
                        Phone2 = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                        Fax = c.String(),
                        CreateDate = c.DateTime(),
                        Active = c.Boolean(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.RTAs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DateRun = c.DateTime(),
                        ClientName = c.String(maxLength: 200),
                        LicenseNumber = c.String(maxLength: 200),
                        PickupAddress = c.String(maxLength: 400),
                        DueTime = c.String(maxLength: 200),
                        PUTime = c.String(maxLength: 200),
                        DOTime = c.String(maxLength: 200),
                        DropOffAddress = c.String(maxLength: 400),
                        Phone = c.String(maxLength: 200),
                        CAB = c.String(maxLength: 200),
                        CL = c.String(maxLength: 200),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaidDate = c.DateTime(),
                        Confirmed = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SavingDepositAgents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Comments = c.String(maxLength: 400),
                        TotalPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AgentVehicleId = c.Guid(nullable: false),
                        AgentId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Agents", t => t.AgentId)
                .ForeignKey("dbo.AgentVehicles", t => t.AgentVehicleId)
                .Index(t => t.AgentId)
                .Index(t => t.AgentVehicleId);
            
            CreateTable(
                "dbo.SavingDeposits",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MedallionId = c.Guid(nullable: false),
                        Comments = c.String(maxLength: 400),
                        TotalPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MemberId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Medallions", t => t.MedallionId)
                .ForeignKey("dbo.Members", t => t.MemberId)
                .Index(t => t.MedallionId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.Stockholders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MemberId = c.Guid(nullable: false),
                        StockholderName = c.String(maxLength: 200),
                        Percentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.MemberId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.TicketItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SupportTicketId = c.Guid(nullable: false),
                        Message = c.String(),
                        CreateBy = c.String(),
                        CreateAt = c.DateTime(nullable: false),
                        Read = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tickets", t => t.SupportTicketId)
                .Index(t => t.SupportTicketId);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        CreateBy = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TransactionHistories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DateReceived = c.DateTime(nullable: false),
                        TransactionType = c.String(maxLength: 1),
                        UserName = c.String(),
                        TransactionDate = c.DateTime(),
                        DueDate = c.DateTime(),
                        MedallionNumber = c.String(),
                        MemberId = c.Guid(nullable: false),
                        MedallionId = c.Guid(nullable: false),
                        NextPayment = c.DateTime(nullable: false),
                        RecieptNumber = c.Int(nullable: false),
                        Interval = c.Int(nullable: false),
                        InsuranceSticker = c.Boolean(nullable: false),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AssociationDueAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CollsionInsuranceAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WorkerCompensationAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InsuranceSurchargeAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Loan = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WerkReceivableAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccountReceivableAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InsuranceDepositAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CCSystemAirtimeAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MiscCharge = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LateFees = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SavingDepositAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Credit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreditCardAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreditCardFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsAutoCashiering = c.Boolean(nullable: false),
                        Cash = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Check = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPaidAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalDueAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDeleted = c.Boolean(nullable: false),
                        IsZeroOut = c.Boolean(nullable: false),
                        BillId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bills", t => t.BillId)
                .ForeignKey("dbo.Medallions", t => t.MedallionId)
                .ForeignKey("dbo.Members", t => t.MemberId)
                .Index(t => t.BillId)
                .Index(t => t.MedallionId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.TransactionHistoryKeepTracks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DateReceived = c.DateTime(nullable: false),
                        RecieptNumber = c.Int(nullable: false),
                        TransactionType = c.String(maxLength: 1),
                        UserName = c.String(),
                        TransactionDate = c.DateTime(),
                        DueDate = c.DateTime(),
                        MemberId = c.Guid(nullable: false),
                        MedallionId = c.Guid(nullable: false),
                        InsuranceSticker = c.Boolean(nullable: false),
                        Interval = c.Int(nullable: false),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AssociationDueAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CollsionInsuranceAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WorkerCompensationAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InsuranceSurchargeAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MedallionLoanAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AutoLoanAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WerkReceivableAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccountReceivableAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InsuranceDepositAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CCSystemAirtimeAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MiscCharge = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LateFees = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SavingDepositAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Credit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreditCardAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreditCardFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsAutoCashiering = c.Boolean(nullable: false),
                        Cash = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Check = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPaidAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalDueAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TransactionHistoryId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Medallions", t => t.MedallionId)
                .ForeignKey("dbo.Members", t => t.MemberId)
                .ForeignKey("dbo.TransactionHistories", t => t.TransactionHistoryId)
                .Index(t => t.MedallionId)
                .Index(t => t.MemberId)
                .Index(t => t.TransactionHistoryId);
            
            CreateTable(
                "dbo.Tutorials",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Content = c.String(),
                        CreateBy = c.String(),
                        CreateAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UniversalAgentRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LicenseNumber = c.String(),
                        IsRenewed = c.Boolean(nullable: false),
                        Status = c.String(),
                        StatusDate = c.DateTime(),
                        DriverType = c.String(),
                        LicenseType = c.String(),
                        OriginalIssueDate = c.DateTime(),
                        Name = c.String(),
                        Sex = c.String(),
                        ChaufferCity = c.String(),
                        ChaufferState = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WerkShops",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Address = c.String(),
                        Address1 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                        Comment = c.String(),
                        Phone1 = c.String(),
                        Phone2 = c.String(),
                        Fax = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionHistoryKeepTracks", "TransactionHistoryId", "dbo.TransactionHistories");
            DropForeignKey("dbo.TransactionHistoryKeepTracks", "MemberId", "dbo.Members");
            DropForeignKey("dbo.TransactionHistoryKeepTracks", "MedallionId", "dbo.Medallions");
            DropForeignKey("dbo.TransactionHistories", "MemberId", "dbo.Members");
            DropForeignKey("dbo.TransactionHistories", "MedallionId", "dbo.Medallions");
            DropForeignKey("dbo.TransactionHistories", "BillId", "dbo.Bills");
            DropForeignKey("dbo.TicketItems", "SupportTicketId", "dbo.Tickets");
            DropForeignKey("dbo.Stockholders", "MemberId", "dbo.Members");
            DropForeignKey("dbo.SavingDeposits", "MemberId", "dbo.Members");
            DropForeignKey("dbo.SavingDeposits", "MedallionId", "dbo.Medallions");
            DropForeignKey("dbo.SavingDepositAgents", "AgentVehicleId", "dbo.AgentVehicles");
            DropForeignKey("dbo.SavingDepositAgents", "AgentId", "dbo.Agents");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Loans", "MemberId", "dbo.Members");
            DropForeignKey("dbo.Loans", "MedallionId", "dbo.Medallions");
            DropForeignKey("dbo.Loans", "BankId", "dbo.Banks");
            DropForeignKey("dbo.InsuranceDeposits", "MemberId", "dbo.Members");
            DropForeignKey("dbo.InsuranceDeposits", "MedallionId", "dbo.Medallions");
            DropForeignKey("dbo.InsuranceDepositAgents", "AgentVehicleId", "dbo.AgentVehicles");
            DropForeignKey("dbo.InsuranceDepositAgents", "AgentId", "dbo.Agents");
            DropForeignKey("dbo.Contacts", "MemberId", "dbo.Members");
            DropForeignKey("dbo.Companies", "BankId", "dbo.Banks");
            DropForeignKey("dbo.CCSystemAirtimes", "MemberId", "dbo.Members");
            DropForeignKey("dbo.CCSystemAirtimes", "MedallionId", "dbo.Medallions");
            DropForeignKey("dbo.CCSystemAirtimes", "CCVendorId", "dbo.CCVendors");
            DropForeignKey("dbo.Bills", "MemberId", "dbo.Members");
            DropForeignKey("dbo.Bills", "MedallionId", "dbo.Medallions");
            DropForeignKey("dbo.AutoLoanSetupAgents", "BankId", "dbo.Banks");
            DropForeignKey("dbo.AutoLoanSetupAgents", "AgentVehicleId", "dbo.AgentVehicles");
            DropForeignKey("dbo.AutoLoanSetupAgents", "AgentId", "dbo.Agents");
            DropForeignKey("dbo.AccountReceivables", "MemberId", "dbo.Members");
            DropForeignKey("dbo.AccountReceivables", "MedallionId", "dbo.Medallions");
            DropForeignKey("dbo.AccountReceivableAgents", "AgentVehicleId", "dbo.AgentVehicles");
            DropForeignKey("dbo.AgentVehicles", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.AgentVehicles", "AgentId", "dbo.Agents");
            DropForeignKey("dbo.AccountReceivableAgents", "AgentId", "dbo.Agents");
            DropForeignKey("dbo.Agents", "MemberId", "dbo.Members");
            DropForeignKey("dbo.Vehicles", "VehicleModelId", "dbo.VehicleModels");
            DropForeignKey("dbo.VehicleModels", "VehicleMakeId", "dbo.VehicleMakes");
            DropForeignKey("dbo.Vehicles", "VehicleMakeId", "dbo.VehicleMakes");
            DropForeignKey("dbo.Vehicles", "VehicleFeatureId", "dbo.VehicleFeatures");
            DropForeignKey("dbo.Vehicles", "MeterManufacturerId", "dbo.MeterManufacturers");
            DropForeignKey("dbo.Vehicles", "MemberId", "dbo.Members");
            DropForeignKey("dbo.Vehicles", "MedallionId", "dbo.Medallions");
            DropForeignKey("dbo.Medallions", "MemberId", "dbo.Members");
            DropForeignKey("dbo.StandardDues", "MemberId", "dbo.Members");
            DropIndex("dbo.TransactionHistoryKeepTracks", new[] { "TransactionHistoryId" });
            DropIndex("dbo.TransactionHistoryKeepTracks", new[] { "MemberId" });
            DropIndex("dbo.TransactionHistoryKeepTracks", new[] { "MedallionId" });
            DropIndex("dbo.TransactionHistories", new[] { "MemberId" });
            DropIndex("dbo.TransactionHistories", new[] { "MedallionId" });
            DropIndex("dbo.TransactionHistories", new[] { "BillId" });
            DropIndex("dbo.TicketItems", new[] { "SupportTicketId" });
            DropIndex("dbo.Stockholders", new[] { "MemberId" });
            DropIndex("dbo.SavingDeposits", new[] { "MemberId" });
            DropIndex("dbo.SavingDeposits", new[] { "MedallionId" });
            DropIndex("dbo.SavingDepositAgents", new[] { "AgentVehicleId" });
            DropIndex("dbo.SavingDepositAgents", new[] { "AgentId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "User_Id" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.Loans", new[] { "MemberId" });
            DropIndex("dbo.Loans", new[] { "MedallionId" });
            DropIndex("dbo.Loans", new[] { "BankId" });
            DropIndex("dbo.InsuranceDeposits", new[] { "MemberId" });
            DropIndex("dbo.InsuranceDeposits", new[] { "MedallionId" });
            DropIndex("dbo.InsuranceDepositAgents", new[] { "AgentVehicleId" });
            DropIndex("dbo.InsuranceDepositAgents", new[] { "AgentId" });
            DropIndex("dbo.Contacts", new[] { "MemberId" });
            DropIndex("dbo.Companies", new[] { "BankId" });
            DropIndex("dbo.CCSystemAirtimes", new[] { "MemberId" });
            DropIndex("dbo.CCSystemAirtimes", new[] { "MedallionId" });
            DropIndex("dbo.CCSystemAirtimes", new[] { "CCVendorId" });
            DropIndex("dbo.Bills", new[] { "MemberId" });
            DropIndex("dbo.Bills", new[] { "MedallionId" });
            DropIndex("dbo.AutoLoanSetupAgents", new[] { "BankId" });
            DropIndex("dbo.AutoLoanSetupAgents", new[] { "AgentVehicleId" });
            DropIndex("dbo.AutoLoanSetupAgents", new[] { "AgentId" });
            DropIndex("dbo.AccountReceivables", new[] { "MemberId" });
            DropIndex("dbo.AccountReceivables", new[] { "MedallionId" });
            DropIndex("dbo.AccountReceivableAgents", new[] { "AgentVehicleId" });
            DropIndex("dbo.AgentVehicles", new[] { "VehicleId" });
            DropIndex("dbo.AgentVehicles", new[] { "AgentId" });
            DropIndex("dbo.AccountReceivableAgents", new[] { "AgentId" });
            DropIndex("dbo.Agents", new[] { "MemberId" });
            DropIndex("dbo.Vehicles", new[] { "VehicleModelId" });
            DropIndex("dbo.VehicleModels", new[] { "VehicleMakeId" });
            DropIndex("dbo.Vehicles", new[] { "VehicleMakeId" });
            DropIndex("dbo.Vehicles", new[] { "VehicleFeatureId" });
            DropIndex("dbo.Vehicles", new[] { "MeterManufacturerId" });
            DropIndex("dbo.Vehicles", new[] { "MemberId" });
            DropIndex("dbo.Vehicles", new[] { "MedallionId" });
            DropIndex("dbo.Medallions", new[] { "MemberId" });
            DropIndex("dbo.StandardDues", new[] { "MemberId" });
            DropTable("dbo.WerkShops");
            DropTable("dbo.UniversalAgentRecords");
            DropTable("dbo.Tutorials");
            DropTable("dbo.TransactionHistoryKeepTracks");
            DropTable("dbo.TransactionHistories");
            DropTable("dbo.Tickets");
            DropTable("dbo.TicketItems");
            DropTable("dbo.Stockholders");
            DropTable("dbo.SavingDeposits");
            DropTable("dbo.SavingDepositAgents");
            DropTable("dbo.States");
            DropTable("dbo.RTAs");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.Notifications");
            DropTable("dbo.ModelYearInsurances");
            DropTable("dbo.Mobilities");
            DropTable("dbo.Loans");
            DropTable("dbo.InsuranceDeposits");
            DropTable("dbo.InsuranceDepositAgents");
            DropTable("dbo.FAQs");
            DropTable("dbo.CorporateClients");
            DropTable("dbo.Contacts");
            DropTable("dbo.Companies");
            DropTable("dbo.ChargeBackTypes");
            DropTable("dbo.CCVendors");
            DropTable("dbo.CCSystemAirtimes");
            DropTable("dbo.Bills");
            DropTable("dbo.Banks");
            DropTable("dbo.AutoLoanSetupAgents");
            DropTable("dbo.AccountReceivables");
            DropTable("dbo.AgentVehicles");
            DropTable("dbo.VehicleModels");
            DropTable("dbo.VehicleMakes");
            DropTable("dbo.VehicleFeatures");
            DropTable("dbo.MeterManufacturers");
            DropTable("dbo.Medallions");
            DropTable("dbo.Vehicles");
            DropTable("dbo.StandardDues");
            DropTable("dbo.Members");
            DropTable("dbo.Agents");
            DropTable("dbo.AccountReceivableAgents");
        }
    }
}
