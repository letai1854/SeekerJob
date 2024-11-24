using SeekerJob.DTO;
using SeekerJob.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class ListJobController : Controller
    {


        testdbs2425Entities db = new testdbs2425Entities();


        public ActionResult GetJobList()
        {
            return View();
        }
        public ActionResult TitleJobList()
        {
            var tableTitleListJob = db.TitlePages
                                .Where(t => t.hide == true && t.typePage == EnumType.Type.Danhsachvieclam.ToString()).OrderBy( t => t.datebegin).FirstOrDefault();

            return PartialView(tableTitleListJob);
        }
        public ActionResult GetTitleSearchJob()
        {

            var tableTileSearchJob = db.tablebanners.Where(t=> t.hide ==true && t.typeRow == EnumType.Type.searchTitleJob.ToString()).OrderBy(t => t.arrange).ToList();


            var tableElementTitleSearchJob = db.tablebannerparts.Where(t => t.hide == true).OrderBy(t => t.arrange).ToList();

            ViewData["tablebanners"] = tableTileSearchJob;
            ViewData["tablebannerparts"] = tableElementTitleSearchJob;

            return PartialView("GetTitleSearchJob");
        }
        public ActionResult GetListAllJob()
        {
            var query = (from j in db.Jobs 
                        join i in db.InforEmployers on j.username equals i.username
                        where j.endday > DateTime.Now
                        orderby j.id
                        select new JobEmployerViewModel 
                        {
                            Job = j,
                            InforEmployer = i
                        });

            // Lấy tham số tìm kiếm từ Session
            string searchTitle = Session["searchJobTitle"] as string;
            string searchLocation = Session["searchLocation"] as string;

            if (!string.IsNullOrEmpty(searchTitle))
            {
                query = query.Where(x => x.Job.title.Contains(searchTitle));
            }

            if (!string.IsNullOrEmpty(searchLocation))
            {
                query = query.Where(x => x.Job.address == searchLocation);
            }

            var results = query.OrderByDescending(x => x.Job.startday).ToList();
            ViewData["listjob"] = results;
            ViewBag.metacontroller = "chi-tiet-viec-lam";
            return PartialView("GetListAllJob");
        }


        // ... existing code ...
        [HttpPost]
        public ActionResult SearchJobs(string address, List<string> jobCategories, string postDayType,
    double? salaryFrom, double? salaryTo)
        {
            var currentTime = DateTime.Now;

            var query = (from j in db.Jobs
                         join i in db.InforEmployers on j.username equals i.username
                         where j.endday > DateTime.Now
                         orderby j.id
                         select new JobEmployerViewModel
                         {
                             Job = j,
                             InforEmployer = i
                         });

            if (!string.IsNullOrEmpty(address))
            {
                query = query.Where(x => x.Job.address == address);
            }

            if (jobCategories != null && jobCategories.Any())
            {
                query = query.Where(x => jobCategories.Contains(x.Job.jobcategory));
            }

            // Xử lý lọc theo thời gian
            if (!string.IsNullOrEmpty(postDayType))
            {
                switch (postDayType)
                {
                    case "1hour":
                        var oneHourAgo = currentTime.AddHours(-1);
                        query = query.Where(x => x.Job.startday >= oneHourAgo);
                        break;
                    case "24hours":
                        var oneDayAgo = currentTime.AddHours(-24);
                        query = query.Where(x => x.Job.startday >= oneDayAgo);
                        break;
                    case "7days":
                        var sevenDaysAgo = currentTime.AddDays(-7);
                        query = query.Where(x => x.Job.startday >= sevenDaysAgo);
                        break;
                    case "14days":
                        var fourteenDaysAgo = currentTime.AddDays(-14);
                        query = query.Where(x => x.Job.startday >= fourteenDaysAgo);
                        break;
                    case "30days":
                        var thirtyDaysAgo = currentTime.AddDays(-30);
                        query = query.Where(x => x.Job.startday >= thirtyDaysAgo);
                        break;
                }
            }

            // Lọc theo mức lương
            if (salaryFrom.HasValue)
            {
                query = query.Where(x => x.Job.offer >= salaryFrom);
            }
            if (salaryTo.HasValue)
            {
                query = query.Where(x => x.Job.offer <= salaryTo);
            }

            // Sắp xếp và lấy kết quả
            var results = query.OrderByDescending(x => x.Job.startday).ToList();

            ViewData["listjob"] = results;
            ViewBag.metacontroller = "chi-tiet-viec-lam";
            return PartialView("GetListAllJob");
        }
        [HttpPost]
        public JsonResult SaveSearchParams(string jobTitle, string location)
        {
            Session["searchJobTitle"] = jobTitle;
            Session["searchLocation"] = location;
            return Json(new { success = true });
        }
        [HttpPost]
public ActionResult SearchJobsHome(string position, string location, string jobType, string salaryRange)
{
    var query = db.Jobs.Where(t => t.endday >= DateTime.Now);

    // Tìm theo vị trí (tìm gần đúng)
    if (!string.IsNullOrEmpty(position))
    {
        query = query.Where(j => j.title.Contains(position));
    }

    // Tìm theo địa điểm
    if (!string.IsNullOrEmpty(location))
    {
        query = query.Where(j => j.address == location);
    }

    // Tìm theo loại công việc
    if (!string.IsNullOrEmpty(jobType) && jobType != "alljob")
    {
        query = query.Where(j => j.jobcategory == jobType);
    }

    // Tìm theo khoảng lương
    if (!string.IsNullOrEmpty(salaryRange))
    {
        switch (salaryRange)
        {
            case "under500":
                query = query.Where(j => j.offer < 500);
                break;
            case "500to2000":
                query = query.Where(j => j.offer >= 500 && j.offer <= 2000);
                break;
            case "2000to4000":
                query = query.Where(j => j.offer > 2000 && j.offer <= 4000);
                break;
            case "4000to7000":
                query = query.Where(j => j.offer > 4000 && j.offer <= 7000);
                break;
            case "7000to10000":
                query = query.Where(j => j.offer > 7000 && j.offer <= 10000);
                break;
            case "above10000":
                query = query.Where(j => j.offer > 10000);
                break;
        }
    }

    // Lưu kết quả tìm kiếm vào Session để sử dụng trong GetListAttractiveJobInHome
    Session["SearchResults"] = query.Select(j => j.id).ToList();

    return Json(new { success = true });
}

    }
}