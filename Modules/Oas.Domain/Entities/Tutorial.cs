using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TDevs.Domain
{
    public class Tutorial
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        [MaxLength(int.MaxValue)]
        public string Content { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateAt { get; set; }
    }
}

