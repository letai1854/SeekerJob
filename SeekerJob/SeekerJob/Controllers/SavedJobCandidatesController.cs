using SeekerJob.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SeekerJob.Controllers
{
    public class SavedJobCandidatesController : Controller
    {
        // GET: SavedJobCandidates

        mydb db = new mydb();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TitleSavedJob()
        {
            var tableTitleListJob = db.TitlePages
                                .Where(t => t.hide == true && t.typePage == EnumType.Type.titlepageSavedJob.ToString()).OrderBy(t => t.datebegin).FirstOrDefault();

            return PartialView(tableTitleListJob);
        }
        public ActionResult GetImageNameCompany()
        {
            var tablemenus = db.InforCandidates.Where(t => t.username == "nguyenalexJR").FirstOrDefault();
            return PartialView(tablemenus);
        }
        public ActionResult GetListTitle()
        {
            var tablemenus = db.tablebanners.Where(t => t.hide == true && t.typeRow == EnumType.Type.listCandidate.ToString()).OrderBy(t => t.arrange).ToList();
            return PartialView(tablemenus);
        }
        public ActionResult GetListSavedJob()
        {
            var result = (from j in db.Jobs
                          join i in db.InforEmployers on j.username equals i.username
                          join s in db.SaveJobs on j.id equals s.idjob
                          where s.usernamecandidate == "nguyenalexJR"
                          orderby j.id
                          select new SaveJobViewModal
                          {
                              Job = j,
                              InforEmployer = i,
                              saveJob = s
                          }).ToList();

            ViewBag.vieclam = "Viec-lam";
            ViewBag.congty = "cong-ty";
            ViewData["savejob"] = result;
            return PartialView("GetListSavedJob");
        }

    }
}