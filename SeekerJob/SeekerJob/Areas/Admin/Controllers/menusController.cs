using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeekerJob.Models;
using SeekerJob.Services;
namespace SeekerJob.Areas.Admin.Controllers
{
    public class menusController : Controller
    {
        // GET: Admin/menus
        testdbs2425Entities db = new testdbs2425Entities();
        public ActionResult Index()
        {
            var tablemenus = db.tablemenus.OrderBy(t => t.arrange).ToList();


            var tablemenuparts = db.tablemenuparts.OrderBy(t => t.arrange).ToList();

            ViewData["tablemenus"] = tablemenus;
            ViewData["tablemenuparts"] = tablemenuparts;

            return PartialView("Index");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public JsonResult EditMenusPart(FormCollection Data)
        {
            IO io = new IO();
            JsonResult js = new JsonResult();
            //Login user = Session["admin"] as Login;

            int id = Convert.ToInt32(Data["id"]);
            string statust = Data["status"];


            tablemenupart table = io.GetTablemenupart(id);

            if (statust == "True" || statust == "true")
            {
                table.hide = false;
                js.Data = new
                {
                    Type = "false",
                    status = "OK"
                };
            }
            else
            {
                table.hide = true;
                js.Data = new
                {
                    Type = "true",
                    status = "OK"
                };

            }
            io.Save();




            return Json(js, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditMenus(FormCollection Data)
        {
            IO io = new IO();
            JsonResult js = new JsonResult();
            //Login user = Session["admin"] as Login;

            int id = Convert.ToInt32(Data["id"]);
            string statust = Data["status"];


            tablemenu table = io.GetTablemenu(id);

            if (statust == "True" || statust == "true")
            {
                table.hide = false;
                js.Data = new
                {
                    Type = "false",
                    status = "OK"
                };
            }
            else
            {
                table.hide = true;
                js.Data = new
                {
                    Type = "true",
                    status = "OK"
                };

            }
            io.Save();




            return Json(js, JsonRequestBehavior.AllowGet);
        }

    }
}