using SeekerJob.DTO;
using SeekerJob.Models;
using SeekerJob.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class JobAppliedController : Controller
    {
        // GET: JobApplied
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
            Login user = Session["candidate"] as Login;
            var tablemenus = db.InforCandidates.Where(t => t.username == user.username).FirstOrDefault();
            return PartialView(tablemenus);
        }
        public ActionResult GetListTitle()
        {
            if (Session["candidate"] is SeekerJob.Models.Login user)
            {
                var tablemenus = db.tablebanners.Where(t => t.hide == true && t.typeRow == EnumType.Type.listCandidate.ToString()).OrderBy(t => t.arrange).ToList();
                return PartialView(tablemenus);
            }
            else if (Session["admin"] is SeekerJob.Models.Login admin)
            {
                var tablemenus = db.tablebanners.Where(t => t.hide == true && t.id!=20 && t.typeRow == EnumType.Type.listCandidate.ToString()).OrderBy(t => t.arrange).ToList();
                return PartialView(tablemenus);

            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "User not logged in");
            }
            return new EmptyResult();
        }

        public ActionResult GetListJobApplied()
        {
            if (Session["candidate"] is SeekerJob.Models.Login candidate)
            {
                var result = (from j in db.Jobs
                              join l in db.ListCandidates on j.id equals l.idjob
                              join i in db.InforCandidates on l.usernamecandidate equals i.username
                              join e in db.InforEmployers on j.username equals e.username
                              where l.usernamecandidate == candidate.username
                              select new ListCandidateApplyJob
                              {
                                  id = j.id,
                                  image = e.image,
                                  name = e.namecompany,
                                  filecandidate = l.filecandiate,
                                  datesend = l.datesend,
                                  meta = j.meta
                              }).ToList();

                ViewData["applyjobcandidate"] = result;
                ViewBag.detail = "Chi-tiet-viec-lam";
                return PartialView();
            }
            else if (Session["admin"] is SeekerJob.Models.Login admin)
            {
                var result = (from j in db.Jobs
                              join l in db.ListCandidates on j.id equals l.idjob
                              join i in db.InforCandidates on l.usernamecandidate equals i.username
                              join e in db.InforEmployers on j.username equals e.username
                              where l.usernamecandidate == admin.username
                              select new ListCandidateApplyJob
                              {
                                  id = j.id,
                                  image = e.image,
                                  name = e.namecompany,
                                  filecandidate = l.filecandiate,
                                  datesend = l.datesend,
                                  meta = j.meta
                              }).ToList();

                ViewData["applyjobcandidate"] = result;
                ViewBag.detail = "Chi-tiet-viec-lam";
                return PartialView();
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "User not logged in");
            }
            return new EmptyResult();
        }


        [HttpPost]
        public JsonResult DeleteApply(int id)
        {
            JsonResult js = new JsonResult();
            try
            {
                // Kiểm tra đăng nhập
                Login user = null;
                if (Session["candidate"] != null)
                {
                    user = Session["candidate"] as Login;
                }
                else
                {
                    user = Session["admin"] as Login;
                }
                if (user == null)
                {
                    js.Data = new
                    {
                        status = "ERROR",
                        message = "Vui lòng đăng nhập"
                    };
                    return Json(js, JsonRequestBehavior.AllowGet);
                }



                // Xóa đơn ứng tuyển
                IO io = new IO();
                io.deleteappliedjobcandidate(id,user.username);
                io.Save();

                js.Data = new
                {
                    status = "OK",
                    message = "Xóa đơn ứng tuyển thành công"
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
    }
}