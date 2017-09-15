using AllThingsDelivered.Models;
using Braintree;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
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

        //Register GET
        public ActionResult Register()
        {
            List<SelectListItem> StateList = new List<SelectListItem>();
            StateList.Add(new SelectListItem { Text = "Select A State", Value = "" });
            foreach (State state in db.States)
            {
                StateList.Add(new SelectListItem { Text = state.StateName, Value = state.StateName });
            }

            List<SelectListItem> CountryList = new List<SelectListItem>();
            CountryList.Add(new SelectListItem { Text = "Select A Country", Value = "" });
            foreach (Country country in db.Countries)
            {
                CountryList.Add(new SelectListItem { Text = country.CountryName, Value = country.CountryName });
            }

            RegisterModel model = new RegisterModel
            {
                stateList = StateList,
                countryList = CountryList
            };
            return View(model);
        }

        //Register POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Register(RegisterModel model)
        {
            //TODO: Better way to do this state/country list?
            List<SelectListItem> StateList = new List<SelectListItem>();
            StateList.Add(new SelectListItem { Text = "Select A State", Value = "" });
            foreach (State state in db.States)
            {
                StateList.Add(new SelectListItem { Text = state.StateName, Value = state.StateName });
            }

            List<SelectListItem> CountryList = new List<SelectListItem>();
            CountryList.Add(new SelectListItem { Text = "Select A Country", Value = "" });
            foreach (Country country in db.Countries)
            {
                CountryList.Add(new SelectListItem { Text = country.CountryName, Value = country.CountryName });
            }

            model.stateList = StateList;
            model.countryList = CountryList;

            if (ModelState.IsValid)
            {
                Result<Braintree.Customer> braintreeResult;

                string merchantId = ConfigurationManager.AppSettings["Braintree.MerchantID"];
                var environment = Braintree.Environment.SANDBOX;
                string publicKey = ConfigurationManager.AppSettings["Braintree.PublicKey"];
                string privateKey = ConfigurationManager.AppSettings["Braintree.PrivateKey"];
                Braintree.BraintreeGateway braintreeGateway = new Braintree.BraintreeGateway(environment, merchantId, publicKey, privateKey);
                
                var request = new CustomerRequest
                {
                    FirstName = model.firstname,
                    LastName = model.lastname,
                    Email = model.username,
                    Phone = model.phone
                };
                braintreeResult = braintreeGateway.Customer.Create(request);
                bool success = braintreeResult.IsSuccess();
                string braintreeId = braintreeResult.Target.Id;
                if (!success)
                {
                    TempData["Errors"] = braintreeResult.Errors;
                    return View(model);
                }

                var manager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
                IdentityUser user = new IdentityUser() { UserName = model.username, Email = model.username };
                IdentityResult userResult = await manager.CreateAsync(user, model.password1);

                if (!userResult.Succeeded)
                {
                    TempData["Errors"] = userResult.Errors;
                    return View(model);
                }

                AllThingsDelivered.Models.Address address = new AllThingsDelivered.Models.Address {
                    Line1 = model.line1,
                    Line2 = model.line2,
                    City = model.city,
                    State = model.state,
                    ZipCode = model.postalcode,
                    Country = model.country,
                    Deleted = false,
                    AddressType = "Customer"
                };
                db.Addresses.Add(address);

                AllThingsDelivered.Models.Customer thisCustomer = new AllThingsDelivered.Models.Customer {
                    AspNetUserID = user.Id,
                    FirstName = model.firstname,
                    LastName = model.lastname,
                    PhoneNumber = model.phone,
                    BrainTreeID = braintreeResult.Target.Id
                };
                db.Customers.Add(thisCustomer);
                db.SaveChanges();

                CustomerAddress customerAddress = new CustomerAddress { DefaultAddr = true };
                customerAddress.CustomerID = thisCustomer.CustomerID;
                customerAddress.AddressID = address.AddressID;
                db.CustomerAddresses.Add(customerAddress);
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
                
                return RedirectToAction("Index", "Restaurants");
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
        public ActionResult ForgotPassword(ForgotPassword model)
        {
            if (ModelState.IsValid)
            {
                var manager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
                IdentityUser user = manager.FindByEmail(model.email);

                if (user != null)
                {
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
                }
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
        public ActionResult SignIn(SignIn model)
        {
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
                IdentityUser user = userManager.FindByName(model.username);
                if (User != null)
                {
                    if (userManager.CheckPassword(user, model.password))
                    {
                        var userIdentity = userManager.CreateIdentity(user,
                            DefaultAuthenticationTypes.ApplicationCookie);

                        var authenticationManager = HttpContext.GetOwinContext().Authentication;
                        authenticationManager.SignIn(
                            new Microsoft.Owin.Security.AuthenticationProperties
                            {
                                IsPersistent = model.rememberMe,
                                ExpiresUtc = DateTime.UtcNow.AddDays(7)
                            }, userIdentity
                            );
                        if (!string.IsNullOrEmpty(model.returnUrl))
                        {
                            return Redirect(model.returnUrl);
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
        public ActionResult ResetPassword(ResetPassword model)
        {
            if (ModelState.IsValid)
            {
                var manager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
                IdentityUser user = manager.FindByEmail(model.email);
                if (user != null)
                {
                    IdentityResult result = manager.ResetPassword(user.Id, model.token, model.password);
                    if (result.Succeeded)
                    {
                        TempData["PasswordReset"] = "Your password has been reset successfully";
                        return RedirectToAction("SignIn");
                    }
                    ViewBag.Errors = result.Errors;
                }
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
            var manager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
            IdentityUser user = manager.FindByEmail(email);
            IdentityResult result = manager.ConfirmEmail(user.Id, token);
            if (result.Succeeded)
            {
                TempData["ConfirmEmail"] = "Your Email address has been confirmed";
                return RedirectToAction("SignIn");
            }
            ViewBag.Error = result.Errors;
            return View();
        }
    }
}