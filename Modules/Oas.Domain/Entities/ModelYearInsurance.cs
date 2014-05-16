using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDevs.Domain
{
    public class ModelYearInsurance
    {
        [Key]
        public Guid Id { get; set; }

        public int ModelYear { get; set; }
        public decimal Amount { get; set; }
    }
}
