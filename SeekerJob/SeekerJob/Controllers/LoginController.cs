using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeekerJob.Models;
using SeekerJob.Services;
namespace SeekerJob.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        testdbs2425Entities db = new testdbs2425Entities();
        public ActionResult IndexLogin()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CheckLoginCandidate(FormCollection collection)
        {
            string uid = collection["uid"];
            string pwd = collection["pwd"];
            JsonResult jr = new JsonResult();

            
            var item = db.Logins.Where(t=>t.username==uid && t.password==pwd && t.typeRow=="Thí sinh").FirstOrDefault();
            var admin = db.Logins.Where(t => t.username == uid && t.password == pwd && t.typeRow == "Admin").FirstOrDefault();
            if (item != null)
            {
                Session["candidate"] = item;
                //Session.Timeout = 5;
                jr.Data = new
                {
                    status = "OK"
                };
            }
            else if (admin != null)
            {
                Session["admin"] = admin;
                //Session.Timeout = 5;
                jr.Data = new
                {
                    status = "OK"
                };
            }
            else
            {
                jr.Data = new
                {
                    status = "F"
                };
            }


            return Json(jr, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult CheckLoginEmployer(FormCollection collection)
        {
            string uid = collection["uid"];
            string pwd = collection["pwd"];
            JsonResult jr = new JsonResult();

            var admin = db.Logins.Where(t => t.username == uid && t.password == pwd && t.typeRow == "Admin").FirstOrDefault();
            var item = db.Logins.Where(t => t.username == uid && t.password == pwd && t.typeRow == "Tuyển dụng").FirstOrDefault();
            if (item != null)
            {
                Session["employer"] = item;
                //Session.Timeout = 5;
                jr.Data = new
                {
                    status = "OK"
                };
            }
            else if (admin != null)
            {
                Session["admin"] = admin;
                //Session.Timeout = 5;
                jr.Data = new
                {
                    status = "OK"
                };
            }
            else
            {
                jr.Data = new
                {
                    status = "F"
                };
            }


            return Json(jr, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SendVerificationEmail(string email)
        {
            try
            {
                InforCandidate userC = null;
                InforEmployer userE = null;
                IO io = new IO();
                userC = io.GetInforCandidate(email);
                userE = io.GetInforEmployer(email);
                
                if (userC == null && userE == null)
                {
                    return Json(new { status = "ERROR", message = "Email không tồn tại trong hệ thống" });
                }

                // Tạo mã xác thực ngẫu nhiên 6 chữ số
                Random random = new Random();
                string verificationCode = random.Next(100000, 999999).ToString();

                // Lưu vào session
                Session["VerificationCode"] = verificationCode;
                Session["VerificationEmail"] = email;
                Session["VerificationExpiry"] = DateTime.Now.AddMinutes(2); // Thời hạn 2 phút

                // Gửi email
                if (userE != null)
                {
                    Session["Username"] = userE.username;
                    Gmail mailer = new Gmail
                    {
                        To = email,
                        Subject = "Xác minh khôi phục mật khẩu của bạn",

                        Body = $@"Kính gửi {userE.namecompany},
Trang Web Tìm kiếm việc làm T&T đã nhận được yêu cầu khôi phục mật khẩu cho tài khoản của bạn.

Mã xác thực của bạn là: <strong>{verificationCode}</strong>

Mã này sẽ hết hạn sau 2 phút.

Nếu bạn không yêu cầu khôi phục mật khẩu, vui lòng bỏ qua email này."
                    };
                    mailer.SendMail();

                }
                else if(userC != null)
                {
                    Session["Username"] = userC.username;

                    Gmail mailer = new Gmail
                    {
                        To = email,
                        Subject = "Xác minh khôi phục mật khẩu của bạn",

                        Body = $@"Kính gửi {userC.name},
Trang Web Tìm kiếm việc làm T&T đã nhận được yêu cầu khôi phục mật khẩu cho tài khoản của bạn.

Mã xác thực của bạn là:  {verificationCode}

Mã này sẽ hết hạn sau 2 phút.

Nếu bạn không yêu cầu khôi phục mật khẩu, vui lòng bỏ qua email này."
                    };
                    mailer.SendMail();

                }
                return Json(new { status = "OK" });

            }
            catch (Exception ex)
            {
                return Json(new { status = "ERROR", message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        [HttpPost]
        public JsonResult VerifyCode(string code)
        {
            try
            {
                string savedCode = Session["VerificationCode"]?.ToString();
                string savedEmail = Session["VerificationEmail"]?.ToString();
                DateTime? expiry = Session["VerificationExpiry"] as DateTime?;

                if (string.IsNullOrEmpty(savedCode) || string.IsNullOrEmpty(savedEmail) || !expiry.HasValue)
                {
                    return Json(new { status = "ERROR", message = "Phiên làm việc đã hết hạn" });
                }

                if (DateTime.Now > expiry.Value)
                {
                    // Xóa session khi hết hạn
                    Session.Remove("VerificationCode");
                    Session.Remove("VerificationEmail");
                    Session.Remove("VerificationExpiry");
                    return Json(new { status = "ERROR", message = "Mã xác thực đã hết hạn" });
                }

                if (code != savedCode)
                {
                    return Json(new { status = "ERROR", message = "Mã xác thực không chính xác" });
                }

                return Json(new { status = "OK", email = savedEmail });
            }
            catch (Exception ex)
            {
                return Json(new { status = "ERROR", message = "Có lỗi xảy ra: " + ex.Message });
            }
        }


    }


}