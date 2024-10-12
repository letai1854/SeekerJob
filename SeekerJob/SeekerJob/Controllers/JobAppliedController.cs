using SeekerJob.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class JobAppliedController : Controller
    {
        // GET: JobApplied
        mydatabase db = new mydatabase();
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

        public ActionResult GetListJobApplied()
        {
            var result = (from j in db.Jobs
                          join l in db.ListCandidates on j.id equals l.idjob
                          join i in db.InforCandidates on l.usernamecandidate equals i.username
                          join e in db.InforEmployers on j.username equals e.username
                          where l.usernamecandidate == "totenla"
                          select new ListCandidateApplyJob
                          {
                              id = j.id,
                              image = e.image,
                              name = e.namecompany,
                              filecandidate = l.filecandiate,
                              datesend = l.datesend
                          }).ToList();

            ViewData["applyjobcandidate"] = result;
            return PartialView();
        }
    }
}