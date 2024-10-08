using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class PostJobController : Controller
    {
        // GET: PostJob
        testdbs2425Entities db = new testdbs2425Entities();
        public ActionResult ShowPostJob()
        {
            return View();
        }
        public ActionResult TitleJobList()
        {
            var tableTitleListJob = db.TitlePages
                                .Where(t => t.hide == true && t.typePage == EnumType.Type.titlepagePostJob.ToString()).OrderBy(t => t.datebegin).FirstOrDefault();

            return PartialView(tableTitleListJob);
        }
        public ActionResult GetListTitle()
        {
            var tablemenus = db.tablebanners.Where(t => t.hide == true && t.typeRow == EnumType.Type.list.ToString()).OrderBy(t => t.arrange).ToList();
            return PartialView(tablemenus);
        }
        public ActionResult GetImageNameCompany() 
        {
            var tablemenus = db.InforEmployers.Where(t => t.username =="tuanta").FirstOrDefault();
            return PartialView(tablemenus);
        }
    }
}