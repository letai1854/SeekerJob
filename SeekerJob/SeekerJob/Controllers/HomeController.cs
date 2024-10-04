using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class HomeController : Controller
    {
        MYDBS db = new MYDBS();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult GetTitileAndAmountJob1()
        {
            var title = db.TitlePages.Where(t => t.hide == true && t.typePage==EnumType.TypeTitleHome.titlepagehome1.ToString()).OrderBy(t => t.datebegin).ToList();


            var amount = db.Jobs.Count().ToString();


            ViewData["title"] = title;
            ViewData["amount"] = amount;

            return PartialView("GetTitileAndAmountJob1");
        }
        public ActionResult GetTitlePageHome2()
        {
            var title = db.TitlePages.Where(t => t.hide == true && t.typePage == EnumType.TypeTitleHome.titlepagehome2.ToString()).OrderBy(t => t.datebegin).ToList();
            ViewData["title"] = title;
            return PartialView("GetTitlePageHome2");
        }
        public ActionResult GetListAttractiveJobInHome()
        {
            var listjob = db.Jobs.Where(t => t.endday>=DateTime.Now).Take(12).ToList();
            ViewData["listjob"] = listjob;
            return PartialView("GetListAttractiveJobInHome");
        }
        public ActionResult AdvertiseHomeFirst()
        {
            var adv = db.tablebanners.Where(t => t.hide == true && t.typeRow == EnumType.Adertise.advertisehome1.ToString()).FirstOrDefault();
            var listadv = db.tablebannerparts.Where(t => t.hide == true && t.link != null && t.idtable == adv.id).Take(8).ToList();
            ViewData["listadv"] = listadv;
            return PartialView("AdvertiseHomeFirst");
        }
    }
}