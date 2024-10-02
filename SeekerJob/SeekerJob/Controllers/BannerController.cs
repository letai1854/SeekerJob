using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class BannerController : Controller
    {
        // GET: Banner
        MYDbS db = new MYDbS();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetBanner()
        {
            var tablemenus = db.tablebanners.Where(t => t.hide == true).OrderBy(t => t.arrange).ToList();


            var tablemenuparts = db.tablemenuparts.Where(t => t.hide == true).OrderBy(t => t.arrange).ToList();


            ViewData["tablemenus"] = tablemenus;
            ViewData["tablemenuparts"] = tablemenuparts;

            return PartialView("GetBanner");
        }
    }
}