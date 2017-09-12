﻿using AllThingsDelivered.Models;
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
        AllThingsDeliveredDBEntities db = new AllThingsDeliveredDBEntities();
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var manager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
                IdentityUser user = new IdentityUser() { UserName = model.username, Email = model.username };
                IdentityResult result = await manager.CreateAsync(user, model.password1);

                if (!result.Succeeded)
                {
                    ViewBag.Error = result.Errors;
                    return View();
                }

                db.Customers.Add(new Customer { AspNetUserID = user.Id, FirstName = model.firstname, LastName = model.lastname, PhoneNumber = model.phone });
                db.SaveChanges();
                var authenticationManager = HttpContext.GetOwinContext().Authentication;
                var userIdentity = await manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(new Microsoft.Owin.Security.AuthenticationProperties() { }, userIdentity);

                string token = manager.GenerateEmailConfirmationToken(user.Id);
                string sendGridApiKey = System.Configuration.ConfigurationManager.AppSettings["SendGrid.ApiKey"];
                SendGrid.SendGridClient client = new SendGrid.SendGridClient(sendGridApiKey);
                SendGrid.Helpers.Mail.SendGridMessage message = new SendGrid.Helpers.Mail.SendGridMessage();
                message.AddTo(model.username);
                message.Subject = "Confirm your account on AllThingsDelivered.com";
                message.SetFrom("no-reply@allthingsdelivered.com", "All Things Deivered Administration");
                string body = string.Format("<a href=\"{0}/account/ConfirmAccount?email={1}&token={2}\">Confirm your account</a>",
                    Request.Url.GetLeftPart(UriPartial.Authority),
                    model.username,
                    token
                    );
                message.AddContent("text/html", body);
                message.SetTemplateId("8765a1ec-6865-4be4-8854-b04ef686d63e");
                var response = client.SendEmailAsync(message).Result;
                var ResponseBody = response.Body.ReadAsStringAsync().Result;

                return RedirectToAction("Index", "Restaurant");
            }
            else
            {
                return View();
            }
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(forgotpassword model)
        {
            if (ModelState.IsValid)
            {
                var manager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();

                IdentityUser user = manager.FindByEmail(model.email);
                string resetToken = manager.GeneratePasswordResetToken(user.Id);
                string sendGridApiKey = System.Configuration.ConfigurationManager.AppSettings["SendGrid.ApiKey"];
                SendGrid.SendGridClient client = new SendGrid.SendGridClient(sendGridApiKey);
                SendGrid.Helpers.Mail.SendGridMessage message = new SendGrid.Helpers.Mail.SendGridMessage();
                message.AddTo(model.email);
                message.Subject = "Reset your password on AllThingsDelivered.com";
                message.SetFrom("no-reply@allthingsdelivered.com", "All Things Deivered Administration");
                string body = string.Format("<a href=\"{0}/account/resetpassword?email={1}&token={2}\">Reset your password</a>", 
                    Request.Url.GetLeftPart(UriPartial.Authority),
                    model.email,
                    resetToken
                    );
                message.AddContent("text/html", body);
                message.SetTemplateId("8765a1ec-6865-4be4-8854-b04ef686d63e");
                var response = client.SendEmailAsync(message).Result;
                var ResponseBody = response.Body.ReadAsStringAsync().Result;

                return RedirectToAction("ForgotPasswordSent");
            }
            return View();
        }

        public ActionResult ForgotPasswordSent()
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
        public ActionResult SignIn(string username, string password, bool? rememberMe, string returnUrl)
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
                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                ViewBag.Errors = new string[] { "Could not sign in with this username/password" };
            }
            return View();
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(string email, string token, string password)
        {
            if (ModelState.IsValid)
            {
                var manager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
                IdentityUser user = manager.FindByEmail(email);
                IdentityResult result = manager.ResetPassword(user.Id, token, password);
                if (result.Succeeded)
                {
                    TempData["PasswordReset"] = "Your password has been reset successfully";
                    return RedirectToAction("SignIn");
                }
                ViewBag.Errors = result.Errors;
            }
            return View();
        }

        public ActionResult SignOut()
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
        
        public ActionResult ConfirmAccount(string email, string token)
        {
            if (ModelState.IsValid)
            {
                var manager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
                IdentityUser user = manager.FindByEmail(email);
                IdentityResult result = manager.ConfirmEmail(user.Id, token);
                if (result.Succeeded)
                {
                    TempData["ConfirmEmail"] = "Your Email address has been confirmed";
                    return RedirectToAction("SignIn");
                }
                ViewBag.Errors = result.Errors;
            }
            return View();
        }
    }
}