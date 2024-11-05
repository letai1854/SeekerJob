using SeekerJob.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class MenuController : Controller
    {



        testdbs2425Entities db = new testdbs2425Entities();


        public ActionResult Index()
        {

            return View();
        }
        public ActionResult GetMenu()
        {
            var tablemenus = db.tablemenus.Where(t => t.hide == true).OrderBy(t => t.arrange).ToList();


            var tablemenuparts = db.tablemenuparts.Where(t => t.hide == true).OrderBy(t => t.arrange).ToList();

            ViewBag.metaNews = "tin-tuc";
            ViewData["tablemenus"] = tablemenus;
            ViewData["tablemenuparts"] = tablemenuparts;

            return PartialView("GetMenu");
        }

        public ActionResult GetLoginPost()
        {
            object info = null;
            if (Session["candidate"]!=null)
            {
                Login login = Session["candidate"] as Login;
                info = db.InforCandidates.Where(t => t.username == login.username).FirstOrDefault();
            }
            else if (Session["employer"] != null)
            {
                Login login = Session["employer"] as Login;
                info = db.InforEmployers.Where(t => t.username == login.username).FirstOrDefault();
            }
            else if (Session["admin"] != null)
            {
                Login login = Session["admin"] as Login;
                info = db.InforCandidates.Where(t => t.username == login.username).FirstOrDefault();

            }
            //var inforcandidate = db.InforCandidates.Where(t => t.username == "totenla").FirstOrDefault();
            var tableLoginPost = db.tablemenufunctions.Where( t =>t.hide == true).OrderBy(t => t.arrange).ToList();
            ViewBag.metaprofile = "ho-so-thi-sinh";
            ViewData["profileE"] = "ho-so-cong-ty";
            ViewData["info"] = info;
            return PartialView(tableLoginPost);
        }
        
    }
}