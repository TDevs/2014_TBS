using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using TDevs.Core.Infrastructure;
using TDevs.Domain;
using TDevs.Domain.Entities;
using TDevs.Domain.ViewModel;

namespace TDevs.Services
{
    public class MemberService : IMemberService
    {
        private readonly IRepository<Member> memberRepository;
        private readonly IRepository<Vehicle> vehicleRepository;
        private readonly IRepository<Medallion> medallionRepository;
        public MemberService(IRepository<Member> memberRepository,
            IRepository<Vehicle> vehicleRepository,
            IRepository<Medallion> medallionRepository)
        {
            this.memberRepository = memberRepository;
            this.vehicleRepository = vehicleRepository;
            this.medallionRepository = medallionRepository;
        }

        public IList<Member> Get()
        {
            return memberRepository.Get.ToList();
        }

        public Member Get(Guid Id)
        {
            return memberRepository.Get.FirstOrDefault(c => c.Id == Id);
        }

        public Member Add(Member member)
        {
            memberRepository.Add(member);
            return member;
        }

        public Member Update(Member member)
        {
            memberRepository.Update(member);
            return member;
        }

        public bool Remove(Guid Id)
        {
            try
            {
                var member = Get(Id);
                if (member == null) return false;
                memberRepository.Remove(member);
                return true;
            }
            catch
            {

            }
            return false;
        }

        #region KhoaHT added for Member Vehicles
        public Vehicle AddVehicleToMemember(Vehicle vehicle)
        {
            var obj = vehicleRepository.Add(vehicle);
            return obj;
        }

        public void RevmoVehicleFromMember(Guid memberId)
        {
            var vehicles = vehicleRepository.Get.Where(t => t.MemberId.Equals(memberId));
            foreach (var item in vehicles)
            {
                vehicleRepository.Remove(item);
            }

        }

        public IList<Vehicle> GetVehicles()
        {
            var lst = vehicleRepository.Get.ToList();
            return lst;
        }

        public IList<Vehicle> GetMemberVehicles(Guid memberId)
        {
            var list = vehicleRepository.Get.Where(t => t.MemberId.Equals(memberId)).ToList();
            return list;
        }


        public Vehicle GetVehicle(Guid vehicleId)
        {
            var vehicle = vehicleRepository.Find(vehicleId);
            return vehicle;
        }

        #endregion


        /// <summary>
        /// Get Medallions of a member by MemberId
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public IList<MemberMedallionVewModel> GetMedallions(Guid memberId)
        {

            var lst = medallionRepository.Get
                .Include(m => m.Member).Where(m=>m.MemberId==memberId)
                .Select(t => new MemberMedallionVewModel()
                {
                    MedallionId = t.Id,
                    Balance = t.Balance,
                    BillingStartDate = t.BillingStartDate,
                    BillingEndDate = t.BillingEndDate,
                    Collision = t.Collision,
                    InsuranceSurcharge = t.InsuranceSurcharge,
                    DateJoined = t.DateJoined,
                    //LicenseNumber = t.vehicle.LicenseNumber,
                    //Make = t.vehicle.VehicleMake.Name,
                    //Model = t.vehicle.VehicleModel.Name,
                    MedallionNumber = t.MedallionNumber,
                    UnderServed = t.UnderServed
                    //Year = t.vehicle.ModelYear
                })
                .ToList();

            //var lst = medallionRepository.Get
            //    .Include(m => m.Member)
            //    .Where(m => m.MemberId.Equals(memberId))
            //    .Include(m => m.Member.Vehicles)
            //    .SelectMany(medal => medal.Member.Vehicles, (medal, vehicle) => new { medal, vehicle })
            //    .Where(x => x.medal.MemberId.Equals(x.vehicle.MemberId)).Include(x => x.vehicle.VehicleMake)
            //    .Include(x => x.vehicle.VehicleModel)
            //    .Include(x => x.vehicle.VehicleFeature)
            //    .Select(t => new MemberMedallionVewModel()
            //    {
            //        MedallionId=t.medal.Id,
            //        Balance = t.medal.Balance,
            //        BillingStartDate = t.medal.BillingStartDate,
            //        BillingEndDate = t.medal.BillingEndDate,
            //        Collision = t.medal.Collision,
            //        InsuranceSurcharge = t.medal.InsuranceSurcharge,
            //        DateJoined = t.medal.DateJoined,
            //        LicenseNumber = t.vehicle.LicenseNumber,
            //        Make = t.vehicle.VehicleMake.Name,
            //        Model = t.vehicle.VehicleModel.Name,
            //        MedallionNumber = t.medal.MedallionNumber,
            //        UnderServed = t.medal.UnderServed,
            //        Year = t.vehicle.ModelYear
            //    })
            //    .ToList();

            return lst;
        }


        /// <summary>
        /// Get Vehicles of a member
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public List<MemberVehicleViewModel> GetVehicles(Guid memberId)
        {
            var list = vehicleRepository.Get.Include(t => t.Member)
                .Include(t => t.VehicleFeature)
                .Include(t => t.VehicleModel)
                .Include(t => t.VehicleMake)
                .Where(t => t.MemberId == memberId)
                .Select(t => new MemberVehicleViewModel()
                {
                    LicenseNumber = t.LicenseNumber,
                    Make = t.VehicleMake.Name,
                    Model = t.VehicleModel.Name,
                    Year = t.ModelYear
                }).ToList();

            return list;
        }


        public IList<Medallion> GetMemberMedallions(Guid memberId)
        {
            var medallions = medallionRepository.Get.Where(m => m.MemberId.Equals(memberId)).ToList();
            return medallions;
        }
    }
}
