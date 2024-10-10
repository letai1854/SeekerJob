using SeekerJob.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class AdminManageNewsController : Controller
    {
        // GET: AdminManageNews
        mydb db = new mydb();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IndexViewNews()
        {
            return View();
        }
        public ActionResult TitleManageNews()
        {
            var tableTitleListJob = db.TitlePages
                                .Where(t => t.hide == true && t.typePage == EnumType.Type.adminnews.ToString()).OrderBy(t => t.datebegin).FirstOrDefault();
            ViewData["title"] = tableTitleListJob;
            return PartialView();
        }

        public ActionResult GetListTitle()
        {
            var tablemenus = db.tablebanners.Where(t => t.hide == true && t.typeRow == EnumType.Type.adminmange.ToString()).OrderBy(t => t.arrange).ToList();
            ViewData["list"] = tablemenus;
            return PartialView();
        }
        public ActionResult GetListNews()
        {
            var table = db.News.OrderBy(t => t.daypost).ToList();
            var listnewscandidate = new List<NewsCandidate>();
            var listnewsemployer = new List<NewsEmployee>();

            foreach(var item in table)
            {
                var candidate = db.InforCandidates.Where(t=>t.username==item.username).FirstOrDefault();
                var employee = db.InforEmployers.Where(t=>t.username == item.username).FirstOrDefault();
                if(candidate != null)
                {
                    listnewscandidate.Add(new NewsCandidate
                    {
                        inforCandidate = candidate,
                        newsinfo = item
                    }) ;
                }
                if (employee != null)
                {
                    listnewsemployer.Add(new NewsEmployee { 
                        inforEmployer= employee,
                        newsinfo = item
                    });
                }
            }
            ViewData["listnewscandidate"] = listnewscandidate;
            ViewData["listnewsemployer"] = listnewsemployer;
            ViewBag.tintuc = "Chi-tiet-tin-tuc";
            ViewBag.suatintuc = "sua-tin-tuc";
            return PartialView();
        }
    }
}