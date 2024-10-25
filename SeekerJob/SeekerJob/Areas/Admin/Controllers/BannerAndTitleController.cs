using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeekerJob.Models;
namespace SeekerJob.Areas.Admin.Controllers
{
    public class BannerAndTitleController : Controller
    {
        // GET: Admin/BannerAndTitle
        testdbs2425Entities db = new testdbs2425Entities();
        public ActionResult Index()
        {
            var banner = db.tablebanners
                .Where(t => !string.IsNullOrEmpty(t.typeRow) && t.typeRow!="" && t.typeRow!=null &&t.typeRow!=EnumType.TypeTitleHome.headercompany.ToString()
                         && t.typeRow != EnumType.Adertise.advertisehome2.ToString()
                         && t.typeRow != EnumType.Adertise.advertisehome1.ToString()
                         && t.typeRow != EnumType.Type.adminmange.ToString())
                .OrderBy(t => t.id)
                .ToList();
            var titlepage = db.TitlePages.OrderBy(t=>t.datebegin).ToList();


            var tablepannerpart = db.tablebannerparts.OrderBy(t=>t.id).ToList();

            ViewData["banner"] = banner;
            ViewData["tablepannerpart"] = tablepannerpart;
            ViewData["titlepage"] = titlepage;
            return PartialView();
        }
    }
}