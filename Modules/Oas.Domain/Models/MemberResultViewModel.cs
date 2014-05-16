using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDevs.Domain.ViewModel
{
    public class MemberResultViewModel
    {
        public string AccountNumber { get; set; }
        public string MedallionNumber { get; set; }
        public Member Member { get; set; }
        public string MemberId { get; set; }
        public string MemberName { get; set; }
        public string PaymentDueDate { get; set; }
        public string Phone { get; set; }
        public string PreferPayment { get; set; }
        public string Status { get; set; }
        public string ChaufferLic { get; set; }
        public string AgentName { get; set; }
        public string ContactName { get; set; }
        public string SSN { get; set; }
    }
}
