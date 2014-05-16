using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDevs.Domain
{
    public class AccountReceivable
    {
        [Key]
        public Guid Id { get; set; }

        public Guid MedallionId { get; set; }
        public decimal AccountReceivableAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int  PaymentTermMonth { get; set; }       
        [MaxLength(400)]
        public string Comments { get; set; }
        
        [MaxLength(20)]
        public string Status { get; set; }

        public decimal MonthlyPayment { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal TotalPaid { get; set; }      
       


        public Guid MemberId { get; set; }
        [ForeignKey("MemberId")]
        public Member Member { get; set; }
        [ForeignKey("MedallionId")]
        public Medallion Medallion { get; set; }
        
    }
}
