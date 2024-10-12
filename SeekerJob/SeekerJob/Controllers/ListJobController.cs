using SeekerJob.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class ListJobController : Controller
    {


        mydatabase db = new mydatabase();


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

            var listjob = (from j in db.Jobs join i in db.InforEmployers on j.username equals i.username
                           where j.endday > DateTime.Now
                           orderby j.id
                           select new JobEmployerViewModel 
                           {
                               Job = j,
                               InforEmployer = i
                           }).ToList();
            ViewData["listjob"] = listjob;
            ViewBag.metacontroller = "chi-tiet-viec-lam";
            return PartialView("GetListAllJob");
        }
    }
}