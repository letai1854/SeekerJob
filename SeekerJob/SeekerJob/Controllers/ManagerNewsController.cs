using SeekerJob.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using SeekerJob.Services;
using System.Web.WebPages;
using System.IO;

namespace SeekerJob.Controllers
{
    public class ManagerNewsController : Controller
    {


        testdbs2425Entities db = new testdbs2425Entities();

        public ActionResult IndexManagerNews()
        {
            return View();
        }
        public ActionResult TitleManagerNews()
        {
            var tableTitleListJob = db.TitlePages
                                .Where(t => t.hide == true && t.typePage == EnumType.Type.titlepageManagerNews.ToString()).OrderBy(t => t.datebegin).FirstOrDefault();

            return PartialView(tableTitleListJob);
        }
        public ActionResult GetImageNameCompany()
        {
            var tablemenus = db.InforEmployers.Where(t => t.username == "tuanta").FirstOrDefault();
            return PartialView(tablemenus);
        }
        public ActionResult GetListTitle()
        {
            var tablemenus = db.tablebanners.Where(t => t.hide == true && t.typeRow == EnumType.Type.listNews.ToString()).OrderBy(t => t.arrange).ToList();
            return PartialView(tablemenus);
        }
        public ActionResult GetListNews()
        {
            var table = db.News.Where(t => t.username == "tuanta").OrderBy(t => t.daypost).ToList();
            ViewBag.tintuc = "Chi-tiet-tin-tuc";
            ViewBag.suatintuc = "sua-tin-tuc";
            return PartialView(table);
        }


        [HttpPost]
        public JsonResult CreateNews(FormCollection Data)
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

            News news = new News() {
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
    }
}