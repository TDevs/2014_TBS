using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDevs.Domain
{
    public class SavingDepositAgent
    {
        [Key]
        public Guid Id { get; set; }

         
        [MaxLength(400)]
        public string Comments { get; set; }
        public decimal TotalPaid { get; set; }
        public Guid AgentVehicleId { get; set; }
        [ForeignKey("AgentVehicleId")]
        public AgentVehicle AgentVehicle { get; set; }
        public Guid AgentId { get; set; }
        [ForeignKey("AgentId")]
        public Agent Agent { get; set; }
    }
}
