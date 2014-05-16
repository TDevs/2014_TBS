using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDevs.Domain
{
    public class Mobility
    {
        [Key]
        public Guid Id { get; set; }
       
        public DateTime? DateRun { get; set; }
        [MaxLength(200)]
        public string ClientName { get; set; }
        [MaxLength(200)]
        public string LicenseNumber { get; set; }
        [MaxLength(200)]
        public string MD { get; set; }
        [MaxLength(400)]
        public string PickupAddress { get; set; }
        [MaxLength(200)]
        public string DueTime { get; set; }
        [MaxLength(200)]
        public string PUTime { get; set; }
        [MaxLength(200)]
        public string DOTime { get; set; }
        [MaxLength(200)]
        public string Early { get; set; }
        [MaxLength(200)]
        public string Late { get; set; }
        [MaxLength(200)]
        public string NS { get; set; }
        [MaxLength(200)]
        public string Hold { get; set; }
        [MaxLength(400)]
        public string DropOffAddress { get; set; }
        [MaxLength(200)]
        public string Phone { get; set; }
        [MaxLength(200)]
        public string CAB { get; set; }
        [MaxLength(200)]
        public string CL { get; set; }        
        public decimal Amount { get; set; }
      
    }
}
