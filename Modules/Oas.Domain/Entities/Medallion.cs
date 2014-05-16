using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDevs.Domain
{
    public class Medallion
    {
        [Key]
        public Guid Id { get; set; }

        public Guid MemberId { get; set; }
        
        [MaxLength(200)]
        public string MedallionNumber { get; set; }
        public DateTime DateJoined { get; set; }
        public DateTime BillingStartDate { get; set; }
        public DateTime BillingEndDate { get; set; }
        public bool UnderServed { get; set; }
        public decimal InsuranceSurcharge { get; set; }
        public decimal Collision { get; set; }
        public decimal Balance { get; set; }

        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }
    }
}
