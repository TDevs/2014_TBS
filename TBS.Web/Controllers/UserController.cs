using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Oas.Service.Security;
using TBS.Web.Models;
using TDevs.Core;
using TDevs.Core.Constants;
using TDevs.Core.Infrastructure;
using TDevs.Domain;
using TDevs.Domain.Entities;
using TDevs.Domain.ViewModel;
using TDevs.Services;

namespace TBS.Web.Controllers
{

    public class UserController : TbsControllerBase
    {
        private readonly IAccountService accountService;
        private readonly INotificationService notificationService;
        private string strCaptcha = "1024";
        public UserController(IAccountService accountService, INotificationService notificationService)
        {
            this.accountService = accountService;
            this.notificationService = notificationService;
            UserManager = new UserManager<User>(new UserStore<User>(new DatabaseContext()));
            UserManager.UserValidator = new UserValidator<User>(UserManager)
            {
                AllowOnlyAlphanumericUserNames = false
            };
        }

        public UserManager<User> UserManager { get; private set; }
        //
        // GET: /User/
        public ActionResult Index()
        {
            ViewBag.SettingActive = "start active";
            return View();
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (Request.IsAjaxRequest())
                return PartialView("_Login");

            return PartialView();
        }


        public ActionResult Lock()
        {
            return View();
        }


        public ActionResult Create()
        {
            return PartialView();
        }

        public ActionResult Roles()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult CheckDuplicateUsername(string username)
        {
            try
            {
                var isDup = accountService.Get().Where(c => c.UserName.ToUpper().Equals(username.ToUpper())).FirstOrDefault() != null;
                return Json(isDup, JsonRequestBehavior.AllowGet);
            }
            catch { return Json(false, JsonRequestBehavior.AllowGet); }
            return Json(false, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult CheckDuplicateEmail(string email)
        {
            try
            {
                var isDup = accountService.Get().Where(c => c.Email.ToUpper().Equals(email.ToUpper())).FirstOrDefault() != null;
                return Json(isDup, JsonRequestBehavior.AllowGet);
            }
            catch { return Json(false, JsonRequestBehavior.AllowGet); }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckDuplicateEmailEditUser(string email, string userId)
        {
            try
            {
                var isDup = accountService.Get().Where(c => c.Email.ToUpper().Equals(email.ToUpper()) && c.Id.ToUpper() != userId.ToUpper()).FirstOrDefault() != null;
                return Json(isDup, JsonRequestBehavior.AllowGet);
            }
            catch { return Json(false, JsonRequestBehavior.AllowGet); }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUsers()
        {
            var users = accountService.Get().ToList();
            var lst = new List<User>();
            users.ForEach(u =>
            {
                lst.Add(new User()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    FirstName = u.FirstName,

                    LastName = u.LastName,
                    Address1 = u.Address1,
                    Address2 = u.Address2,
                    Email = u.Email,
                    Phone1 = u.Phone1,
                    Phone2 = u.Phone2,
                    Active = u.Active,
                    Fax = u.Fax,
                    City = u.City,
                    SecretQuestion = u.SecretQuestion,
                    SecretAnswer = u.SecretAnswer,
                    State = u.State,
                    Zip = u.Zip
                });
            });
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRoles()
        {
            var roles = accountService.GetRoles().ToList();
            var lst = new List<Role>();
            roles.ForEach(r =>
            {
                lst.Add(new Role()
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    Predefined = r.Predefined,
                    AccountCanEdit = r.AccountCanEdit,
                    AccountCanView = r.AccountCanView,
                    AgentCanCreate = r.AgentCanCreate,
                    MedallionCanDelete = r.MedallionCanDelete,

                    BillOutMember = r.BillOutMember,
                    ViewMemberData = r.ViewMemberData,
                    ViewDeletedMembers = r.ViewDeletedMembers,
                    ViewMemberCashiering = r.ViewMemberCashiering,
                    EditMemberData = r.EditMemberData,
                    DeleteMemberData = r.DeleteMemberData,

                    ReportCanMakeReport = r.ReportCanMakeReport,
                    ReportCanMakeEndOfDayTrans = r.ReportCanMakeEndOfDayTrans,
                    RoleCanViewList = r.RoleCanViewList,
                    RoleCanEdit = r.RoleCanEdit,
                    RoleCanCreate = r.RoleCanCreate,
                    RoleCanDelete = r.RoleCanDelete,
                    UserCanViewList = r.UserCanViewList,
                    UserCanEdit = r.UserCanEdit,
                    UserCanCreate = r.UserCanCreate,
                    UserCanDelete = r.UserCanDelete,

                    ReferenceCanEdit = r.ReferenceCanEdit,



                    AllowDashboard = r.AllowDashboard

                });
            });
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetUseRoles(Guid? userId)
        {
            var uid = userId.Value.ToString();
            var roles = accountService.GetRoles();
            var userRoles = accountService.GetRoles(uid);
            var ur = new List<UserInRole>();
            roles.ForEach(r =>
            {
                ur.Add(new UserInRole()
                {
                    RoleId = r.Id,
                    UserId = uid,
                    Name = r.Name,
                    IsInRole = userRoles.Exists(t => t.Id == r.Id)
                });
            });
            return Json(ur, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetNotifications()
        {
            var lst =  notificationService.GetNotifications(User.Identity.GetUserId());
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCurrentRole()
        {

            var uid = User.Identity.GetUserId();
            var currentRole = accountService.GetRoles(uid).FirstOrDefault();
            if (currentRole == null)
                currentRole = new Role();
            var role = new RoleViewModel()
            {
                Name = currentRole.Name,
                Description = currentRole.Description,
                Predefined = currentRole.Predefined,
                AccountCanEdit = currentRole.AccountCanEdit,
                AccountCanView = currentRole.AccountCanView,
                AgentCanCreate = currentRole.AgentCanCreate,
                MedallionCanDelete = currentRole.MedallionCanDelete,

                BillOutMember = currentRole.BillOutMember,
                ViewMemberData = currentRole.ViewMemberData,
                ViewDeletedMembers = currentRole.ViewDeletedMembers,
                ViewMemberCashiering = currentRole.ViewMemberCashiering,
                EditMemberData = currentRole.EditMemberData,
                DeleteMemberData = currentRole.DeleteMemberData,

                ReportCanMakeReport = currentRole.ReportCanMakeReport,
                ReportCanMakeEndOfDayTrans = currentRole.ReportCanMakeEndOfDayTrans,
                RoleCanViewList = currentRole.RoleCanViewList,
                RoleCanEdit = currentRole.RoleCanEdit,
                RoleCanCreate = currentRole.RoleCanCreate,
                RoleCanDelete = currentRole.RoleCanDelete,
                UserCanViewList = currentRole.UserCanViewList,
                UserCanEdit = currentRole.UserCanEdit,
                UserCanCreate = currentRole.UserCanCreate,
                UserCanDelete = currentRole.UserCanDelete,

                ReferenceCanEdit = currentRole.ReferenceCanEdit,
                AllowDashboard = currentRole.AllowDashboard,


                IsAdmin = currentRole.Name == UserRoles.Administrator.ToString()

            };
            return Json(role, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public JsonResult EditRoles(IList<UserInRole> Roles)
        {
            if (Roles.Count > 0)
            {
                var userId = Roles.First().UserId;
                accountService.ClearUserRoles(userId);

                Roles.Where(x => x.IsInRole)
                .ForEach(item => accountService.AddUserToRole(item.UserId, item.Name));
            }
            return Json(true);
        }

        [HttpPost]
        public JsonResult Create(CreateUserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var newUser = new User()
                {
                    Id = Guid.NewGuid().ToString(),

                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    SecretQuestion = user.SecretQuestion,
                    SecretAnswer = user.SecretAnswer,
                    Email = user.Email,
                    Phone1 = user.Phone1,
                    Phone2 = user.Phone2,
                    Address1 = user.Address1,
                    Address2 = user.Address2,
                    City = user.City,
                    State = user.State,
                    Zip = user.Zip,
                    Fax = user.Fax,
                    CreateDate = DateTime.Now,
                    Active = user.Active

                };
                try
                {

                    //var password = Encryptor.MD5Hash(user.Password);

                    IdentityResult result = UserManager.Create(newUser, user.Password);

                    if (result.Succeeded)
                    {
                        UserManager.AddToRoleAsync(newUser.Id, UserRoles.Employee); ;
                        return Json(true);
                    }
                    return Json(false);
                }
                catch (Exception ex)
                {
                    //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                }
            }
            return Json(true);
        }


        [HttpPost]
        public JsonResult Delete(User user)
        {
            if (user.UserName != User.Identity.Name)
            {
                accountService.DeleteUser(user);
                return Json(true);
            }
            else

            { return Json(false); }
        }

        public ActionResult Edit()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult Edit(CreateUserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var updateUser = new User()
                {
                    Id = user.Id.ToString(),
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    SecretQuestion = user.SecretQuestion,
                    SecretAnswer = user.SecretAnswer,
                    Email = user.Email,
                    Phone1 = user.Phone1,
                    Phone2 = user.Phone2,
                    Address1 = user.Address1,
                    Address2 = user.Address2,
                    City = user.City,
                    State = user.State,
                    Zip = user.Zip,
                    Fax = user.Fax,
                    CreateDate = DateTime.Now,
                    Active = user.Active

                };

                if (!string.IsNullOrEmpty(user.Password))// Password has changed
                {
                    var newPwd = Oas.Service.Security.Encryptor.MD5Hash(user.Password);
                    var result = UserManager.AddPassword(updateUser.Id, newPwd);
                }
                accountService.UpdateUserInfo(updateUser);
                return Json(true);//Json(updateUser, JsonRequestBehavior.AllowGet);
            }
            return Json(false);
        }
        //

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                int count = model.LoginTime;
                var user = UserManager.Find(model.UserName, model.Password);
                if (user != null)
                {
                    if (user.Active)
                    {
                        await SignInAsync(user, model.RememberMe);
                        Session["IsLock"] = null;
                        return Json(true);
                    }
                    else
                    {

                        ModelState.AddModelError("", "This account is locked..");
                    }
                }
                else
                {

                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return Json(false);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> ReLogin(LoginViewModel model)
        {
            //if (ModelState.IsValid)
            {

                int count = model.LoginTime;
                var user = UserManager.Find(model.UserName, model.Password);
                if (user != null)
                {
                    if (user.Active)
                    {
                        await SignInAsync(user, model.RememberMe);
                        Session["IsLock"] = null;
                        Session["User"] = null;
                        return Json(true);
                    }

                }
            }

            // If we got this far, something failed, redisplay form
            return Json(false);
        }
        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            Session["IsLock"] = null;
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        [AllowAnonymous, HttpPost]
        public JsonResult GetCaptcha()
        {
            return Json(strCaptcha);

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(User user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion

    }
}