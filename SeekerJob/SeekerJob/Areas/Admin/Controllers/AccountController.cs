using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeekerJob.Models;
using SeekerJob.Services;
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
        [HttpPost]
        public JsonResult EditAccount(FormCollection Data)
        {
            IO io = new IO();
            JsonResult js = new JsonResult();
            Login user = Session["admin"] as Login;

            string username = Data["user"];
            string statust = Data["status"];


                Login account = io.GetAccount(username);


            if (statust == "On")
            {
                account.status = false;
                js.Data = new
                {
                    Type = "Off",
                    status = "OK"
                };
            }
            else
            {
                account.status = true;
                js.Data = new
                {
                    Type = "On",
                    status = "OK"
                };

            }
            io.Save();
                
            


            return Json(js, JsonRequestBehavior.AllowGet);
        }
    }
}