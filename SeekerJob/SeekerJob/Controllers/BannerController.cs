using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class BannerController : Controller
    {


        MyDB db = new MyDB();




        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetBanner()
        {
            var tablepanners = db.tablebanners.Where(t => t.hide == true).OrderBy(t => t.arrange).ToList();
            var tablebannerparts = db.tablebannerparts.Where(t => t.hide == true).OrderBy(t => t.arrange).ToList();

            var imgbanner = (from banner in db.tablebanners
                             join  part in db.tablebannerparts on banner.id equals part.idtable
                             where banner.hide == true && part.hide == true && banner.typeRow== EnumType.Banner.imgbanner.ToString()
                             orderby part.datebegin
                             select part).ToList();
            var imgperson = (from banner in db.tablebanners
                             join part in db.tablebannerparts on banner.id equals part.idtable
                             where banner.hide == true && part.hide == true && banner.typeRow == EnumType.Banner.imgperson.ToString()
                             orderby part.datebegin
                             select part).ToList();
            ViewData["tablepanners"] = tablepanners;
            ViewData["tablebannerparts"] = tablebannerparts;
            ViewData["imgbanner"] = imgbanner; 
            ViewData["imgperson"] = imgperson;
            return PartialView("GetBanner");
        }

    }
}