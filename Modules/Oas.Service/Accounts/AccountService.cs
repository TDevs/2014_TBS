
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Mail;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TDevs.Core.Constants;
using TDevs.Core.Infrastructure;
using TDevs.Domain;
using TDevs.Domain.Entities;
using TDevs.Infrastructure;


namespace TDevs.Services
{
    [ExcludeFromCodeCoverage]
    public class AccountService : IAccountService
    {
        private readonly IRepository<User> accountRepository;
        private readonly IRepository<Role> roleRepository;


        public AccountService(IRepository<User> accountRepository, IRepository<Role> roleRepository)
        {
            this.accountRepository = accountRepository;
            this.roleRepository = roleRepository;
        }


        public List<User> Get()
        {
            return accountRepository.Get.ToList();
        }

        public User Get(string id)
        {
            return accountRepository.Get.SingleOrDefault(s => s.Id == id);
        }

        public User GetByEmail(string email)
        {
            return accountRepository.Get.SingleOrDefault(s => s.Email == email);
        }

        public List<Role> GetRoles(string userId)
        {
            var ur = accountRepository.Get.SelectMany(user => user.Roles, (user, role) => new { user, role })
                .Where(x => x.user.Id.Equals(userId))
                .Select(x => x.role
                ).ToList();

            var xRole = roleRepository.Get.ToList();
            var uRole = xRole.Where(r => ur.Exists(u => u.RoleId.Equals(r.Id))).ToList();
            return uRole;
        }

        public List<Role> GetRoles()
        {
            var roles = roleRepository.Get.ToList();
            return roles;
        }


        public User Find(object[] keyValues)
        {
            return accountRepository.Find(keyValues);
        }

        public List<User> GetUsersByRole(string userRole)
        {
            //return (accountRepository.Get.SelectMany(user => user.Roles, (user, role) => new { user, role })
            //        .Where(x => x.role.Name == userRole).Select(x => x.user)).ToList();
            return null;
        }

        public bool RoleExists(string name)
        {
            var rm = new RoleManager<IdentityRole>(
    new RoleStore<IdentityRole>(new DatabaseContext()));
            return rm.RoleExists(name);
        }

        public bool CreateRole(string name)
        {
            var rm = new RoleManager<IdentityRole>(
     new RoleStore<IdentityRole>(new DatabaseContext()));
            IdentityResult idResult = rm.Create(new IdentityRole(name));
            return idResult.Succeeded;
        }

        public bool CreateUser(User user, string password)
        {
            throw new NotImplementedException();
        }

        public bool CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool AddUserToRole(string userId, string roleName)
        {
            var um = new UserManager<User>(
                    new UserStore<User>(new DatabaseContext()));
            um.UserValidator = new UserValidator<User>(um) { AllowOnlyAlphanumericUserNames = false };
            IdentityResult idResult = um.AddToRole(userId, roleName);
            return idResult.Succeeded;
        }

        public void ClearUserRoles(string userId)
        {
            var um = new UserManager<User>(new UserStore<User>(new DatabaseContext()));
            um.UserValidator = new UserValidator<User>(um) { AllowOnlyAlphanumericUserNames = false };
            User user = um.FindById(userId);
            List<IdentityUserRole> currentRoles = user.Roles.ToList();
            currentRoles.ForEach(item => um.RemoveFromRole(user.Id, item.Role.Name));
        }

        public void UpdateUserInfo(User user)
        {
            accountRepository.Update(user);
        }

        public bool DeleteUser(User user)
        {

            using (var dbContext = new DatabaseContext())
            {

                var rDel = dbContext.Users.Include(t => t.Roles).FirstOrDefault(r => r.Id.Equals(user.Id));

                if (rDel != null)
                {
                    dbContext.Users.Remove(rDel);
                    dbContext.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        public void CreateRole(Role role)
        {
            roleRepository.Add(role);
        }

        public void UpdateRole(Role role)
        {
            roleRepository.Update(role);
        }

        public void DeleteRole(Role role)
        {

            using (var dbContext = new DatabaseContext())
            {

                var rDel = dbContext.Roles.Include(r => r.UserRoles).FirstOrDefault(r => r.Id.Equals(role.Id));

                if (rDel != null)
                {
                    dbContext.Roles.Remove(rDel);
                    dbContext.SaveChanges();
                }
            }

        }


        public User GetUserByUserName(string userName)
        {
            var user = accountRepository.Get.Where(t => t.UserName == userName).FirstOrDefault();
            return user;
        }
    }
}