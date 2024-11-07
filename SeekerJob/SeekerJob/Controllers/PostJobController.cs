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
    public class PostJobController : Controller
    {
        // GET: PostJob


        testdbs2425Entities db = new testdbs2425Entities();

        public ActionResult ShowPostJob()
        {
            return View();
        }
        public ActionResult TitleJobList()
        {
            var tableTitleListJob = db.TitlePages
                                .Where(t => t.hide == true && t.typePage == EnumType.Type.titlepagePostJob.ToString()).OrderBy(t => t.datebegin).FirstOrDefault();

            return PartialView(tableTitleListJob);
        }
        public ActionResult GetListTitle()
        {
            var tablemenus = db.tablebanners.Where(t => t.hide == true && t.typeRow == EnumType.Type.list.ToString()).OrderBy(t => t.arrange).ToList();
            return PartialView(tablemenus);
        }
        public ActionResult GetImageNameCompany() 
        {
            var tablemenus = db.InforEmployers.Where(t => t.username =="tuanta").FirstOrDefault();
            return PartialView(tablemenus);
        }


        [HttpPost]
        public JsonResult CreateJob(FormCollection Data)
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
            DateTime startday = DateTime.Now;
            string uniqueFileName = null;

            if (file != null && file.ContentLength > 0)
            {
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/ContentImage/images"), uniqueFileName); // Check this path!
                file.SaveAs(path);
            }

            IO io = new IO();
            JsonResult js = new JsonResult();
            Login user = new Login();
            if (Session["employer"] != null)
            {
                user = Session["employer"] as Login;
            }

            else if (Session["admin"] != null)
            {
                user = Session["admin"] as Login;
            }


            Job job = new Job()
            {
                username = user?.username,
                title = title,
                email = email,
                address = country,
                addressdetail = address,
                gender = gender,
                degree = trinhdo,
                experience = !string.IsNullOrEmpty(experience) ? (double?)double.Parse(experience) : null,
                offer = !string.IsNullOrEmpty(salary) ? (double?)double.Parse(salary) : null,
                jobcategory = category,
                description = description,
                image = uniqueFileName,
                meta = meta,
                likeNumber = 0,
                endday = ParseExactDateTime(endday),
                startday = startday
            };

            io.AddObject(job);
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

    }
}