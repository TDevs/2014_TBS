using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDevs.Domain
{
    public class SavingDeposit
    {
        [Key]
        public Guid Id { get; set; }

        public Guid MedallionId { get; set; }      
        [MaxLength(400)]
        public string Comments { get; set; }
        public decimal TotalPaid { get; set; }
        [ForeignKey("MedallionId")]
        public Medallion Medallion { get; set; }
        public Guid MemberId { get; set; }
        [ForeignKey("MemberId")]
        public Member Member { get; set; }
    }
}
