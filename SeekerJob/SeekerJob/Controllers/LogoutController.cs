using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class LogoutController : Controller
    {
        // GET: Logout
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session["employer"] = null;
            Session["candidate"] = null;
            Session["admin"] = null;

            return RedirectToAction("IndexLogin", "Login"); 
        }
    }
}