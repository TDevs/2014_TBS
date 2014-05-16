using System.Collections.Generic;
using System.Web.Security;

namespace Oas.Service.Security
{
    public interface IMembershipService
    {
        MembershipUser CreateUser(string userName, string password);
        bool ValidateUser(string userName, string password);
        MembershipCreateStatus CreateUser(string userName, string password, string email);
        MembershipUser GetUser(string userName);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
        bool ResetPassword(string userName, string newPassword);
        string CreateUserName(string firstName,string lastName);
    }
}
