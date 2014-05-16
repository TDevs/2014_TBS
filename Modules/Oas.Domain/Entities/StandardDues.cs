using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDevs.Domain
{
    public class StandardDue
    {
        [Key]
        public Guid Id { get; set; }

        public Guid MemberId { get; set; }

        [ForeignKey("MemberId")]
        public Member Member { get; set; }

        public DateTime StartDate { get; set; }
        public decimal Dues { get; set; }
    }
}
