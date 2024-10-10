using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class MenuController : Controller
    {


        MyDB db = new MyDB();


        public ActionResult Index()
        {

            return View();
        }
        public ActionResult GetMenu()
        {
            var tablemenus = db.tablemenus.Where(t => t.hide == true).OrderBy(t => t.arrange).ToList();


            var tablemenuparts = db.tablemenuparts.Where(t => t.hide == true).OrderBy(t => t.arrange).ToList();

            ViewBag.metaNews = "tin-tuc";
            ViewData["tablemenus"] = tablemenus;
            ViewData["tablemenuparts"] = tablemenuparts;

            return PartialView("GetMenu");
        }

        public ActionResult GetLoginPost()
        {
            var inforcandidate = db.InforCandidates.Where(t => t.username == "totenla").FirstOrDefault();
            var tableLoginPost = db.tablemenufunctions.Where( t =>t.hide == true).OrderBy(t => t.arrange).ToList();
            ViewBag.metaprofile = "ho-so-thi-sinh";
            ViewData["infocandidate"] = inforcandidate;
            return PartialView(tableLoginPost);
        }
        
    }
}