using SeekerJob.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class NewsDetailController : Controller
    {
        // GET: NewsDetail

        MYDB db = new MYDB();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewNewsDetail(long id)
        {
            var newsemployer = new NewsEmployee();
            var newscandidate = new NewsCandidate();


            var infoemployee = new List<NewsEmployee>();
            var infocandidate = new List<NewsCandidate>();

            var listnews= db.News.Where(t=>t.id!=id)
                         .OrderByDescending(t => t.daypost)
                         .Take(5)
                         .ToList();

            foreach (var item in listnews)
            {
                var inforEmployer = db.InforEmployers
                                      .Where(t => t.username == item.username)
                                      .FirstOrDefault();

                if (inforEmployer != null)
                {
                    infoemployee.Add(new NewsEmployee
                    {
                        inforEmployer = inforEmployer,
                        newsinfo = item
                    });
                }

                var inforcandidate = db.InforCandidates
                                       .Where(t => t.username == item.username)
                                       .FirstOrDefault();

                if (inforcandidate != null)
                {
                    infocandidate.Add(new NewsCandidate
                    {
                        inforCandidate = inforcandidate,
                        newsinfo = item
                    });
                }
            }

            var news = db.News.Where(t => t.id == id).FirstOrDefault();

            if (news == null)
            {
                return HttpNotFound(); 
            }

            var useremployer = db.InforEmployers.Where(t => t.username == news.username).FirstOrDefault();
            var usercandidate = db.InforCandidates.Where(t => t.username == news.username).FirstOrDefault();

            if (useremployer != null)
            {
                newsemployer = new NewsEmployee()
                {
                    inforEmployer = useremployer,
                    newsinfo = news
                };
            }

            if (usercandidate != null)
            {
                newscandidate = new NewsCandidate()
                {
                    inforCandidate = usercandidate,
                    newsinfo = news
                };
            }
            ViewBag.infoemployee = infoemployee;
            ViewBag.infocandidate = infocandidate;
            ViewData["newsemployer"] = newsemployer;
            ViewData["newscandidate"] = newscandidate;
            ViewBag.metacontroller = "Chi-tiet-tin-tuc";
            return View();
        }
    }
}