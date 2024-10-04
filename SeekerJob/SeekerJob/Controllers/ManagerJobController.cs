using SeekerJob.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class ManagerJobController : Controller
    {
        // GET: ManagerJob
        mydbs db = new mydbs();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetImageNameCompany()
        {
            var tablemenus = db.InforEmployers.Where(t => t.username == "tuanta").FirstOrDefault();
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
            var result = (from j in db.Jobs
                          join l in db.ListCandidates on j.id equals l.idjob
                          where j.username == "tuanta"
                          group l by new
                          {
                              j.title,
                              j.offer,
                              j.startday,
                              j.jobcategory,
                              j.address,
                              l.idjob
                          } into g
                          select new JobApplicationViewModel
                          {
                              Title = g.Key.title,
                              Offer = (double)g.Key.offer,
                              StartDay = g.Key.startday,
                              JobCategory = g.Key.jobcategory,
                              Address = g.Key.address,
                              IdJob = (int)g.Key.idjob,
                              CandidateCount = g.Count()
                          }).ToList();

            ViewBag.vieclam = "Viec-lam";
            ViewBag.soluongungtuyen = "so-luong-ung-tuyen";
            ViewData["applyjob"] = result;
            return PartialView("GetListApplyJob");
        }
    }
}