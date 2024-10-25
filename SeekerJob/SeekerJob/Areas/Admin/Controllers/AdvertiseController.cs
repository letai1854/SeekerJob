using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeekerJob.Models;
namespace SeekerJob.Areas.Admin.Controllers
{
    public class AdvertiseController : Controller
    {
        // GET: Admin/Advertise
        testdbs2425Entities db = new testdbs2425Entities();
        public ActionResult Index()
        {
            var tablepro = db.tablebannerparts.Where(t => t.typeRow == EnumType.Type.advproduct.ToString()).OrderBy(t => t.id).ToList();
            var tablenews = db.tablebannerparts.Where(t => t.typeRow == EnumType.Type.adminnews.ToString()).OrderBy(t => t.id).ToList();
            var tablecompany = db.tablebannerparts.Where(t => t.typeRow == EnumType.Type.advcompany.ToString()).OrderBy(t => t.id).ToList();
            ViewData["tablepro"] = tablepro;
            ViewData["tablenews"] = tablenews;
            ViewData["tablecompany"] = tablecompany;
            return PartialView();
        }
    }
}