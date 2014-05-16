using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TDevs.Domain
{
    public class FAQ
    {
        public Guid Id { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }
    }
}

