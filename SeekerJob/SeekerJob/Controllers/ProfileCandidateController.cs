using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class ProfileCandidateController : Controller
    {
        // GET: ProfileCandidate
        MySql db = new MySql();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewProfileCandidate(string id)
        {
            var info = db.InforCandidates.Where(t => t.username == id).FirstOrDefault();
            var table = db.News.Where(t => t.username == id).OrderByDescending(t => t.daypost).ToList();
            ViewData["info"] = info;
            ViewData["table"] = table;
            @ViewBag.metanews = "Chi-tiet-tin-tuc";
            return View();
        }
    }
}