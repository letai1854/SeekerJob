using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class HomeController : Controller
    {
        MYdBS db = new MYdBS();

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
        public ActionResult GetListAttractiveJobInHome()
        {
            var listjob = db.Jobs.Where(t => t.endday>=DateTime.Now).Take(12).ToList();
            ViewData["listjob"] = listjob;
            return PartialView("GetListAttractiveJobInHome");
        }
    }
}