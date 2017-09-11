using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodingTemple.CodingCookware.Web.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Register()
        {
            return View();
        }

        //GET: Account/signIn
        public ActionResult SignIn()
        {
            return View();
        }

        //POST: Account/SignIn
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(string username, string password, bool? rememberMe)
        {
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
                IdentityUser user = userManager.FindByName(username);
                if (User != null)
                {
                    if (userManager.CheckPassword(user, password))
                    {
                        var userIdentity = userManager.CreateIdentity(user,
                            DefaultAuthenticationTypes.ApplicationCookie);

                        var authenticationManager = HttpContext.GetOwinContext().Authentication;
                        authenticationManager.SignIn(
                            new Microsoft.Owin.Security.AuthenticationProperties
                            {
                                IsPersistent = rememberMe ?? false,
                                ExpiresUtc = DateTime.UtcNow.AddDays(7)
                            }, userIdentity
                            );
                        return RedirectToAction("Index", "Home");
                    }
                }
                ViewBag.Errors = new string[] { "Could not sign in with this username/password" };
            }
            return View();
        }
        
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Register(string username, string password)
        {
            var userStore = new Microsoft.AspNet.Identity.EntityFramework.UserStore<Microsoft.AspNet.Identity.EntityFramework.IdentityUser>();
            var manager = new Microsoft.AspNet.Identity.UserManager<Microsoft.AspNet.Identity.EntityFramework.IdentityUser>(userStore);
            var user = new Microsoft.AspNet.Identity.EntityFramework.IdentityUser() { UserName = username };
            
            Microsoft.AspNet.Identity.IdentityResult result = await manager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                var authenticationManager = HttpContext.GetOwinContext().Authentication;
                var userIdentity = await manager.CreateIdentityAsync(user, Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(new Microsoft.Owin.Security.AuthenticationProperties() { }, userIdentity);
            }
            else
            {
                ViewBag.Error = result.Errors;
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}