using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TDevs.Core.Constants
{
    public class GlobalConstants
    {
        public const string Config_ConnectionKey = "DatacontextConnection";
        public const string AppConfig_Pagging = "PageCount";
        public static string DateTimeFormat = "dd/Mm/yyyy";

    }

    public struct UserRoles
    {
        public const string Administrator = "Administrator";
        public const string Dispatcher = "Dispatcher";
        public const string Cashier = "Cashier";
        public const string Employee = "Employee"; 

    }

    public struct MemberStatus
    {
        public const string Active = "Active";
        public const string Deleted = " Deleted	";
        public const string All = "All";

    }


    public enum FeeTypes
    {
        [Description("Balance")]
        Balance,
        [Description("Association Dues")]
        AssociationDues,

        [Description("Collision Insurance")]
        CollisionInsurance,

        [Description("Workers Compensation")]
        WorkersCompensation,

        [Description("Insurance Surcharge")]
        InsuranceSurcharge,

        [Description("Medallion Loan")]
        MedallionLoan,

        [Description("Medallion Loan (addtional)")]
        MedallionLoanPlus,

        [Description("Auto Loan")]
        AutoLoan,

        [Description("Auto Loan (addtional)")]
        AutoLoanPlus,

        [Description("Werks Receivable")]
        WerksReceivable,

        [Description("Accounts Receivable")]
        AccountsReceivable,

        [Description("Radio Deposit")]
        RadioDeposit,

        [Description("Deposit")]
        Deposit,

        [Description("CC System")]
        CCSystem,

        [Description("CC System Airtime")]
        CCSystemAirtime,

        [Description("Misc Charge")]
        MiscCharge,

        [Description("Late Fees")]
        LateFees,

    }

    public enum PaymentTypes
    {
        [Description("Credit")]
        Credit,
        [Description("Credit Cards")]
        CreditCards,

        [Description("Credit Cards Fee")]
        CreditCardsFee,

        [Description("Cash")]
        Cash,

        [Description("Checks")]
        Checks,

        [Description("Coupons")]
        Coupons,

        [Description("Corporate Voucher (including fee)")]
        CorpporateVoucher,

        [Description("Saving Deposit")]
        SavingDeposit
    }

}
