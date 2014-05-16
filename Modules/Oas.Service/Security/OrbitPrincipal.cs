using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Security.Cryptography;
using System.Web.Security;
using TDevs.Core.Repository;


namespace Oas.Service.Security
{
    /// <summary>
    /// Orbit Principal
    /// </summary>
    /// Created by SMK
    public class SitePrincipal : System.Security.Principal.IPrincipal
    {
        private readonly IUserRepository userRepository;
        private readonly SiteIdentity siteIdentity;
        protected System.Security.Principal.IIdentity identity;

        /// <summary>
        /// Initializes a new instance of the <see cref="SitePrincipal"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// Created by SMK
        public SitePrincipal(IUserRepository userRepository, SiteIdentity siteIdentity)
        {
            this.userRepository = userRepository;
            this.siteIdentity = siteIdentity;
        }

        /// <summary>
        /// Gets the identity of the current principal.
        /// </summary>
        /// <value></value>
        /// <returns>The <see cref="T:System.Security.Principal.IIdentity"/> object associated with the current principal.</returns>
        /// Created by SMK
        public System.Security.Principal.IIdentity Identity
        {
            get
            {
                return identity;
            }
            set
            {
                identity = value;
            }
        }

        public bool IsInRole(string role)
        {
            return CurrentRoles.Contains(role);
        }

        public string[] CurrentRoles { get; set; }


        public SitePrincipal CreateInstance(string userName)
        {
            identity = siteIdentity.CreateInstance(userName) as SiteIdentity;
            CurrentRoles = new string[] { siteIdentity.Role };
            return this;
        }


        public bool CheckExistUserName(string userName)
        {
            return userRepository.IsExistUserName(userName);
        }

        public bool LockAccount(string userName)
        {
            return userRepository.LockAccount(userName);
        }


    }
}
