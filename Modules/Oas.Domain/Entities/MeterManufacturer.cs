using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDevs.Domain
{
    public class MeterManufacturer
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }
}
