using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;


namespace ADFS_Login.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {

            ViewBag.ClaimsIdentity = Thread.CurrentPrincipal.Identity;
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ValidateClaim()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ValidateClaim(FormCollection formData)
        {
            var claimname = formData["claimname"];
            var claimvalue = formData["claimvalue"];
            var userClaims = Thread.CurrentPrincipal.Identity;
            var claimsIdentity = userClaims as ClaimsIdentity;
            
            // Access claim
            try
            {
                if (claimsIdentity != null)
                {
                    var isValid = claimsIdentity.Claims.FirstOrDefault(x => x.Type == claimname && x.Value == claimvalue);
                    ViewBag.Result = isValid != null ? "Claim matched " : "Claim data mismatched ";
                    ViewBag.isValidClaim = isValid != null;
                }
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
            }
            return View(formData);
        }
    }
}