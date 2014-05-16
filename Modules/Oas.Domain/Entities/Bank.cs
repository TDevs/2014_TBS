using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDevs.Domain
{
    public class Bank
    {
        [Key]
        public Guid Id { get; set; }

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
        [MaxLength(400)]
        public string Comment { get; set; }
        [MaxLength(200)]
        public string Phone1 { get; set; }
        [MaxLength(200)]
        public string Phone2 { get; set; }
        [MaxLength(200)]
        public string Fax { get; set; }
        [MaxLength(200)]
        public string Email { get; set; }
    }
}
