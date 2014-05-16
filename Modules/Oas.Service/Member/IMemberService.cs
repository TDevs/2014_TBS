using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;
using TDevs.Domain.ViewModel;
 

namespace TDevs.Services
{
    public interface IMemberService
    {
        IList<Member> Get();

        Member Get(Guid Id);

        Member Add(Member member);

        Member Update(Member member);

        bool Remove(Guid Id);

        Vehicle AddVehicleToMemember(Vehicle vehicle);
        void RevmoVehicleFromMember(Guid memberId);

        IList<Vehicle> GetVehicles();

        IList<Vehicle> GetMemberVehicles(Guid memberId);

        Vehicle GetVehicle(Guid vehicleId);

        List<MemberVehicleViewModel> GetVehicles(Guid memberId);

        IList<MemberMedallionVewModel> GetMedallions(Guid memberId);

        IList<Medallion> GetMemberMedallions(Guid memberId);
    }
}
