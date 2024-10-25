using SeekerJob.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class AdminManageAdvertisementController : Controller
    {
        // GET: AdminManageAdvertisement
        testdbs2425Entities db = new testdbs2425Entities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IndexViewAdverstisement()
        {

            return View();

        }
        public ActionResult TitleManageAdv()
        {
            var tableTitleListJob = db.TitlePages
                                .Where(t => t.hide == true && t.typePage == EnumType.Type.adminadv.ToString()).OrderBy(t => t.datebegin).FirstOrDefault();
            ViewData["title"] = tableTitleListJob;
            return PartialView();
        }

        public ActionResult GetListTitle()
        {
            var tablemenus = db.tablebanners.Where(t => t.hide == true && t.typeRow == EnumType.Type.adminmange.ToString()).OrderBy(t => t.arrange).ToList();
            ViewData["list"] = tablemenus;
            return PartialView();
        }
        public ActionResult GetListAdvertise()
        {
            var tablepro = db.tablebannerparts.Where(t => t.typeRow==EnumType.Type.advproduct.ToString()).OrderBy(t => t.id).ToList();
            var tablenews = db.tablebannerparts.Where(t => t.typeRow == EnumType.Type.adminnews.ToString()).OrderBy(t => t.id).ToList();
            var tablecompany = db.tablebannerparts.Where(t => t.typeRow == EnumType.Type.advcompany.ToString()).OrderBy(t => t.id).ToList();
            ViewData["tablepro"] = tablepro;
            ViewData["tablenews"] = tablenews;
            ViewData["tablecompany"] = tablecompany;

            return PartialView();
        }
    }
}