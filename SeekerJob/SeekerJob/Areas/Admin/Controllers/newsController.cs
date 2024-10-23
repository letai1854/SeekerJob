using SeekerJob.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Areas.Admin.Controllers
{
    public class newsController : Controller
    {
        // GET: Admin/news
        mydatabase db = new mydatabase();
        public ActionResult Index()
        {
            var table = db.News.OrderBy(t => t.daypost).ToList();
            var listnewscandidate = new List<NewsCandidate>();
            var listnewsemployer = new List<NewsEmployee>();

            foreach (var item in table)
            {
                var candidate = db.InforCandidates.Where(t => t.username == item.username).FirstOrDefault();
                var employee = db.InforEmployers.Where(t => t.username == item.username).FirstOrDefault();
                if (candidate != null)
                {
                    listnewscandidate.Add(new NewsCandidate
                    {
                        inforCandidate = candidate,
                        newsinfo = item
                    });
                }
                if (employee != null)
                {
                    listnewsemployer.Add(new NewsEmployee
                    {
                        inforEmployer = employee,
                        newsinfo = item
                    });
                }
            }
            ViewData["listnewscandidate"] = listnewscandidate;
            ViewData["listnewsemployer"] = listnewsemployer;
            ViewBag.tintuc = "Chi-tiet-tin-tuc";
            ViewBag.suatintuc = "Sua-tin-tuc";
            return PartialView("Index");
        }
        public ActionResult Create()
        {

            return View();
        }
    }
}