using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using TDevs.Domain.Entities;


namespace TDevs.Domain
{

    /// <summary>
    /// User
    /// </summary>
    public class User : IdentityUser
    {


        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string SecretQuestion { get; set; }

        public string SecretAnswer { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [DataType(DataType.PhoneNumber)]
        public string Phone1 { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone2 { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        [DataType(DataType.PostalCode)]
        public string Zip { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Fax { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }

        [NotMapped]
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }

        [NotMapped]        
        public string Address { get { return string.IsNullOrEmpty(Address1) ? Address2 : Address1; } }

        public bool Active { get; set; }

                
    }
}
