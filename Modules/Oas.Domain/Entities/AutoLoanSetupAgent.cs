using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDevs.Domain
{
    public class AutoLoanSetupAgent
    {
        [Key]
        public Guid Id { get; set; }
       
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
        public Guid AgentVehicleId { get; set; }
        [ForeignKey("AgentVehicleId")]
        public AgentVehicle AgentVehicle { get; set; }

        [ForeignKey("BankId")]
        public Bank Bank { get; set; }
        public Guid AgentId { get; set; }
        [ForeignKey("AgentId")]
        public Agent Agent { get; set; }
    }
}
