using BlogOperations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace BlogPlatform.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            BlogOperator oper = new BlogOperator();
            Claim oidClaim = ClaimsPrincipal.Current.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier");
            bool hasBlog = oidClaim != null ? oper.UserHasBlog(oidClaim.Value) : false;

            if (hasBlog)
            {
                ViewBag.HasBlog = true;
            }
            else
            {
                ViewBag.HasBlog = false;
            }

            return View();
        }

        // You can use the PolicyAuthorize decorator to execute a certain policy if the user is not already signed into the app.
        [Authorize]
        public ActionResult Claims()
        {
            Claim displayName = ClaimsPrincipal.Current.FindFirst(ClaimsPrincipal.Current.Identities.First().NameClaimType);
            ViewBag.DisplayName = displayName != null ? displayName.Value : string.Empty;
            return View();
        }

        public ActionResult Error(string message)
        {
            ViewBag.Message = message;

            return View("Error");
        }
    }
}