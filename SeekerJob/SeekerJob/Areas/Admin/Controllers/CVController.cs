using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Areas.Admin.Controllers
{
    public class CVController : Controller
    {
        // GET: Admin/CV
        mydatabase db = new mydatabase();
        public ActionResult Index()
        {
            var table = db.AddCVs.ToList();

            ViewData["table"] = table;
            ViewBag.cv = "cv";
            return View();
        }
        public ActionResult Create()
        {

            return View();
        }
    }
}