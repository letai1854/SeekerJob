using SeekerJob.Models;
using SeekerJob.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class PostNewsController : Controller
    {
        // GET: PostNews


        testdbs2425Entities db = new testdbs2425Entities();


        public ActionResult IndexPostNews()
        {
            return View();
        }
        public ActionResult GetImageNameCompany()
        {
            var tablemenus = db.InforEmployers.Where(t => t.username == "tuanta").FirstOrDefault();
            return PartialView(tablemenus);
        }
        public ActionResult TitlePostNews()
        {
            var tableTitleListJob = db.TitlePages
                                .Where(t => t.hide == true && t.typePage == EnumType.Type.titlepagePostNews.ToString()).OrderBy(t => t.datebegin).FirstOrDefault();

            return PartialView(tableTitleListJob);
        }
        public ActionResult GetListTitle()
        {
            var tablemenus = db.tablebanners
      .Where(t => t.hide == true &&
                 t.typeRow == EnumType.Type.listNews.ToString() &&
                 t.id != 26)  // Thêm điều kiện này
      .OrderBy(t => t.arrange)
      .ToList();
            return PartialView(tablemenus);
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
            Login user = null;
            if (Session["admin"] != null)
            {
                user = Session["admin"] as Login;
            }
            if (Session["candiate"] != null)
            {
                user = Session["candidate"] as Login;
            }
            if (Session["employer"] != null)
            {
                user = Session["employer"] as Login;
            }
            IO io = new IO();
            JsonResult js = new JsonResult();

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
        public JsonResult DeleteNews(FormCollection Data)
        {
            int id = Convert.ToInt32(Data["id"]);
            IO io = new IO();
            JsonResult js = new JsonResult();
            

            bool checkDeleteNews = io.DeleteNews(id);
            if (checkDeleteNews)
            {
                io.Save();
                js.Data = new
                {
                    status = "OK"
                };
            }
            return Json(js, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetNewsDetailById(int id)
        {
            var news = db.News.FirstOrDefault(t => t.id == id);
            ViewData["news"] = news;
            return PartialView("GetNewsDetailById");
        }

        public ActionResult GetNewsDetail()
        {
            return PartialView("_EditNewsPartial");
        }



        [HttpPost]
        public JsonResult UpdateNews(FormCollection Data)
        {
            string description = HttpUtility.UrlDecode(Data["mota"]);
            string title = Data["tieude"];
            string shortbrief = Data["noidung"];
            string email = Data["email"];
            string phone = Data["phone"];
            string day = Data["day"];
            string meta = Data["meta"];
            string img = Data["anh"];
            int id = Convert.ToInt32(Data["id"]);
            var file = Request.Files["anh"];
            string uniqueFileName = null;
            Login user = null;
            if (Session["admin"] != null)
            {
                user = Session["admin"] as Login;
            }
            if (Session["candiate"] != null)
            {
                user = Session["candidate"] as Login;
            }
            if (Session["employer"] != null)
            {
                user = Session["employer"] as Login;
            }
            if (file != null && file.ContentLength > 0)
            {
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/ContentImage/images"), uniqueFileName); // Check this path!
                file.SaveAs(path);
            }

            IO io = new IO();
            News info = io.GetNews(id);
            JsonResult js = new JsonResult();
            info.username = user.username;
            info.title = title;
            if (uniqueFileName != null)
            {
                info.image = uniqueFileName;
            }
            info.meta = meta;
            info.shortbref = shortbrief;
            info.description  = description;
            io.Save();
            js.Data = new
            {
                status = "OK"
            };
            return Json(js, JsonRequestBehavior.AllowGet);
        }


    }

}