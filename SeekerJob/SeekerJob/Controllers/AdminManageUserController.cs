using SeekerJob.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class AdminManageUserController : Controller
    {
        // GET: AdminManageUser
        mydb db = new mydb();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexUser()
        {
            return View();
        }
        public ActionResult TitleManageUser()
        {
            var tableTitleListJob = db.TitlePages
                                .Where(t => t.hide == true && t.typePage == EnumType.Type.adminusers.ToString()).OrderBy(t => t.datebegin).FirstOrDefault();
            ViewData["title"] = tableTitleListJob;
            return PartialView();
        }

        public ActionResult GetListTitle()
        {
            var tablemenus = db.tablebanners.Where(t => t.hide == true && t.typeRow == EnumType.Type.adminmange.ToString()).OrderBy(t => t.arrange).ToList();
            ViewData["list"] = tablemenus;
            return PartialView();
        }
        public ActionResult GetListUser()
        {
            var table = db.Logins.ToList();

            ViewData["table"] = table;
            ViewBag.hoso = "ho-so-thi-sinh";
            ViewBag.hosocongty = "ho-so-cong-ty";
            return PartialView();
        }
    }
}