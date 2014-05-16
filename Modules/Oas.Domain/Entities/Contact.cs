using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDevs.Domain
{
    public class Contact
    {
        [Key]
        public Guid Id { get; set; }

        public Guid MemberId { get; set; }
        public Member Member { get; set; }

        [MaxLength(200)]
        public string FirstName { get; set; }
        [MaxLength(200)]
        public string LastName { get; set; }
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
        public bool AgentAssigned { get; set; }
        [MaxLength(200)]
        public string Title { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Comments { get; set; }
        [MaxLength(200)]
        public string MobilePhone { get; set; }
        [MaxLength(200)]
        public string HomePhone { get; set; }
        [MaxLength(200)]
        public string SSN { get; set; }
        [MaxLength(200)]
        public string ChaufferLic { get; set; }
        [MaxLength(200)]
        public string DriveLic { get; set; }
        [MaxLength(200)]
        public string ContinueEducation { get; set; }
        public bool IsStockholder { get; set; }
        public bool UseThisAddresForMemberBilling { get; set; }
        public bool UseThisContactAsRegisteredAgent { get; set; }
        [MaxLength(400)]
        public string PictureContact { get; set; }
      
    }
}
