using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TDevs.Domain;

namespace TBS.Web.Utility
{
    public class ResultMemberSearch
    {
        public Guid MemberId { get; set; }

        public Member Member { get; set; }
        public string AccountNumber { get; set; }
        public string MemberName { get; set; }
        public string Phone { get; set; }
        public string PreferPayment { get; set; }
        public DateTime? PaymentDueDate { get; set; }
        public string MedallionNumber { get; set; }
        public string Status { get; set; }
        public string ChaufferLic { get; set; }
        public string AgentName { get; set; }
        public string ContactName { get; set; }
        public string SSN { get; set; }
    }
}