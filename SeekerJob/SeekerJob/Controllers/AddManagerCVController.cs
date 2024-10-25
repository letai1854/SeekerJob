using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeekerJob.Models;
namespace SeekerJob.Controllers
{
    public class AddManagerCVController : Controller
    {
        // GET: AddManagerCV
        testdbs2425Entities  db = new testdbs2425Entities();
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