using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDevs.Domain
{
    public class MedallionLoanSetupAgent
    {
        [Key]
        public Guid Id { get; set; }
        public int Cab { get; set; }
        public decimal LoanAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal InterestRate { get; set; }
        public decimal LoanTerm { get; set; }
        [MaxLength(20)]
        public string LoanTermBy { get; set; }
        [MaxLength(400)]
        public string Comments { get; set; }
        public Guid BankId { get; set; }
        [MaxLength(20)]
        public string Status { get; set; }

        public decimal CalculatedMonthlyPayment { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal TotalPrincipalPaid { get; set; }
        public decimal TotalInterestPaid { get; set; }
      
        [ForeignKey("BankId")]
        public Bank Bank { get; set; }
        public Guid? MemberId { get; set; }
        [ForeignKey("MemberId")]
        public Member Member { get; set; }


    }
}
