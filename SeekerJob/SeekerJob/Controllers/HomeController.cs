using SeekerJob.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeekerJob.Models;
namespace SeekerJob.Controllers
{
    public class HomeController : Controller
    {
        testdbs2425Entities db = new testdbs2425Entities();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetTitileAndAmountJob1()
        {
            var title = db.TitlePages.Where(t => t.hide == true && t.typePage==EnumType.TypeTitleHome.titlepagehome1.ToString()).OrderBy(t => t.datebegin).ToList();


            var amount = db.Jobs.Count().ToString();


            ViewData["title"] = title;
            ViewData["amount"] = amount;

            return PartialView("GetTitileAndAmountJob1");
        }
        public ActionResult GetTitlePageHome2()
        {
            var title = db.TitlePages.Where(t => t.hide == true && t.typePage == EnumType.TypeTitleHome.titlepagehome2.ToString()).OrderBy(t => t.datebegin).ToList();
            ViewData["title"] = title;
            return PartialView("GetTitlePageHome2");
        }
        public ActionResult GetImageCompanyHome()
        {
            var table = db.tablebanners.Where(t => t.hide == true && t.typeRow == EnumType.TypeTitleHome.headercompany.ToString()).FirstOrDefault() ;
            var title = db.tablebannerparts.Where(t => t.hide == true && t.idtable == table.id).OrderBy(t => t.datebegin).Take(8).ToList();
            ViewData["title"] = title;
            return PartialView("GetImageCompanyHome");
        }
        public ActionResult GetTitlePageHomeNews()
        {
            var title = db.TitlePages.Where(t => t.hide == true && t.typePage == EnumType.TypeTitleHome.titlepagehomenews.ToString()).OrderBy(t => t.datebegin).ToList();
            ViewData["title"] = title;
            return PartialView("GetTitlePageHomeNews");
        }
        //public ActionResult GetListAttractiveJobInHome()
        //{
        //     var listjobuser = new List<CombineJobUser>();
        //    var listjob = db.Jobs.Where(t => t.endday>=DateTime.Now).OrderByDescending(t=>t.endday).ToList();


        //    foreach(var job in listjob)
        //    {
        //        var inforEmployer = db.InforEmployers
        //                             .Where(t => t.username == job.username)
        //                             .FirstOrDefault();
        //        listjobuser.Add(new CombineJobUser()
        //        {
        //            job=job,
        //            employer=inforEmployer
        //        });
        //    }
        //    ViewBag.metacontroller = "chi-tiet-viec-lam";
        //    ViewData["listjobuser"] = listjobuser;
        //    return PartialView("GetListAttractiveJobInHome");
        //}
        // public ActionResult GetListAttractiveJobInHome(int page = 1)
        // {
        //     int pageSize = 12;
        //     var listjobuser = new List<CombineJobUser>();
        //     var listjob = db.Jobs.Where(t => t.endday >= DateTime.Now)
        //                         .OrderByDescending(t => t.endday)
        //                         .Skip((page - 1) * pageSize)
        //                         .Take(pageSize)
        //                         .ToList();

        //     foreach (var job in listjob)
        //     {
        //         var inforEmployer = db.InforEmployers
        //                             .Where(t => t.username == job.username)
        //                             .FirstOrDefault();
        //         listjobuser.Add(new CombineJobUser()
        //         {
        //             job = job,
        //             employer = inforEmployer
        //         });
        //     }

        //     // Tính tổng số trang
        //     int totalJobs = db.Jobs.Where(t => t.endday >= DateTime.Now).Count();
        //     int totalPages = (int)Math.Ceiling((double)totalJobs / pageSize);

        //     ViewBag.CurrentPage = page;
        //     ViewBag.TotalPages = totalPages;
        //     ViewBag.metacontroller = "chi-tiet-viec-lam";
        //     ViewData["listjobuser"] = listjobuser;

        //     if (Request.IsAjaxRequest())
        //         return PartialView("JobListPartial", listjobuser);
        //     return PartialView("GetListAttractiveJobInHome");
        // }
 public ActionResult GetListAttractiveJobInHome(int page = 1)
{
    int pageSize = 12;
    var listjobuser = new List<CombineJobUser>();
    
    // Nếu không phải là AJAX request (tức là load trang), reset session tìm kiếm
    if (!Request.IsAjaxRequest())
    {
        Session["IsSearching"] = false;
        Session["SearchResults"] = null;
        Session["SearchParams"] = null;
    }
    
    // Kiểm tra xem có đang trong chế độ tìm kiếm không
    bool isSearching = Session["IsSearching"] != null && (bool)Session["IsSearching"];
    
    if (isSearching && Session["SearchResults"] != null)
    {
        // Sử dụng kết quả tìm kiếm đã lưu
        var searchResults = Session["SearchResults"] as List<Job>;
        var pagedResults = searchResults.OrderByDescending(t => t.endday)
                                      .Skip((page - 1) * pageSize)
                                      .Take(pageSize)
                                      .ToList();

        foreach (var job in pagedResults)
        {
            var inforEmployer = db.InforEmployers
                                .Where(t => t.username == job.username)
                                .FirstOrDefault();
            listjobuser.Add(new CombineJobUser()
            {
                job = job,
                employer = inforEmployer
            });
        }

        // Tính tổng số trang dựa trên kết quả tìm kiếm
        int totalJobs = searchResults.Count;
        ViewBag.TotalPages = (int)Math.Ceiling((double)totalJobs / pageSize);
    }
    else
    {
        // Truy vấn bình thường nếu không có tìm kiếm
        var listjob = db.Jobs.Where(t => t.endday >= DateTime.Now)
                            .OrderByDescending(t => t.endday)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();

        foreach (var job in listjob)
        {
            var inforEmployer = db.InforEmployers
                                .Where(t => t.username == job.username)
                                .FirstOrDefault();
            listjobuser.Add(new CombineJobUser()
            {
                job = job,
                employer = inforEmployer
            });
        }

        int totalJobs = db.Jobs.Where(t => t.endday >= DateTime.Now).Count();
        ViewBag.TotalPages = (int)Math.Ceiling((double)totalJobs / pageSize);
    }

    ViewBag.CurrentPage = page;
    ViewBag.metacontroller = "chi-tiet-viec-lam";
    ViewData["listjobuser"] = listjobuser;

    if (Request.IsAjaxRequest())
        return PartialView("JobListPartial", listjobuser);
    return PartialView("GetListAttractiveJobInHome");
}
[HttpPost]
public ActionResult SearchJobs1(string position, string location, string jobType, string salaryRange, int page = 1)
{
    int pageSize = 12;
    var query = db.Jobs.Where(t => t.endday >= DateTime.Now);

    // Tìm theo vị trí
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
        string jobCategory = "";
        switch (jobType)
        {
            case "fulltime": jobCategory = "Toàn thời gian"; break;
            case "parttime": jobCategory = "Bán thời gian"; break;
            case "intern": jobCategory = "Thực tập"; break;
            case "freelance": jobCategory = "Tự do"; break;
        }
        query = query.Where(j => j.jobcategory == jobCategory);
    }

    // Tìm theo mức lương
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

    // Lưu điều kiện tìm kiếm vào Session
    Session["SearchParams"] = new Dictionary<string, string>
    {
        {"position", position},
        {"location", location},
        {"jobType", jobType},
        {"salaryRange", salaryRange}
    };

    // Lưu kết quả query vào Session
    var searchResults = query.ToList();
    Session["SearchResults"] = searchResults;
    Session["IsSearching"] = true;

    // Phân trang kết quả
    var pagedResults = searchResults.OrderByDescending(t => t.endday)
                                  .Skip((page - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToList();

    // Tạo danh sách kết hợp job và employer
    var listjobuser = new List<CombineJobUser>();
    foreach (var job in pagedResults)
    {
        var inforEmployer = db.InforEmployers
                            .Where(t => t.username == job.username)
                            .FirstOrDefault();
        listjobuser.Add(new CombineJobUser()
        {
            job = job,
            employer = inforEmployer
        });
    }

    // Tính tổng số trang
    int totalJobs = searchResults.Count;
    ViewBag.TotalPages = (int)Math.Ceiling((double)totalJobs / pageSize);
    ViewBag.CurrentPage = page;
    ViewBag.metacontroller = "chi-tiet-viec-lam";
    ViewData["listjobuser"] = listjobuser;

    return PartialView("GetListAttractiveJobInHome", listjobuser);
}
[HttpPost]
public ActionResult ResetPaginationState()
{
    Session["IsPaginationCreated"] = false;
    return Json(new { success = true });
}
    

        // [HttpPost]
        // public ActionResult SearchJobs1(string position, string location, string jobType, string salaryRange, int page = 1)
        // {
        //     int pageSize = 12;
        //     var query = db.Jobs.Where(t => t.endday >= DateTime.Now);

        //     // Tìm theo vị trí
        //     if (!string.IsNullOrEmpty(position))
        //     {
        //         query = query.Where(j => j.title.Contains(position));
        //     }

        //     // Tìm theo địa điểm
        //     if (!string.IsNullOrEmpty(location))
        //     {
        //         query = query.Where(j => j.address == location);
        //     }

        //     // Tìm theo loại công việc
        //     if (!string.IsNullOrEmpty(jobType) && jobType != "alljob")
        //     {
        //         string jobCategory = "";
        //         switch (jobType)
        //         {
        //             case "fulltime": jobCategory = "Toàn thời gian"; break;
        //             case "parttime": jobCategory = "Bán thời gian"; break;
        //             case "intern": jobCategory = "Thực tập"; break;
        //             case "freelance": jobCategory = "Tự do"; break;
        //         }
        //         query = query.Where(j => j.jobcategory == jobCategory);
        //     }

        //     // Tìm theo mức lương
        //     if (!string.IsNullOrEmpty(salaryRange))
        //     {
        //         switch (salaryRange)
        //         {
        //             case "under500":
        //                 query = query.Where(j => j.offer < 500);
        //                 break;
        //             case "500to2000":
        //                 query = query.Where(j => j.offer >= 500 && j.offer <= 2000);
        //                 break;
        //             case "2000to4000":
        //                 query = query.Where(j => j.offer > 2000 && j.offer <= 4000);
        //                 break;
        //             case "4000to7000":
        //                 query = query.Where(j => j.offer > 4000 && j.offer <= 7000);
        //                 break;
        //             case "7000to10000":
        //                 query = query.Where(j => j.offer > 7000 && j.offer <= 10000);
        //                 break;
        //             case "above10000":
        //                 query = query.Where(j => j.offer > 10000);
        //                 break;
        //         }
        //     }
        //     int totalJobs = query.Count();
        //     int totalPages = (int)Math.Ceiling((double)totalJobs / pageSize);
        //     var listjob = query.OrderByDescending(t => t.endday)
        //                       .Skip((page - 1) * pageSize)
        //                       .Take(pageSize)
        //                       .ToList();

        //     var listjobuser = new List<CombineJobUser>();
        //     foreach (var job in listjob)
        //     {
        //         var inforEmployer = db.InforEmployers
        //                             .Where(t => t.username == job.username)
        //                             .FirstOrDefault();
        //         listjobuser.Add(new CombineJobUser()
        //         {
        //             job = job,
        //             employer = inforEmployer
        //         });
        //     }

        //     ViewBag.CurrentPage = page;
        //     ViewBag.TotalPages = (int)Math.Ceiling((double)query.Count() / pageSize);
        //     ViewBag.metacontroller = "chi-tiet-viec-lam";
        //     ViewData["listjobuser"] = listjobuser;

        //     return PartialView("JobListPartial", listjobuser);
        // }

        public ActionResult GetTitleListCompany()
        {
            var title = db.TitlePages.Where(t => t.hide == true && t.typePage == EnumType.TypeTitleHome.titlepagehomecompany.ToString()).OrderBy(t => t.datebegin).FirstOrDefault();
            ViewData["title"] = title;
            return PartialView("GetTitleListCompany");
        }
        public ActionResult AdvertiseHomeFirst()
        {
            var adv = db.tablebanners.Where(t => t.hide == true && t.typeRow == EnumType.Adertise.advertisehome1.ToString()).FirstOrDefault();
            var listadv = db.tablebannerparts
    .Where(t => t.hide == true &&
           t.link != null &&
           t.idtable == adv.id)
    .OrderByDescending(t => t.datebegin)  // Sắp xếp theo datebegin mới nhất
    .Take(8)
    .ToList();
            ViewData["listadv"] = listadv;
            return PartialView("AdvertiseHomeFirst");
        }
        public ActionResult GetListJobLike()
        {
            var listjobuser = new List<CombineJobUser>();

            var listjob = db.Jobs
                            .Where(t => t.endday >= DateTime.Now)
                            .OrderByDescending(t => t.startday)
                            .Take(8)
                            .ToList();
            foreach (var job in listjob)
            {
                var inforEmployer = db.InforEmployers
                                     .Where(t => t.username == job.username)
                                     .FirstOrDefault();
                listjobuser.Add(new CombineJobUser()
                {
                    job = job,
                    employer = inforEmployer
                });
            }
            ViewData["listjobuser"] = listjobuser;
            return PartialView("GetListJobLike");
        }
        public ActionResult AdvertiseHomeSecond()
        {
            var adv = db.tablebanners.Where(t => t.hide == true && t.typeRow == EnumType.Adertise.advertisehome2.ToString()).FirstOrDefault();
            var listadv = db.tablebannerparts
    .Where(t => t.hide == true &&
           t.link != null &&
           t.idtable == adv.id &&
           t.datebegin != null)  // Kiểm tra datebegin không null
    .OrderByDescending(t => t.datebegin)  // Sắp xếp theo ngày mới nhất
    .Take(3)  // Lấy 3 bản ghi
    .ToList();
            ViewData["listadv"] = listadv;
            return PartialView("AdvertiseHomeSecond");
        }

        public ActionResult GetListNewsHome()
        {
            var infoemployee = new List<NewsEmployee>();
            var infocandidate = new List<NewsCandidate>();

            var news = db.News
                         .OrderByDescending(t => t.daypost)
                         .Take(4)
                         .ToList();

            foreach (var item in news)
            {
                var inforEmployer = db.InforEmployers
                                      .Where(t => t.username == item.username)
                                      .FirstOrDefault();

                if (inforEmployer != null)
                {
                    infoemployee.Add(new NewsEmployee
                    {
                        inforEmployer = inforEmployer,
                        newsinfo = item
                    });
                }

                var inforcandidate = db.InforCandidates
                                       .Where(t => t.username == item.username)
                                       .FirstOrDefault();

                if (inforcandidate != null) 
                {
                    infocandidate.Add(new NewsCandidate
                    {
                        inforCandidate = inforcandidate,
                        newsinfo = item
                    });
                }
            }
            ViewBag.metacontroller = "Chi-tiet-tin-tuc";
            ViewData["infoemployee"] = infoemployee;
            ViewData["infocandidate"] = infocandidate;

            return PartialView("GetListNewsHome");
        }


        public ActionResult GetFooter()
        {
            var imgcompany = db.tablefooters.Where(t => t.typerow == EnumType.footer.imgcompany.ToString() && t.hide == true)
                                            .OrderByDescending(t => t.datebegin)
                                            .FirstOrDefault();
            var package = db.tablefooters.Where(t => t.typerow == EnumType.footer.packgagesoftware.ToString() && t.hide == true)
                                         .OrderByDescending(t => t.datebegin)
                                         .FirstOrDefault();
            var contact = db.tablefooters.Where(t => t.typerow == EnumType.footer.contactinfo.ToString() && t.hide == true)
                                         .OrderByDescending(t => t.datebegin)
                                         .Take(3).ToList();
            var infofooter = db.tablefooters.Where(t => t.typerow == EnumType.footer.infofooter.ToString() && t.hide == true)
                                            .OrderByDescending(t => t.datebegin)
                                            .Take(4).ToList();
            var copyright = db.tablefooters.Where(t => t.typerow == EnumType.footer.copyright.ToString() && t.hide == true)
                                            .OrderByDescending(t => t.datebegin)
                                            .FirstOrDefault();
            var connectdiff = db.tablefooters.Where(t => t.typerow == EnumType.footer.connectdiff.ToString() && t.hide == true)
                                .OrderByDescending(t => t.datebegin)
                                .Take(4).ToList();
            var listfooter = new List<FooterCombineTable>();

            foreach (var item in infofooter)
            {
                var parts = db.tablefooterparts.Where(t => t.hide == true && t.idtable == item.id)
                                               .OrderByDescending(t => t.datebegin)
                                               .Take(4).ToList();

                listfooter.Add(new FooterCombineTable
                {
                    footercombine = new Dictionary<tablefooter, List<tablefooterpart>>()
            {
                { item, parts } 
            }
                });
            }
            ViewData["imgcompany"] = imgcompany;
            ViewData["package"] = package;
            ViewData["contact"] = contact;
            ViewData["listfooter"] = listfooter;
            ViewData["copyright"] = copyright;
            ViewData["connectdiff"] = connectdiff;

            return PartialView("GetFooter");
        }



    }
}