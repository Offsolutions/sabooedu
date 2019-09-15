using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sabooedu.Models;
using System.Net.Mail;
using System.Net;
using System.Data.Entity;
using System.Web.Security;

namespace sabooedu.Controllers
{
    public class HomeController : Controller
    {
        dbcontext db = new dbcontext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Home()
        {
            Data dd = new Data();
            return View(dd);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult ContactUs()
        {
            

            return View(db.Settings.FirstOrDefault());
        }
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration([Bind(Include ="FirstName,LastName,Contact,Email,UserName,Password")] Registration Registrations)
        {
            if(ModelState.IsValid)
            {
                Registration reg = db.Registrations.FirstOrDefault(x => x.Email == Registrations.Email);
                if (reg == null)
                {


                    Guid activationCode = Guid.NewGuid();
                    Registrations.Date = System.DateTime.Now.AddHours(12).AddMinutes(30);
                    Registrations.EmailVerify = false;
                    Registrations.EmailCode = activationCode.ToString();
                    db.Registrations.Add(Registrations);
                    db.SaveChanges();
                    #region Email Verification
                    using (MailMessage mm = new MailMessage("offsolut@gmail.com", Registrations.Email))
                    {
                        mm.Subject = "Account Activation";
                        string body = "Hello " + Registrations.FirstName + ",";
                        body += "<br /><br />Please click the following link to activate your account";
                        body += "<br /><a href = '" + string.Format("{0}://{1}/Home/Activation/{2}", Request.Url.Scheme, Request.Url.Authority, activationCode) + "'>Click here to activate your account.</a>";
                        body += "<br /><br />Thanks";
                        mm.Body = body;
                        mm.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential("offsolut@gmail.com", "Official@123");
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);
                    }
                    TempData["Success"] = "Check Email For Verification";
                    #endregion
                    return View();
                }
                else
                {
                    TempData["Success"] = "Email Already Registered";
                    return View();
                }
            }
            return View();

        }
        public ActionResult Activation()
        {
            if (RouteData.Values["id"] != null)
            {
                Guid activationCode = new Guid(RouteData.Values["id"].ToString());
                Registration reg = db.Registrations.FirstOrDefault(x => x.EmailCode == activationCode.ToString());
                if(reg!=null)
                {
                    reg.EmailVerify = true;
                    db.Entry(reg).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Success"] = "Thank You For Email Verification";
                    return View();
                }
            }
                return View();
        }
        public ActionResult Login()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Login(Registration model, string returnUrl)
        {
            dbcontext db = new dbcontext();
            var dataItem = db.Registrations.Where(x => x.Email == model.Email && x.Password == model.Password).FirstOrDefault();
            if (dataItem != null)
            {
             
                FormsAuthentication.SetAuthCookie(dataItem.Rid.ToString(), false);

                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                         && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                
                else
                {
                    TempData["Success"] = "Login Successfully";
                    Session["User"] = dataItem.Rid;
                    return RedirectToAction("Profiles", "Profile");


                }
            }
            else
            {
                // ModelState.AddModelError("", "Invalid user/pass");
                TempData["danger"] = "Invalid user/pass";
                return View();
            }
        }
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Registration", "Home");
        }
        public ActionResult ForgetPassword()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ForgetPassword(string Email,Helper help)
        {
            Registration rr = db.Registrations.FirstOrDefault(x => x.Email == Email);
            if(rr==null)
            {
                TempData["Wrong"]= "Wrong Email";
                return View();

            }
            else
            {
                help.SendHtmlFormattedEmail(rr.Email, "New Password", "Your Password Is: " + rr.Password + "");
                return RedirectToAction("Login", "Home");
            }
            
        }
    }
}