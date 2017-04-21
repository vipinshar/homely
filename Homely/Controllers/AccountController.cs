using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Homely.Models;
using Homely.Common.IRepository;
using System.IO;
using Homely.Common.Model;
using System.Web.Script.Serialization;
using System.Web.Security;
using Homely.Utility;

namespace Homely.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IRegisterRepository _IRegisterRepository;
        public AccountController(IRegisterRepository IRegisterRepository)
        {
            _IRegisterRepository = IRegisterRepository;
        }

        [AllowAnonymous]
        public ActionResult Login(Common.Model.LoginViewModel model)
        {
            var data = _IRegisterRepository.Login(model);
            if (data == null)
            {
                TempData["login"] = "0";
            }
            else
            {
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1,
        model.login_username,
        DateTime.Now,
        DateTime.Now.AddDays(1),
        true,
        string.Empty);

                // add cookie to response stream         
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                System.Web.HttpCookie authCookie = new System.Web.HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                if (authTicket.IsPersistent)
                {
                    authCookie.Expires = authTicket.Expiration;
                }
                System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
            }
            return RedirectToAction("Index","Home");
        }

        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Registers(Homely.Common.Model.Register model, HttpPostedFileBase profileImage)
        {

            if (ModelState.IsValid)
            {
                string result = string.Empty;
                //string imagePath = Guid.NewGuid().ToString() + Path.GetExtension(profileImage.FileName);
                string msg = _IRegisterRepository.Register(model, "");
                if (msg == "0")
                {
                    TempData["signup"] = "0";
                    result = "The entered Email Id/Mobile is already registered. Please enter another Email Id/Mobile.";
                }
                if (msg == "1")
                {
                    //if (!string.IsNullOrEmpty(profileImage.FileName))
                    //{
                    //    string FolderPathMember = Server.MapPath("~/MemberImages/");
                    //    //profileImage.SaveAs(Path.Combine(FolderPathMember, imagePath));
                    //}

                    try
                    {
                        CommonUtility.SendMail("Registration", model.Name, model.emailId, "", model.emailId, "Registration");
                    }
                    catch
                    { }
                    return View("thankyouIndex", new SuccessMessageViewModel { message = "We thank you for registering with Homely. You will be notified with the details on your email id shortly." });
                }
            }
            return RedirectToAction("Index","Home");
        }

        [AllowAnonymous]
        public ActionResult UserProfile()
        {
            if (User.Identity.IsAuthenticated)
            {
               string userId= User.Identity.Name;
            }
            return View();
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

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
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
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
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