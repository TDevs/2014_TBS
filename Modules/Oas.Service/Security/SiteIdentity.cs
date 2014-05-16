using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using TDevs.Core.Repository;
using TDevs.Domain.Entities;


namespace Oas.Service.Security
{
    /// <summary>
    /// Site Identity 
    /// </summary>
    /// Created by SMK
    public class SiteIdentity : System.Security.Principal.IIdentity
    {
        private readonly IUserRepository userRepository;
        private User user;

        public SiteIdentity(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public string AuthenticationType
        {
            get
            {
                return "Custom Authentication";
            }
            set
            {
                // do nothing
            }
        }
        public bool IsAuthenticated
        {
            get { return true; }
        }

        public SiteIdentity CreateInstance(string userName)
        {
            var obj = userRepository.GetUserProfile(userName);
            user = obj as User;

            return this;
        }


        #region IIdentity Members


        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        /// <value></value>
        /// <returns>The name of the user on whose behalf the code is running.</returns>
        /// Created by SMK
        public string Name
        {
            get { if (user != null) return string.Format("{0}", user.FullName); return string.Empty; }
        }


        public Guid UserId
        {
            get { { if (user != null) return user.Id; return Guid.Empty; } }
        }

        public Guid ApplicationId { get { return new Guid(ConfigurationManager.AppSettings["ApplicationId"]); } }

        public string Role
        {
            get{return string.Empty;}
        }

        public User CurrentUser
        {
            get { return user; }
        }
        #endregion
    }
}
