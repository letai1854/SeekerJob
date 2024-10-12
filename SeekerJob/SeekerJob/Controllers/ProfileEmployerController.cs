using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class ProfileEmployerController : Controller
    {
        // GET: ProfileEmployer

        MYDB db = new MYDB();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewProfile(string username)
        {
            var info = db.InforEmployers.Where(t=>t.username == username).FirstOrDefault();
            ViewData["info"] = info;
            return View();
        }
        public ActionResult ViewProfileEmployer(string id)
        {
            var info = db.InforEmployers.Where(t => t.username == id).FirstOrDefault();
            var table = db.Jobs.Where(t => t.username == id && t.endday>=DateTime.Now).OrderByDescending(t=>t.startday).ToList();
            ViewData["info"] = info;
            ViewData["table"] = table;
            return View();
        }
    }
}