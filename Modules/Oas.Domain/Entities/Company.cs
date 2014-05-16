using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDevs.Domain
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; }
        #region Company Info
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string TaxiRepresentativeName { get; set; }
        [MaxLength(400)]
        public string Address1 { get; set; }
        [MaxLength(400)]
        public string Address2 { get; set; }
        [MaxLength(200)]
        public string City { get; set; }
        [MaxLength(40)]
        public string State { get; set; }
        [MaxLength(40)]
        public string Zip { get; set; }
        [MaxLength(200)]
        public string SendRemittanceTo { get; set; }
        [MaxLength(200)]
        public string Phone1 { get; set; }
        [MaxLength(200)]
        public string Phone2 { get; set; }
        [MaxLength(200)]
        public string Fax { get; set; }
        [MaxLength(200)]
        public string Email { get; set; }
        [MaxLength(300)]
        public string URL { get; set; }
        [MaxLength(400)]
        public string Comment { get; set; }
        [MaxLength(400)]
        public string ReceiptMessage { get; set; }
        #endregion

        #region Agent Register
        [MaxLength(200)]
        public string AgentName { get; set; }
        [MaxLength(200)]
        public string AgentAddress1 { get; set; }
        [MaxLength(200)]
        public string AgentAddress2 { get; set; }
        [MaxLength(200)]
        public string AgentCity { get; set; }
        [MaxLength(40)]
        public string AgentState { get; set; }
        [MaxLength(40)]
        public string AgentZip { get; set; }

        public bool UseCompanyAddress { get; set; }
        public string AgentPhone { get; set; }
        #endregion

        #region Account Setting
        public bool AutoAssignAccountNumber { get; set; }
        public int Key { get; set; }
        public decimal StandardAssociationDue { get; set; }
        public decimal StandardAgentFee { get; set; }
        public decimal DefaultLateBreak { get; set; }
        public decimal DefaultLateCharge { get; set; }
        public decimal DefaultWorkComp { get; set; }
        public decimal CreditCardFee { get; set; }
        public string PaymentType { get; set; }

        #endregion

        #region Payment Receipt Setting
        public bool SummaryReceipt { get; set; }
        public bool TwoPartReceipt { get; set; }
        public bool AutomaticCashiering { get; set; }

        #endregion

        #region Member search Setting
        public bool SeachByAccountNumber { get; set; }
        public bool SearchByMedallionNumber { get; set; }
        public bool SearchByAgentChaufferNumber { get; set; }
        public bool SearchBySSN { get; set; }
        public bool SearchByMemberName { get; set; }
        public bool SearchByMemberContactName { get; set; }
        public bool SearchByAgentLastName { get; set; }
        #endregion

        #region Voucher Invoice
        [MaxLength(200)]
        public string Invoice_Key { get; set; }
        #endregion

        #region Actual Rate Set and Actual Collision Rate Set
        public decimal ActualRateSet { get; set; }
        public decimal ActualCollisionRateSet { get; set; }
        public int InterestRate { get; set; }
        #endregion

        #region Bank
        public Guid? BankId { get; set; }
        [ForeignKey("BankId")]
        public Bank Bank { get; set; }
        #endregion
    }
}
