using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace TDevs.Domain.Criteria
{
    public abstract class CriteriaBase
    {
        private static int _pageCount = int.Parse(ConfigurationManager.AppSettings["PageCount"] ?? "20");
        public int? Page { get; set; }
        public int PageCount { get { return _pageCount; } set { _pageCount = value; } }
    }
}
