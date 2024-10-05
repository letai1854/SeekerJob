using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class ManagerNewsController : Controller
    {
        mydbs db = new mydbs();
        public ActionResult IndexManagerNews()
        {
            return View();
        }
        public ActionResult TitleManagerNews()
        {
            var tableTitleListJob = db.TitlePages
                                .Where(t => t.hide == true && t.typePage == EnumType.Type.titlepageManagerNews.ToString()).OrderBy(t => t.datebegin).FirstOrDefault();

            return PartialView(tableTitleListJob);
        }
        public ActionResult GetImageNameCompany()
        {
            var tablemenus = db.InforEmployers.Where(t => t.username == "tuanta").FirstOrDefault();
            return PartialView(tablemenus);
        }
        public ActionResult GetListTitle()
        {
            var tablemenus = db.tablebanners.Where(t => t.hide == true && t.typeRow == EnumType.Type.listNews.ToString()).OrderBy(t => t.arrange).ToList();
            return PartialView(tablemenus);
        }
        public ActionResult GetListNews()
        {
            var table = db.News.Where(t => t.username == "tuanta").OrderBy(t => t.daypost).ToList();
            ViewBag.tintuc = "Chi-tiet-tin-tuc";
            ViewBag.suatintuc = "sua-tin-tuc";
            return PartialView(table);
        }
    }
}