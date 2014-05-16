using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace Oas.Service.Security
{
    public class MembershipService : IMembershipService
    {


        /// <summary>
        /// BaoDT 06/08/2012
        /// Create Membership user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public MembershipUser CreateUser(string userName, string password)
        {
            var user = Membership.CreateUser(userName, password);
            return user;
        }

        /// <summary>
        /// BaoDT 06/08/2012
        /// Validate user using membership
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public bool ValidateUser(string userName, string password)
        {
            return Membership.ValidateUser(userName, password);
        }

        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        /// <author>hoantq</author>
        /// <datetime>8/15/2012-10:05 AM</datetime>
        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            MembershipCreateStatus createStatus;
            Membership.CreateUser(userName, password, email, null, null, true, null, out createStatus);

            return createStatus;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        /// <author>hoantq</author>
        /// <datetime>8/15/2012-10:19 AM</datetime>
        public MembershipUser GetUser(string userName)
        {
            return Membership.GetUser(userName, false /* userIsOnline */);
        }

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns></returns>
        /// <author>hoantq</author>
        /// <datetime>8/15/2012-10:19 AM</datetime>
        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            //Get the user in question
            MembershipUser user = GetUser(userName);
            user.IsApproved = true;
            Membership.UpdateUser(user);

            return user.ChangePassword(oldPassword, newPassword);
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns></returns>
        /// <author>hoantq</author>
        /// <datetime>8/15/2012-11:28 AM</datetime>
        public bool ResetPassword(string userName, string newPassword)
        {
            //Get the user in question
            MembershipUser user = GetUser(userName);
            //Reset password to a temporary value
            if (user.IsLockedOut)
            {
                user.UnlockUser();
            }
            var tempPword = user.ResetPassword();

            user.IsApproved = false;
            Membership.UpdateUser(user);

            //Change the users password from the temp value to the new one
            return user.ChangePassword(tempPword, newPassword);
        }


        public string CreateUserName(string firstName, string lastName)
        {
            firstName = firstName.ToLower().Trim();
            lastName = lastName.ToLower();
            string tem = string.Empty;
            var arr = firstName.Split(' ').ToList();
            arr.ForEach(t =>
            {
                tem = string.Format("{0}{1}", tem, t[0]);
            });
            return string.Format("{0}{1}", lastName, tem);
        }
    }
}
