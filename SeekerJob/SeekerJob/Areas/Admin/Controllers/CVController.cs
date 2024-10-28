using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeekerJob.Models;
namespace SeekerJob.Areas.Admin.Controllers
{
    public class CVController : Controller
    {
        // GET: Admin/CV
        testdbs2425Entities  db = new testdbs2425Entities();
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