using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace CyberspaceAdmin.Controllers
{
    public class AuthController : Controller
    {
        public IAuthenticationManager Authentication
        {
            get
            {
                return this.Request.GetOwinContext().Authentication;
            }
        }

        //public AuthController()
        //{
        //}

        public ActionResult Logout()
        {
            Authentication.SignOut("ApplicationCookie");
            return Redirect("Login");
        }
        [HttpPost]
        public ActionResult Login(LoginInfo info, string ReturnUrl)
        {
            string username = "gabyslaw@gmail.com";
            string password = "admin";

            if (this.ModelState.IsValid)
            {
                if (username.Equals(info.Username) && password.Equals(info.Password))
                {
                    ClaimsIdentity claimsIdentity =
                        new ClaimsIdentity("ApplicationCookie");
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, info.Username));
                    //claimsIdentity.AddClaim(new Claim("PassportUrl", Url.Content("~/images/profile.png")));


                    var ctxt = this.Request.GetOwinContext();
                    ctxt.Authentication.SignIn(claimsIdentity);

                    return RedirectToAction(info.ReturnUrl);
                }
                else
                {
                    this.ModelState.AddModelError("", "Username or password is invalid");
                }
            }

            return View(info);
        }
        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("Index", "Home");
            }

            return returnUrl;
        }

        [HttpGet]
        public ActionResult Login()
        {
            var model = new LoginInfo();
            return View(model);
        }
    }
}