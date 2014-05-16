

using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using TDevs.Domain;
using TDevs.Domain.Entities;
namespace TDevs.Services
{
    public interface IAccountService
    {
        List<User> Get();
        User Get(string id);
        User GetByEmail(string email);
        List<Role> GetRoles(string userId);
        List<Role> GetRoles();
        User Find(object[] keyValues);
        List<User> GetUsersByRole(string userRole);
        bool RoleExists(string name);
        bool CreateRole(string name);
        bool CreateUser(User user, string password);
        bool CreateUser(User user);
        bool AddUserToRole(string userId, string roleName);
        void ClearUserRoles(string userId);
        void UpdateUserInfo(User user);
        bool DeleteUser(User user);
        void CreateRole(Role role);
        void UpdateRole(Role role);
        void DeleteRole(Role role);

        User GetUserByUserName(string userName);
    }
}
