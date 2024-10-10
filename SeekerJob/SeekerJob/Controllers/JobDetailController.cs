using SeekerJob.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class JobDetailController : Controller
    {
        // GET: JobDetail

        mydb db = new mydb();
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
            ViewBag.metaprofile = "ho-so-cong-ty";
            return View(jobuser);
        }
    }
}