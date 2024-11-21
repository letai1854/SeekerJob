using System;
using System.Collections.Generic;
using System.Linq;
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
            var tablemenus = db.tablebanners.Where(t => t.hide == true && t.typeRow == EnumType.Type.listCandidate.ToString()).OrderBy(t => t.arrange).ToList();
            ViewData["list"] = tablemenus;
            return PartialView();
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
            Login user = Session["candidate"] as Login;
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