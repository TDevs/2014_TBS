using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDevs.Domain
{
    public class Stockholder
    {
        [Key]
        public Guid Id { get; set; }

        public Guid MemberId { get; set; }
        [MaxLength(200)]
        public string StockholderName { get; set; }
        public Member Member { get; set; }
        public decimal Percentage { get; set; }
    }
}
