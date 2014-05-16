using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDevs.Domain
{
    public class Vehicle
    {
        [Key]
        public Guid Id { get; set; }

        public Guid MemberId { get; set; }

        [ForeignKey("MemberId")]
        public Member Member { get; set; }

        [MaxLength(200)]
        public string LicenseNumber { get; set; }
        public Guid VehicleMakeId { get; set; }
        public Guid VehicleModelId { get; set; }
        public Guid VehicleFeatureId { get; set; }
        public Guid MeterManufacturerId { get; set; }
        public Guid? MedallionId { get; set; }

        [MaxLength(200)]
        public string VINNumber { get; set; }
        [MaxLength(200)]
        public string InsurancePolicyNumber { get; set; }
        [MaxLength(200)]
        public string WorkmanCompNumber { get; set; }
        [MaxLength(200)]
        public string LicenseState { get; set; }
        [MaxLength(200)]
        public string RadioSerialNumber { get; set; }


        [MaxLength(200)]
        public string MeterNumber { get; set; }
        [MaxLength(200)]
        public string EINNumber { get; set; }
        [MaxLength(200)]


        public string ModelYear { get; set; }
        public bool UnderServed { get; set; }


        [ForeignKey("VehicleMakeId")]
        public VehicleMake VehicleMake { get; set; }

        [ForeignKey("VehicleModelId")]
        public VehicleModel VehicleModel { get; set; }

        [ForeignKey("VehicleFeatureId")]
        public VehicleFeature VehicleFeature { get; set; }

        [ForeignKey("MeterManufacturerId")]
        public MeterManufacturer MeterManufacturer { get; set; }

        [ForeignKey("MedallionId")]
        public Medallion Medallion { get; set; }
    }
}
