using SeekerJob.DTO;
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
    public class JobDetailController : Controller
    {
        // GET: JobDetail



        testdbs2425Entities db = new testdbs2425Entities();

        public ActionResult Index(long id)
        {
            var job = db.Jobs.Where(t=>t.id==id).FirstOrDefault();
            var user = db.InforEmployers.Where(t=>t.username==job.username).FirstOrDefault();
            var jobuser = new CombineJobUser() {
                job = job,
                employer = user,
            };
            ViewData["jobuser"] = jobuser;
            return View();
        }

        public ActionResult ViewJobDetail(long id)
        {
            Login info = null;
            if (Session["candidate"] != null)
            {
                info = Session["candidate"] as Login;

            }
            if (Session["adimin"] != null)
            {
                info = Session["admin"] as Login;
            }
            if (Session["employer"] != null)
            {
                info = Session["employer"] as Login;
            }
            var checkinfo = db.SaveJobs.FirstOrDefault(t=>t.idjob==id && t.usernamecandidate ==info.username);
            bool checkexist = false;
            if (checkinfo != null)
            {
                checkexist = true;
            }
            var listjobuser = new List<CombineJobUser>();
            var job = db.Jobs.Where(t => t.id == id).FirstOrDefault();
            var user = db.InforEmployers.Where(t => t.username == job.username).FirstOrDefault();
            var listjob = db.Jobs.Where(t => t.endday >= DateTime.Now && t.id!=id).OrderByDescending(t => t.endday).Take(4).ToList();
            var jobuser = new CombineJobUser()
            {
                job = job,
                employer = user,
            };
            foreach (var item in listjob)
            {
                var inforEmployer = db.InforEmployers
                                     .Where(t => t.username == item.username)
                                     .FirstOrDefault();
                listjobuser.Add(new CombineJobUser()
                {
                    job = item,
                    employer = inforEmployer
                });
            }
            ViewBag.metacontroller = "chi-tiet-viec-lam";
            ViewData["jobuser"] = jobuser;
            ViewData["listjobuser"] = listjobuser;
            ViewData["check"] = checkexist;
            ViewBag.metaprofile = "ho-so-cong-ty";
            return View(jobuser);
        }


        [HttpPost]
        public JsonResult UploadCV(FormCollection Data)
        {
            JsonResult js = new JsonResult();
            try
            {
                // Lấy thông tin user từ session
                Login user = Session["candidate"] as Login;
                if (user == null)
                {
                    js.Data = new
                    {
                        status = "ERROR",
                        message = "Vui lòng đăng nhập để nộp đơn"
                    };
                    return Json(js, JsonRequestBehavior.AllowGet);
                }

                // Lấy jobId từ form
                int jobId;
                if (!int.TryParse(Data["id"], out jobId))
                {
                    js.Data = new
                    {
                        status = "ERROR",
                        message = "Thông tin công việc không hợp lệ"
                    };
                    return Json(js, JsonRequestBehavior.AllowGet);
                }

                // Kiểm tra xem đã nộp đơn chưa
                var existingApplication = db.ListCandidates.FirstOrDefault(t =>
                    t.idjob == jobId &&
                    t.usernamecandidate == user.username
                );

                if (existingApplication != null)
                {
                    js.Data = new
                    {
                        status = "ERROR",
                        message = "Bạn đã nộp đơn cho công việc này rồi"
                    };
                    return Json(js, JsonRequestBehavior.AllowGet);
                }

                // Xử lý file CV
                var file = Request.Files["file"];
                string uniqueFileName = null;

                if (file != null && file.ContentLength > 0)
                {
                    // Kiểm tra định dạng file
                    string fileExtension = Path.GetExtension(file.FileName).ToLower();
                    string[] allowedExtensions = { ".pdf", ".doc", ".docx" };

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        js.Data = new
                        {
                            status = "ERROR",
                            message = "Chỉ chấp nhận file PDF hoặc Word"
                        };
                        return Json(js, JsonRequestBehavior.AllowGet);
                    }

                    // Tạo tên file unique
                    uniqueFileName = "CV-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + Path.GetFileName(file.FileName);// Lưu file
                    var path = Path.Combine(Server.MapPath("~/Content/CVs"), uniqueFileName);
                    file.SaveAs(path);
             
                }
                else
                {
                    js.Data = new
                    {
                        status = "ERROR",
                        message = "Vui lòng chọn file CV"
                    };
                    return Json(js, JsonRequestBehavior.AllowGet);
                }

                // Lưu vào database
                IO io = new IO();
              
                    var listcandidate = new ListCandidate
                    {
                        idjob = jobId,
                        usernamecandidate = user.username,
                        filecandiate = uniqueFileName,
                        datesend = DateTime.Now
                    };

                    io.AddObject(listcandidate);
                    io.Save();

                    js.Data = new
                    {
                        status = "OK",
                        message = "Nộp đơn ứng tuyển thành công!",
                        fileName = uniqueFileName
                    };
               
            }
            catch (Exception ex)
            {
                js.Data = new
                {
                    status = "ERROR",
                    message = "Có lỗi xảy ra: " + ex.Message
                };
            }

            return Json(js, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SaveJob(int id)
        {
            JsonResult js = new JsonResult();
            try
            {
                // Kiểm tra đăng nhập
                Login user = null;
                if (Session["candidate"]!=null)
                {
                    user = Session["candidate"] as Login;

                }
                else
                {
                    user = Session["admin"] as Login;
                }
                if (user == null)
                {
                    js.Data = new
                    {
                        status = "ERROR",
                        message = "LOGIN_REQUIRED"
                    };
                    return Json(js, JsonRequestBehavior.AllowGet);
                }

                // Kiểm tra job có tồn tại
                var job = db.Jobs.FirstOrDefault(t => t.id == id);
                if (job == null)
                {
                    js.Data = new
                    {
                        status = "ERROR",
                        message = "Công việc không tồn tại"
                    };
                    return Json(js, JsonRequestBehavior.AllowGet);
                }

                // Kiểm tra xem đã save chưa
                var saveJob = db.SaveJobs.FirstOrDefault(t =>
                    t.idjob == id &&
                    t.usernamecandidate == user.username
                );

                IO io = new IO();
                bool isSaved;

                if (saveJob != null)
                {
                    // Nếu đã save thì xóa (unsave)
                    io.deletesavejob(id,user.username);
                    isSaved = false;
                }
                else
                {
                    // Nếu chưa save thì thêm mới
                    SaveJob newSaveJob = new SaveJob
                    {
                        idjob = id,
                        usernamecandidate = user.username,
                        saveDate = DateTime.Now
                    };
                    io.AddObject(newSaveJob);
                    isSaved = true;
                }

                io.Save();

                js.Data = new
                {
                    status = "OK",
                    saved = isSaved,
                    message = isSaved ? "Đã lưu công việc" : "Đã bỏ lưu công việc"
                };
            }
            catch (Exception ex)
            {
                js.Data = new
                {
                    status = "ERROR",
                    message = "Có lỗi xảy ra: " + ex.Message
                };
            }

            return Json(js, JsonRequestBehavior.AllowGet);
        }
    }
}