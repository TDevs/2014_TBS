using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDevs.Domain
{
   public class AgentVehicle
    {
       [Key]
       public Guid Id { get; set; }
       public Guid AgentId { get; set; }
       public Guid VehicleId { get; set; }
       public bool AgentFee { get; set; }

       public Agent Agent { get; set; }
       public Vehicle Vehicle { get; set; }
    }
}
