using SeekerJob.DTO;
using SeekerJob.Models;
using SeekerJob.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;


namespace SeekerJob.Controllers
{
    public class SavedJobCandidatesController : Controller
    {
        // GET: SavedJobCandidates



        testdbs2425Entities db = new testdbs2425Entities();


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
            SeekerJob.Models.Login user = Session["candidate"] as SeekerJob.Models.Login;
            var tablemenus = db.InforCandidates.Where(t => t.username == user.username).FirstOrDefault();
            return PartialView(tablemenus);

        }
        public ActionResult GetListTitle()
        {
            var tablemenus = db.tablebanners.Where(t => t.hide == true && t.typeRow == EnumType.Type.listCandidate.ToString()).OrderBy(t => t.arrange).ToList();
            return PartialView(tablemenus);
        }
        public ActionResult GetListSavedJob()
        {

             if (Session["candidate"] is SeekerJob.Models.Login user)
            {
                var result = (from j in db.Jobs
                              join i in db.InforEmployers on j.username equals i.username
                              join s in db.SaveJobs on j.id equals s.idjob
                              where s.usernamecandidate == user.username
                              orderby j.id
                              select new SaveJobViewModal
                              {
                                  Job = j,
                                  InforEmployer = i,
                                  saveJob = s
                              }).ToList();

                ViewBag.vieclam = "Chi-tiet-viec-lam";
                ViewBag.congty = "cong-ty";
                ViewData["savejob"] = result;
                return PartialView("GetListSavedJob");
            }
             else if(Session["admin"] is SeekerJob.Models.Login admin)
            {
                var result = (from j in db.Jobs
                              join i in db.InforEmployers on j.username equals i.username
                              join s in db.SaveJobs on j.id equals s.idjob
                              where s.usernamecandidate == admin.username
                              orderby j.id
                              select new SaveJobViewModal
                              {
                                  Job = j,
                                  InforEmployer = i,
                                  saveJob = s
                              }).ToList();

                ViewBag.vieclam = "Chi-tiet-viec-lam";
                ViewBag.congty = "cong-ty";
                ViewData["savejob"] = result;
                return PartialView("GetListSavedJob");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "User not logged in");
            }

            return new EmptyResult();


        }




        [HttpPost]
        public JsonResult DeleteSavedJob(FormCollection Data)
        {
            int id = Convert.ToInt32(Data["id"]);
            IO io = new IO();
            JsonResult js = new JsonResult();
            

            bool checkDelete = io.DeleteSavedJob(id);
            if (checkDelete)
            {
                io.Save();
                js.Data = new
                {
                    status = "OK"
                };
            }
            return Json(js, JsonRequestBehavior.AllowGet);
        }

    }
}