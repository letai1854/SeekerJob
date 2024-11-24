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
    public class ManagerJobController : Controller
    {
        // GET: ManagerJob



        testdbs2425Entities db = new testdbs2425Entities();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetImageNameCompany()
        {
            Login login = Session["employer"] as Login;
            var tablemenus = db.InforEmployers.Where(t => t.username == login.username).FirstOrDefault();
            return PartialView(tablemenus);
        }
        public ActionResult TitleSavedJob()
        {
            var tableTitleListJob = db.TitlePages
                                .Where(t => t.hide == true && t.typePage == EnumType.Type.titlepageManager.ToString()).OrderBy(t => t.datebegin).FirstOrDefault();

            return PartialView(tableTitleListJob);
        }
        public ActionResult GetListTitle()
        {
            var tablemenus = db.tablebanners.Where(t => t.hide == true && t.typeRow == EnumType.Type.list.ToString()).OrderBy(t => t.arrange).ToList();
            return PartialView(tablemenus);
        }
        public ActionResult GetListApplyJob()
        {
            Login login = Session["employer"] as Login;

            // Lấy danh sách công việc
            var result = (from j in db.Jobs
                          join l in db.ListCandidates on j.id equals l.idjob into gj  // LEFT JOIN
                          from l in gj.DefaultIfEmpty()                               // Cho phép null từ bảng ListCandidates
                          where j.username == login.username
                          group l by new
                          {
                              j.meta,
                              j.title,
                              j.offer,
                              j.startday,
                              j.jobcategory,
                              j.address,
                              j.id                                                    // Dùng j.id thay vì l.idjob
                          } into g
                          select new JobApplicationViewModel
                          {
                              Title = g.Key.title,
                              Offer = (double)g.Key.offer,
                              StartDay = g.Key.startday,
                              JobCategory = g.Key.jobcategory,
                              Address = g.Key.address,
                              IdJob = (int)g.Key.id,
                              CandidateCount = g.Count(x => x != null),              // Đếm các bản ghi không null
                              meta = g.Key.meta
                          }).ToList();

            // Tạo dictionary lưu danh sách file theo IdJob
            var filesByJob = new Dictionary<int, List<string>>();
            foreach (var job in result)
            {
                var files = db.ListCandidates
                             .Where(l => l.idjob == job.IdJob)
                             .Select(l => l.filecandiate)
                             .ToList();
                filesByJob.Add(job.IdJob, files);
            }

            ViewBag.vieclam = "Viec-lam";
            ViewBag.soluongungtuyen = "so-luong-ung-tuyen";
            ViewBag.FilesByJob = filesByJob; // Lưu dictionary vào ViewBag
            ViewData["applyjob"] = result;
            return PartialView("GetListApplyJob");
        }
        public ActionResult GetListCandidateApplyJob(int id)
        {
            var result = (from j in db.Jobs
                          join l in db.ListCandidates on j.id equals l.idjob
                          join i in db.InforCandidates on l.usernamecandidate equals i.username
                          where j.id == id
                          select new ListCandidateApplyJob
                          {
                             id=  j.id,
                             image = i.image,
                             name=  i.name,
                             filecandidate = l.filecandiate,
                              datesend =  l.datesend
                          }).ToList();

            ViewData["applyjobcandidate"] = result;
            return PartialView("GetListCandidateApplyJob");
        }

        [HttpPost]
        public JsonResult DeleteJob(int id)
        {
            JsonResult js = new JsonResult();
            try
            {
                // Kiểm tra đăng nhập
                Login user = Session["employer"] as Login;
                if (user == null)
                {
                    js.Data = new
                    {
                        status = "ERROR",
                        message = "Vui lòng đăng nhập"
                    };
                    return Json(js, JsonRequestBehavior.AllowGet);
                }

               
                    // Xóa các bản ghi liên quan trong ListCandidates trước
                    IO io = new IO();
                    io.DeleteJobs(id);
                    io.Save();
                    js.Data = new
                    {
                        status = "OK",
                        message = "Xóa công việc thành công"
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

        public ActionResult EditJob(int id)
        {

            var job = db.Jobs.Where(t => t.id == id).FirstOrDefault();
            ViewData["job"] = job;
            return PartialView("EditJob");

        }



        public ActionResult GetJobDetail()
        {
            return PartialView("_EditJobPartial"); // Trả về partial view trống
        }

        // Action này sẽ được gọi khi click nút edit
        [HttpPost]
        public ActionResult GetJobDetailById(int id)
        {
            var job = db.Jobs.Where(t => t.id == id).FirstOrDefault();
            ViewData["job"] = job;
            return PartialView("GetJobDetailById");
        }
        [HttpPost]
        public JsonResult UpdateJob(FormCollection Data)
        {
            // Giải mã nội dung từ CKEditor
            string description = HttpUtility.UrlDecode(Data["mota"]);

            // Lấy các thông tin từ Data
            string title = Data["tieude"];
            string category = Data["category"]; // Cập nhật từ "noidung" thành "category"
            string salary = Data["salary"]; // Lấy thông tin salary
            string experience = Data["experience"]; // Lấy thông tin experience
            string trinhdo = Data["trinhdo"]; // Lấy thông tin trình độ
            string gender = Data["gender"]; // Lấy thông tin giới tính
            string country = Data["country"]; // Lấy thông tin quốc gia
            string email = Data["email"];
            string img = Data["img"];
            string address = Data["address"]; // Lấy thông tin địa chỉ
            string endday = Data["endday"]; // Lấy thông tin ngày kết thúc
            string day = Data["day"]; // Lấy thông tin ngày hiện tại
            string meta = Data["meta"];
            var file = Request.Files["img"];
            int id = Convert.ToInt32(Data["id"]);
            string uniqueFileName = null;

            if (file != null && file.ContentLength > 0)
            {
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/ContentImage/images"), uniqueFileName); // Check this path!
                file.SaveAs(path);
            }

            IO io = new IO();
            JsonResult js = new JsonResult();
            Login user = null;
            if (Session["employer"] != null)
            {
                user = Session["employer"] as Login;
            }

            else if (Session["admin"] != null)
            {
                user = Session["admin"] as Login;
            }
            else if (Session["candidate"] != null)
            {
                user = Session["candidate"] as Login;
            }
            Job info = io.GetInfoJob(id);
            info.username = user.username;
            info.title = title;
            info.email = email;
            info.address = country;
            info.gender = gender;
            info.addressdetail = address;
            info.degree = trinhdo;
            info.experience = !string.IsNullOrEmpty(experience) ? (double?)double.Parse(experience) : null;
            info.offer = !string.IsNullOrEmpty(salary) ? (double?)double.Parse(salary) : null;
            info.jobcategory = category;
            info.description = description;
            if (uniqueFileName != null)
            {
                info.image = uniqueFileName;

            }
            info.meta = meta;
            info.endday = ParseExactDateTime(endday);
            io.Save();
            js.Data = new
            {
                status = "OK"
            };
            return Json(js, JsonRequestBehavior.AllowGet);
        }

        // Method to parse nullable DateTime safely
        private DateTime? ParseExactDateTime(string dateString)
        {
            if (DateTime.TryParseExact(dateString, "dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out DateTime date))
            {
                return date;
            }
            return null; // Return null if parsing fails
        }

        
        public ActionResult DownloadCV(string fileName)
        {
            try
            {
                string filePath = Server.MapPath("~/Content/CVs/" + fileName);

                if (System.IO.File.Exists(filePath))
                {
                    byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                    return File(fileBytes, "application/octet-stream", fileName);
                }

                return Json(new { success = false, message = "File không tồn tại" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}