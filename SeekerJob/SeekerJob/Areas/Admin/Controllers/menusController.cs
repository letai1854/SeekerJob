using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Areas.Admin.Controllers
{
    public class menusController : Controller
    {
        // GET: Admin/menus
        mydatabase db = new mydatabase();
        public ActionResult Index()
        {
            var tablemenus = db.tablemenus.OrderBy(t => t.arrange).ToList();


            var tablemenuparts = db.tablemenuparts.OrderBy(t => t.arrange).ToList();

            ViewData["tablemenus"] = tablemenus;
            ViewData["tablemenuparts"] = tablemenuparts;

            return PartialView("Index");
        }
        public ActionResult Create()
        {
            return View();
        }

    }
}