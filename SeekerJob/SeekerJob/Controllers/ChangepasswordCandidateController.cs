using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SeekerJob.Models;
using SeekerJob.Services;
namespace SeekerJob.Controllers
{
    public class ChangepasswordCandidateController : Controller
    {
        // GET: ChangepasswordCandidate

        testdbs2425Entities db = new testdbs2425Entities();
        //public ActionResult Index()
        //{
        //    return View();
        //}
        //public ActionResult IndexChangePasswordCandidate()
        //{
        //    return View();
        //}
        public ActionResult IndexViewChangePassword()
        {
            return View();
        }
        public ActionResult Changepassword()
        {
            return PartialView();
        }
        public ActionResult GetTitle() {
            var tableTitleListJob = db.TitlePages
                                .Where(t => t.hide == true && t.typePage == EnumType.Type.changepasswordcandidate.ToString()).OrderBy(t => t.datebegin).FirstOrDefault();
            ViewData["password"] = tableTitleListJob;
            return PartialView();
        }
        public ActionResult GetListChoosen()
        {
            
            if (Session["candidate"] is SeekerJob.Models.Login user)
            {
                var tablemenus = db.tablebanners.Where(t => t.hide == true && t.typeRow == EnumType.Type.listCandidate.ToString()).OrderBy(t => t.arrange).ToList();
                ViewData["list"] = tablemenus;
                return PartialView();
            }
            else if (Session["admin"] is SeekerJob.Models.Login admin)
            {
                var tablemenus = db.tablebanners.Where(t => t.hide == true && t.id!=20 && t.typeRow == EnumType.Type.listCandidate.ToString()).OrderBy(t => t.arrange).ToList();
                ViewData["list"] = tablemenus;
                return PartialView();

            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "User not logged in");
            }
            return new EmptyResult();
        }
        public ActionResult GetInfoChangepassword()
        {

            return PartialView();
        }



        [HttpPost]
        public JsonResult ChangePassword(FormCollection Data)
        {
            JsonResult js = new JsonResult();
            string passold = Data["passold"];
            string passnew = Data["passnew"]; // Cập nhật từ "noidung" thành "category"
            string passnewagain = Data["passnewagain"]; // Lấy thô
            Login user = null;
            if (Session["candidate"] != null)
            {
                user = Session["candidate"] as Login;
            }
            if (Session["employer"] != null)
            {
                user = Session["employer"] as Login;
            }
            if (Session["admin"]!= null)
            {
                user = Session["admin"] as Login;
            }
            if (user.password != passold)
            {
                js.Data = new
                {
                    status = "ERROR"
                };
                return Json(js, JsonRequestBehavior.AllowGet);
            }
            IO io = new IO();
            Login login = io.GetLogin(user.username);
            login.password = passnew;
            io.Save();
            Session["candidate"] = login;
            js.Data = new
            {
                status = "OK"
            };

            return Json(js, JsonRequestBehavior.AllowGet);
        }
    }
}