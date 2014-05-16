using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDevs.Domain.Models;

namespace TDevs.Domain.ViewModel
{
    public class UserListViewModel
    {

    }

    public class MemberMedallionVewModel:IEquatable<MemberMedallionVewModel>
    {
        public Guid MedallionId { get; set; }
        public string VehicleAssigned { get { return string.Format("{0}/{1}/{2}/{3}", LicenseNumber, Make, Model, Year); } }
        public string MedallionNumber { get; set; }
        public DateTime DateJoined { get; set; }
        public DateTime BillingStartDate { get; set; }
        public DateTime BillingEndDate { get; set; }
        public bool UnderServed { get; set; }
        public decimal InsuranceSurcharge { get; set; }
        public decimal Collision { get; set; }
        public decimal Balance { get; set; }
        public Guid MemberId { get; set; }
        public string LicenseNumber { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }


        public bool Equals(MemberMedallionVewModel other)
        {
            if (MedallionId == other.MedallionId)
                return true;

            return false;
        }
    }

    public class MemberVehicleViewModel
    {
        public string VehicleAssigned { get { return string.Format("{0}/{1}/{2}/{3}", LicenseNumber, Make, Model, Year); } }
        public string LicenseNumber { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
    }

    public class MedallionLoan
    {
        public Guid MedallionLoanId { get; set; }
        public Guid MedallionId { get; set; }
        public string MedallionNumber { get; set; }         
        public decimal LoanAmount { get; set; }
        
        public decimal InterestRate { get; set; }
        public decimal LoanTerm { get; set; }        
        public string LoanTermBy { get; set; }
        public decimal CurrentBalance { get; set; }
        public string Comments { get; set; }
        public Guid BankId { get; set; }
        public string BankName { get; set; }
        public string Status { get; set; }
        public int DateLates { get; set; }
        public DateTime? PayTillDate { get; set; }
        public DateTime? PaymentDueDate { get; set; }
    }

    public class BillMedallionViewModel {
        public Bill Bill { get; set; }
        public Medallion Medallion { get; set; }
        public Member Member { get; set; }
    }

    public class TwoPart
    {
        private List<AllLoan> loans = new List<AllLoan>();

        public List<AllLoan> Loans
        {
            get { return loans; }
            set { loans = value; }
        }

        private List<AllDeposit> deposits = new List<AllDeposit>();

        public List<AllDeposit> Deposits
        {
            get { return deposits; }
            set { deposits = value; }
        }
        private List<AllReceivable> receivable = new List<AllReceivable>();

        public List<AllReceivable> Receivable
        {
            get { return receivable; }
            set { receivable = value; }
        }
    }

    public class AllLoan
    {
        public decimal Loan { get; set; }
        public decimal Interest { get; set; }
        public decimal InterestedPaid { get; set; }
        public decimal PrincipalPaid { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal Balance { get; set; }
        public string Title { get; set; }
    }

    public class AllDeposit
    {
        public decimal Deposit { get; set; }
        public decimal WeeklyPayment { get; set; }        
        public decimal TotalPaid { get; set; }
        public decimal Balance { get; set; }
        public string Title { get; set; }
    }

    public class AllReceivable
    {
        public decimal Amount { get; set; }
        public decimal MonthlyPayment { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal Balance { get; set; }
        public string Title { get; set; }
    }

    public class AgentCriterial
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Medallion { get; set; }
        public string ChaufferLic { get; set; }
    }

    public class SearchAgentResult {
        public Guid  Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid MedallionId { get; set; }
        public string MedallionNumber { get; set; }
        public string ChaufferLic { get; set;}
        public string AccountNumber { get; set; }
        public Guid MemberId { get; set; }
        public Member Member { get; set;}
    }

    public class PaymentHistoryReport
    {
        public string MemberStatus { get; set; }
        public Guid MemberId { get; set; }
        public Guid MedallionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndTime { get; set; }

        public List<EnumModelView> FeeTypes { get; set; }
        public List<EnumModelView> PaymentTypes { get; set; }
    }
}
