using SeekerJob.Models;
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
    public class CVController : Controller
    {
        // GET: Admin/CV
        testdbs2425Entities db = new testdbs2425Entities();
        public ActionResult Index()
        {
            var table = db.AddCVs.ToList();

            ViewData["table"] = table;
            ViewBag.cv = "cv";
            return View();
        }
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public JsonResult CreateCV(FormCollection Data)
        {
            string title = Data["tieude"];
            string shortbrief = Data["noidung"];
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
            AddCV addCV = new AddCV
            {
                tilteBig = title,
                titleMall = shortbrief,
                image = uniqueFileName,
                meta = meta,
                typeCv = "Tiêu chuẩn"

            };
            io.AddObject(addCV);
            io.Save();
            js.Data = new
            {
                status = "OK"
            };
            return Json(js, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(int id)
        {
            var inforCV = db.AddCVs.Where(t => t.id == id).FirstOrDefault();
            ViewBag.cv = inforCV;
            return PartialView();
        }
    }
}