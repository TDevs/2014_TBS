using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDevs.Domain
{
    public class InsuranceDepositAgent
    {
        [Key]
        public Guid Id { get; set; }        
         
        public decimal DepositAmount { get; set; }
        public DateTime StartDate { get; set; }
        public decimal WeeklyPayment { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal CurrentBalance { get; set; }
        [MaxLength(400)]
        public string Comments { get; set; }
        [MaxLength(20)]
        public string Status { get; set; }
        public Guid AgentVehicleId { get; set; }
        [ForeignKey("AgentVehicleId")]
        public AgentVehicle AgentVehicle { get; set; }

        public Guid AgentId { get; set; }
        [ForeignKey("AgentId")]
        public Agent Agent { get; set; }
    }
}
