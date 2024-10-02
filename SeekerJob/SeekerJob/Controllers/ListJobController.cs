using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class ListJobController : Controller
    {
        // GET: ListJob
        MYDbS db = new MYDbS();
        public ActionResult GetJobList()
        {
            return View();
        }
        public ActionResult TitleJobList()
        {

            var tableTitleListJob = db.TitlePages
                              .Where(t => t.hide == true && t.typePage == EnumType.Type.Danhsachvieclam.ToString())
                              .ToList();
            return View(tableTitleListJob.FirstOrDefault());
        }
    }
}