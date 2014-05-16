using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TDevs.Domain;

namespace TBS.Web.Utility
{
    public class ResultAgent
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
         
        public string Status { get; set; }
        
        public string ActiveDate { get; set; }

        public string ChaufferLic { get; set; }

        public string MobilePhone { get; set; }

        public string HomePhone { get; set; }
        
       
        public Agent Agent { get; set; }
        public Guid VehicleId { get; set; }
        public string VehicleName { get; set; }
    }

    public class ResultAgentVehilce
    {
        public AgentVehicle AgentVehicle { get;set; }         
        public string  LicenseNumber { get; set; }
        public bool IsNew { get; set; }
    }
}