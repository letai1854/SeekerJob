using SeekerJob.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeekerJob.Models;
using SeekerJob.Services;
namespace SeekerJob.Areas.Admin.Controllers
{
    public class ListJobController : Controller
    {
        // GET: Admin/ListJob
        testdbs2425Entities db = new testdbs2425Entities();
        public ActionResult Index()
        {
            var table = db.Jobs.OrderBy(t => t.endday).ToList();
            var listnewsemployer = new List<CombineJobUser>();

            foreach (var item in table)
            {
                var employee = db.InforEmployers.Where(t => t.username == item.username).FirstOrDefault();

                if (employee != null)
                {
                    listnewsemployer.Add(new CombineJobUser
                    {
                        employer = employee,
                        job = item
                    });
                }
            }
            ViewData["listnewsemployer"] = listnewsemployer;
            ViewBag.vieclam = "chi-tiet-viec-lam";
            ViewBag.suatintuc = "sua-viec-lam";
            return PartialView();
        }
        [HttpPost]
        public JsonResult DeleteJobs(FormCollection Data)
        {
            int id = Convert.ToInt32(Data["id"]);
            IO io = new IO();
            JsonResult js = new JsonResult();
            Login user = Session["admin"] as Login;

            bool checkDeleteJobs = io.DeleteJobs(id);
            if (checkDeleteJobs)
            {
                io.Save();
                js.Data = new
                {
                    status = "OK"
                };
            }
            return Json(js, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {

            return PartialView();
        }
        public ActionResult ViewJobDetail(long id)
        {

            var listjobuser = new List<CombineJobUser>();
            var job = db.Jobs.Where(t => t.id == id).FirstOrDefault();
            var user = db.InforEmployers.Where(t => t.username == job.username).FirstOrDefault();
            var listjob = db.Jobs.Where(t => t.endday >= DateTime.Now && t.id != id).OrderByDescending(t => t.endday).Take(4).ToList();
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