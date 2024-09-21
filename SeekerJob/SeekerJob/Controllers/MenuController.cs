using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        MyDB db = new MyDB();
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult GetMenu()
        {
            var tablemenus = db.tablemenus.Where(t => t.hide == true).OrderBy(t => t.arrange).ToList();


            var tablemenuparts = db.tablemenuparts.Where(t => t.hide == true).OrderBy(t => t.arrange).ToList();


            ViewData["tablemenus"] = tablemenus;
            ViewData["tablemenuparts"] = tablemenuparts;

            return PartialView("GetMenu");
        }
    }
}