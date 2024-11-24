using SeekerJob.Models;
using SeekerJob.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class MenuController : Controller
    {



        testdbs2425Entities db = new testdbs2425Entities();


        public ActionResult Index()
        {

            return View();
        }
        public ActionResult GetMenu()
        {
            var tablemenus = db.tablemenus.Where(t => t.hide == true).OrderBy(t => t.arrange).ToList();


            var tablemenuparts = db.tablemenuparts.Where(t => t.hide == true).OrderBy(t => t.arrange).ToList();

            ViewBag.metaNews = "tin-tuc";
            ViewData["tablemenus"] = tablemenus;
            ViewData["tablemenuparts"] = tablemenuparts;

            return PartialView("GetMenu");
        }

        public ActionResult GetLoginPost()
        {
            object info = null;
            if (Session["candidate"]!=null)
            {
                Login login = Session["candidate"] as Login;
                info = db.InforCandidates.Where(t => t.username == login.username).FirstOrDefault();
            }
            else if (Session["employer"] != null)
            {
                Login login = Session["employer"] as Login;
                info = db.InforEmployers.Where(t => t.username == login.username).FirstOrDefault();
            }
            else if (Session["admin"] != null)
            {
                Login login = Session["admin"] as Login;
                info = db.InforCandidates.Where(t => t.username == login.username).FirstOrDefault();

            }
            //var inforcandidate = db.InforCandidates.Where(t => t.username == "totenla").FirstOrDefault();
            var tableLoginPost = db.tablemenufunctions.Where( t =>t.hide == true).OrderBy(t => t.arrange).ToList();
            ViewBag.metaprofile = "ho-so-thi-sinh";
            ViewData["profileE"] = "ho-so-cong-ty";
            ViewData["info"] = info;
            return PartialView(tableLoginPost);
        }

        public void AddAccount(string email, string phone, string address, string name)
        {
            try
            {
                IO io = new IO();
                InforCandidate info = new InforCandidate
                {
                    email = email?.Trim(),
                    phone = phone != null ? Convert.ToInt32(phone.Trim()) : 0,
                    adrress = address?.Trim(),
                    name = name?.Trim(),
                    image = "facebook.jpg",
                    username = email // Thêm username để liên kết với bảng Login
                };
                io.AddObject(info);
                io.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm thông tin ứng viên: " + ex.Message);
            }
        }

        [HttpPost]
        public JsonResult SignUp(string username, string password, string email,
                        string phone, string address, string name)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    // Validate input
                    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) ||
                        string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone))
                    {
                        return Json(new { status = "ERROR", message = "Vui lòng nhập đầy đủ thông tin" });
                    }

                    // Chuyển đổi phone thành int trước khi query
                    int phoneNumber;
                    if (!int.TryParse(phone.Trim(), out phoneNumber))
                    {
                        return Json(new { status = "ERROR", message = "Số điện thoại không hợp lệ" });
                    }

                    // Kiểm tra tài khoản đã tồn tại
                    var existingUser = db.Logins.FirstOrDefault(x => x.username == username);
                    if (existingUser != null)
                    {
                        return Json(new { status = "EXIST", message = "Tài khoản đã tồn tại" });
                    }

                    // Kiểm tra email đã tồn tại
                    var existingEmail = db.InforCandidates.FirstOrDefault(x => x.email == email);
                    if (existingEmail != null)
                    {
                        return Json(new { status = "EXIST", message = "Email đã được sử dụng" });
                    }

                    // Kiểm tra số điện thoại đã tồn tại
                    var existingPhone = db.InforCandidates.FirstOrDefault(x => x.phone == phoneNumber);
                    if (existingPhone != null)
                    {
                        return Json(new { status = "EXIST", message = "Số điện thoại đã được sử dụng" });
                    }

                    // Tạo tài khoản mới
                    Login newUser = new Login
                    {
                        username = username.Trim(),
                        password = password.Trim(),
                        status = true,
                        typeRow = "Thí sinh",
                    };

                    // Tạo thông tin ứng viên
                    InforCandidate info = new InforCandidate
                    {
                        email = email.Trim(),
                        phone = phoneNumber,
                        adrress = address?.Trim(),
                        name = name?.Trim(),
                        image = "facebook.jpg",
                        username = username,
                        skill="",
                        qualification="",
                        experience=null,
                        jobcategory="",
                        gender="",
                        offercurrent=null,
                        description="",
                    };

                    // Lưu cả hai đối tượng
                    db.Logins.Add(newUser);
                    db.InforCandidates.Add(info);
                    db.SaveChanges();

                    transaction.Commit();

                    return Json(new { status = "OK", message = "Đăng ký thành công" });
                }
                catch (DbEntityValidationException ex)
                {
                    transaction.Rollback();
                    var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                    return Json(new
                    {
                        status = "ERROR",
                        message = "Lỗi validation: " + string.Join("; ", errorMessages)
                    });
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Json(new { status = "ERROR", message = "Lỗi: " + ex.Message });
                }
            }
        }





        [HttpPost]
        public JsonResult SignUpEmployer(string username, string password, string company,
                               string email, string phone, string city, string address)
        {
            try
            {
                // Kiểm tra tài khoản đã tồn tại
                var existingUser = db.Logins.FirstOrDefault(x => x.username == username);
                if (existingUser != null)
                {
                    return Json(new { status = "EXIST", message = "Tài khoản đã tồn tại" });
                }

                // Kiểm tra email đã tồn tại
                var existingEmail = db.InforEmployers.FirstOrDefault(x => x.email == email);
                if (existingEmail != null)
                {
                    return Json(new { status = "EXIST", message = "Email đã được sử dụng" });
                }
                int phoneNumber;
                if (!int.TryParse(phone.Trim(), out phoneNumber))
                {
                    return Json(new { status = "ERROR", message = "Số điện thoại không hợp lệ" });
                }
                var existingPhone = db.InforCandidates.FirstOrDefault(x => x.phone == phoneNumber);
                if (existingPhone != null)
                {
                    return Json(new { status = "EXIST", message = "Số điện thoại đã được sử dụng" });
                }
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        // Tạo tài khoản mới
                        Login newUser = new Login
                        {
                            username = username.Trim(),
                            password = password.Trim(),
                            status = true,
                            typeRow = "Tuyển dụng",
                        };

                        // Tạo thông tin nhà tuyển dụng
                        InforEmployer info = new InforEmployer
                        {
                            email = email.Trim(),
                            phone = phoneNumber,
                            adrress = city,
                            addressDetail = address,
                            namecompany = company?.Trim(),
                            username = username,
                            image="facebook.jpg"
                        };

                        db.Logins.Add(newUser);
                        db.InforEmployers.Add(info);
                        db.SaveChanges();

                        transaction.Commit();
                        return Json(new { status = "OK", message = "Đăng ký thành công" });
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = "ERROR", message = ex.Message });
            }
        }


    }
}