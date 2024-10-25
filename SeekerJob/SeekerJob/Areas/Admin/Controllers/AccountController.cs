using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeekerJob.Models;
namespace SeekerJob.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Admin/Account
        testdbs2425Entities db = new testdbs2425Entities();
        public ActionResult Index()
        {
            var table = db.Logins.ToList();

            ViewData["table"] = table;
            ViewBag.hoso = "ho-so-thi-sinh";
            ViewBag.hosocongty = "ho-so-cong-ty";
            return PartialView();
        }
    }
}