using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDevs.Domain
{
    public class Agent
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? MemberId { get; set; }
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

        [MaxLength(200)]
        public string MobilePhone { get; set; }
        [MaxLength(200)]
        public string HomePhone { get; set; }
        [MaxLength(200)]
        public string Fax { get; set; }
        [MaxLength(200)]
        public string Email { get; set; }

        [MaxLength(200)]
        public string SSN { get; set; }
        [MaxLength(400)]
        public string Comment { get; set; }
        public DateTime DateOfBirth { get; set; }
        [MaxLength(200)]
        public string ChaufferLic { get; set; }
        [MaxLength(200)]
        public string DriveLic { get; set; }
        [MaxLength(200)]
        public string ContinueEducation { get; set; }
        [MaxLength(200)]
        public string DriverID { get; set; }
        [MaxLength(200)]
        public string LicenseState { get; set; }
        [MaxLength(200)]
        public string Status { get; set; }

        public bool CTATAP { get; set; }
        public bool DayDriver { get; set; }
        public bool NightDriver { get; set; }
        [MaxLength(400)]
        public string PictureAgent { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime ActiveDate { get; set; }
        public DateTime InactiveDate { get; set; }

        public bool IsDeleted { get; set; }
        public Member Member { get; set; } 
    }
}
