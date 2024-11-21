﻿using SeekerJob.DTO;
using SeekerJob.Models;
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



        testdbs2425Entities db = new testdbs2425Entities();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetImageNameCompany()
        {
            Login login = Session["employer"] as Login;
            var tablemenus = db.InforEmployers.Where(t => t.username == login.username).FirstOrDefault();
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
            Login login = Session["employer"] as Login;
            var result = (from j in db.Jobs
                          join l in db.ListCandidates on j.id equals l.idjob
                          where j.username == login.username
                          group l by new
                          {
                              j.meta,
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
                              CandidateCount = g.Count(),
                              meta = g.Key.meta,

                          }).ToList();

            ViewBag.vieclam = "Viec-lam";
            ViewBag.soluongungtuyen = "so-luong-ung-tuyen";
            ViewData["applyjob"] = result;
            return PartialView("GetListApplyJob");
        }
        public ActionResult GetListCandidateApplyJob(int id)
        {
            var result = (from j in db.Jobs
                          join l in db.ListCandidates on j.id equals l.idjob
                          join i in db.InforCandidates on l.usernamecandidate equals i.username
                          where j.id == id
                          select new ListCandidateApplyJob
                          {
                             id=  j.id,
                             image = i.image,
                             name=  i.name,
                             filecandidate = l.filecandiate,
                              datesend =  l.datesend
                          }).ToList();

            ViewData["applyjobcandidate"] = result;
            return PartialView("GetListCandidateApplyJob");
        }

    }
}