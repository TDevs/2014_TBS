using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDevs.Domain.Models
{
    public class EnumModelView
    {

        public int Value { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public bool IsChoose { get; set; }
    }
}
