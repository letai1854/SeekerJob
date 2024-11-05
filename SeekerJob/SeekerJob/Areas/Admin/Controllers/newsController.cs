using SeekerJob.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeekerJob.Models;
using SeekerJob.Services;
using System.IO;
namespace SeekerJob.Areas.Admin.Controllers
{
    public class newsController : Controller
    {
        // GET: Admin/news
        testdbs2425Entities db = new testdbs2425Entities();
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

        public ActionResult Edit(int id)
        {
            var inforNews = db.News.Where(t => t.id == id).FirstOrDefault();
            ViewBag.news= inforNews;
            return PartialView();
        }
        
        [HttpPost]
        public JsonResult testnews(FormCollection Data)
        {

            string description = Data["mota"];
            string title = Data["tieude"];
            string shortbrief = Data["noidung"];
            string email = Data["email"];
            string phone = Data["phone"];
            string day = Data["day"];
            string meta = Data["meta"];
            string img = Data["anh"];

            var file = Request.Files["anh"];
            string uniqueFileName = null;

            if (file != null && file.ContentLength > 0)
            {
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/ContentImage/images"), uniqueFileName); // Check this path!
                file.SaveAs(path);
            }

            IO io = new IO();
            JsonResult js = new JsonResult();
            Login user = Session["admin"] as Login;

            News news = new News()
            {
                username = user.username,
                title = title,
                image = uniqueFileName,
                meta = meta,
                daypost = DateTime.Now,
                shortbref = shortbrief,
                description = description,
            };
            io.AddObject(news);
            io.Save();
            js.Data = new
            {
                status = "OK"
            };
            return Json(js, JsonRequestBehavior.AllowGet);
        }
    



            [HttpPost]
        public JsonResult CreateNews(FormCollection Data)
        {
            string description = HttpUtility.UrlDecode(Data["mota"]);
            string title = Data["tieude"];
            string shortbrief = Data["noidung"];
            string email = Data["email"];
            string phone = Data["phone"];
            string day = Data["day"];
            string meta = Data["meta"];
            string img = Data["anh"];

            var file = Request.Files["anh"];
            string uniqueFileName = null;

            if (file != null && file.ContentLength > 0)
            {
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/ContentImage/images"), uniqueFileName); // Check this path!
                file.SaveAs(path);
            }

            IO io = new IO();
            JsonResult js = new JsonResult();
            Login user = Session["admin"] as Login;

            News news = new News()
            {
                username = user.username,
                title = title,
                image = uniqueFileName,
                meta = meta,
                daypost = DateTime.Now,
                shortbref = shortbrief,
                description = description,
            };
            io.AddObject(news);
            io.Save();
            js.Data = new
            {
                status = "OK"
            };
            return Json(js, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditNews(FormCollection Data)
        {
            IO io = new IO();
            JsonResult js = new JsonResult();
            Login user = Session["admin"] as Login;

            string description = HttpUtility.UrlDecode(Data["mota"]);
            int id = Convert.ToInt32(Data["id"]);
            string title = Data["tieude"];
            string shortbrief = Data["noidung"];
            string day = Data["day"];
            string meta = Data["meta"];
            var file = Request.Files["anh"];

            if (file!= null)
            {
                string uniqueFileName = null;

                if (file != null && file.ContentLength > 0)
                {
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/ContentImage/images"), uniqueFileName); // Check this path!
                    file.SaveAs(path);
                }
                News news = io.GetNews(id);



                    news.title = title;
                    news.description= description;
                    news.shortbref = shortbrief;
                    news.image = uniqueFileName;
                    news.meta = meta;
                    news.daypost = DateTime.Now;
               
                    io.Save();
                js.Data = new
                {
                    status = "OK"
                };
            }
            else
            {
                News news = io.GetNews(id);
                news.title = title;
                news.description = description;
                news.shortbref = shortbrief;
                news.meta = meta;
                news.daypost = DateTime.Now;
                io.Save();
                js.Data = new
                {
                    status = "OK"
                };
            }
            
            return Json(js, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteNews(FormCollection Data)
        {
            int id = Convert.ToInt32(Data["id"]);
            IO io = new IO();
            JsonResult js = new JsonResult();
            Login user = Session["admin"] as Login;

            bool checkDeleteNews = io.DeleteNews(id);
            if(checkDeleteNews)
            {
                io.Save();
                js.Data = new
                {
                    status = "OK"
                };
            }
            return Json(js, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewNewsDetail(long id)
        {
            var newsemployer = new NewsEmployee();
            var newscandidate = new NewsCandidate();


            var infoemployee = new List<NewsEmployee>();
            var infocandidate = new List<NewsCandidate>();

            var listnews = db.News.Where(t => t.id != id)
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