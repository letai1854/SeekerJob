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
                username = "Admin01",
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
                username = "Admin01",
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
    }
}