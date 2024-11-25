using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeekerJob.Models;
using SeekerJob.Services;
namespace SeekerJob.Controllers
{
    public class ChangePassworCompanyController : Controller
    {
        // GET: ChangePassworCompany

        testdbs2425Entities db = new testdbs2425Entities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IndexViewChangePassword()
        {
            return View();
        }
        public ActionResult TitleChangePassword()
        {
            var tableTitleListJob = db.TitlePages
                                .Where(t => t.hide == true && t.typePage == EnumType.Type.changepasswordcompany.ToString()).OrderBy(t => t.datebegin).FirstOrDefault();
            ViewData["title"] = tableTitleListJob;
            return PartialView();
        }

        public ActionResult GetListTitle()
        {
            var tablemenus = db.tablebanners.Where(t => t.hide == true && t.typeRow == EnumType.Type.list.ToString()).OrderBy(t => t.arrange).ToList();
            ViewData["list"] = tablemenus;
            return PartialView();
        }
        public ActionResult GetInfoChangePassword()
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
            Login user = Session["employer"] as Login;
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
            Session["employer"] = login;
            js.Data = new
            {
                status = "OK"
            };

            return Json(js, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ChangePasswordForgot(FormCollection Data)
        {
            JsonResult js = new JsonResult();

            try
            {
                string passnew = Data["passnew"];
                string passnewagain = Data["passnewagain"];

                // Lấy username từ session đã lưu
                string username = Session["Username"]?.ToString();

                if (string.IsNullOrEmpty(username))
                {
                    js.Data = new { status = "ERROR", message = "Phiên làm việc đã hết hạn" };
                    return Json(js, JsonRequestBehavior.AllowGet);
                }

                if (passnew != passnewagain)
                {
                    js.Data = new { status = "ERROR", message = "Mật khẩu xác nhận không khớp" };
                    return Json(js, JsonRequestBehavior.AllowGet);
                }

                IO io = new IO();
                Login login = io.GetLogin(username);

                if (login == null)
                {
                    js.Data = new { status = "ERROR", message = "Không tìm thấy tài khoản" };
                    return Json(js, JsonRequestBehavior.AllowGet);
                }

                // Cập nhật mật khẩu mới
                login.password = passnew;
                io.Save();

                // Xóa session
                Session.Remove("Username");

                js.Data = new { status = "OK" };
                return Json(js, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                js.Data = new { status = "ERROR", message = "Có lỗi xảy ra: " + ex.Message };
                return Json(js, JsonRequestBehavior.AllowGet);
            }
        }
    }

}