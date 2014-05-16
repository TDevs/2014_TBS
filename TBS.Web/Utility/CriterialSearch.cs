using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TBS.Web.Utility
{
    public class CriterialSearch
    {
        public string Keyword { get; set; }
        public bool ViewDeleted { get; set; }
        public int SearchBy { get; set; }
        public string SearchFilter { get; set; }
    }

    public enum EnumSearchBy
    {
        SearchByAccountNumber = 1,
        SearchByMedallionNumber = 2,
        SearchAgentsChaufferNumber = 3,
        SearchBySSN = 4,
        SearchByMemberName = 5,
        SearchByMemberContactName = 6,
        SearchByAgentName = 7,
        //SearchByAll = 8
    }
}