using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDevs.Domain
{
    public class UniversalAgentRecord
    {
        [Key]
        public Guid Id { get; set; }
        public string LicenseNumber { get; set; }
        public bool IsRenewed { get; set; }
        public string Status { get; set; }
        public DateTime? StatusDate { get; set; }
        public string DriverType { get; set; }
        public string LicenseType { get; set; }
        public DateTime? OriginalIssueDate { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string ChaufferCity { get; set; }
        public string ChaufferState { get; set; }
    }
}
