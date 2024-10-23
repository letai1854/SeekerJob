using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class AddManagerCVController : Controller
    {
        // GET: AddManagerCV
        mydatabase db = new mydatabase();
        public ActionResult IndexCV()
        {
            return View();
        }
        public ActionResult GetListTitle()
        {
            var tablemenus = db.tablebanners.Where(t => t.hide == true && t.typeRow == EnumType.Type.adminmange.ToString()).OrderBy(t => t.arrange).ToList();
            ViewData["list"] = tablemenus;
            return PartialView();
        }
        public ActionResult GetListCV()
        {
            
            var table = db.AddCVs.ToList();

            ViewData["table"] = table;
            ViewBag.cv = "cv";
            return PartialView();
        }
    }
}