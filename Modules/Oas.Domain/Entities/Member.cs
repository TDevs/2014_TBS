using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDevs.Domain
{
    public class Member
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(200)]
        public string AccountNumber { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Address { get; set; }
        [MaxLength(200)]
        public string Address1 { get; set; }
        [MaxLength(200)]
        public string City { get; set; }
        [MaxLength(10)]
        public string State { get; set; }
        [MaxLength(5)]
        public string Zip { get; set; }

        [MaxLength(200)]
        public string Phone1 { get; set; }
        [MaxLength(200)]
        public string Phone2 { get; set; }
        [MaxLength(200)]
        public string Fax { get; set; }
        [MaxLength(200)]
        public string Email { get; set; }

        [MaxLength(200)]
        public string SSN { get; set; }
        [MaxLength(200)]
        public string IRIS { get; set; }
        [MaxLength(200)]
        public string Article { get; set; }
        [MaxLength(200)]
        public string Fein { get; set; }


        public DateTime? IncorporationDate { get; set; }
        public DateTime? JoinedDate { get; set; }

        [MaxLength(200)]
        public string Status { get; set; }


        public bool UseThisAddressForBilling { get; set; }
        [MaxLength(200)]
        public string PreferPayment { get; set; }
        public int LateBreaks { get; set; }
        public decimal? DefaultWorkerComp { get; set; }
        [MaxLength(200)]
        public string RegisteredAgentName { get; set; }
        [MaxLength(200)]
        public string AgentAddress { get; set; }
        [MaxLength(200)]
        public string AgentAddress1 { get; set; }
        [MaxLength(200)]
        public string AgentCity { get; set; }
        [MaxLength(10)]
        public string AgentState { get; set; }
        [MaxLength(5)]
        public string AgentZip { get; set; }

        public string ReciepMessage { get; set; }

        public virtual ICollection<StandardDue> StandardDues { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }


        public bool IsDeleted { get; set; }
    }
}
