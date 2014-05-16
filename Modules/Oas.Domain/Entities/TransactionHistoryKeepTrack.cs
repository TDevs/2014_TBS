using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDevs.Domain
{
    public class TransactionHistoryKeepTrack
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DateReceived { get; set; }
        public int RecieptNumber { get; set; }
        [MaxLength(1)]
        public string TransactionType { get; set; }
        public string UserName { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid MemberId { get; set; }
        public Guid MedallionId { get; set; }
        public bool InsuranceSticker { get; set; }
        public int Interval { get; set; }
        public decimal Balance { get; set; }
        public decimal AssociationDueAmount { get; set; }
        public decimal CollsionInsuranceAmount { get; set; }
        public decimal WorkerCompensationAmount { get; set; }
        public decimal InsuranceSurchargeAmount { get; set; }
        public decimal MedallionLoanAmount { get; set; }
        public decimal AutoLoanAmount { get; set; }
        public decimal WerkReceivableAmount { get; set; }
        public decimal AccountReceivableAmount { get; set; }
        public decimal InsuranceDepositAmount { get; set; }
        public decimal CCSystemAirtimeAmount { get; set; }

        public decimal MiscCharge { get; set; }
        public decimal LateFees { get; set; }
        public decimal SavingDepositAmount { get; set; }
        public decimal Subtotal { get; set; }

        public decimal Credit { get; set; }
        public decimal CreditCardAmount { get; set; }
        public decimal CreditCardFee { get; set; }
        public bool IsAutoCashiering { get; set; }
        public decimal Cash { get; set; }
        public decimal Check { get; set; }
        public decimal TotalPaidAmount { get; set; }
        public decimal TotalDueAmount { get; set; }
        public decimal TotalPaid { get; set; }
        public Guid TransactionHistoryId { get; set; }
        [ForeignKey("MemberId")]
        public Member Member { get; set; }
        [ForeignKey("MedallionId")]
        public Medallion Medallion { get; set; }
        [ForeignKey("TransactionHistoryId")]
        public TransactionHistory TransactionHistory { get; set; } 

    }
}
