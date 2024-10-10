using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class ProfileCandidatesController : Controller
    {
        mydb db = new mydb();
        public ActionResult IndexProfileCandidate()
        {
            return View();
        }
        public ActionResult TitleProfileCandidates()
        {
            var tableTitleListJob = db.TitlePages
                                .Where(t => t.hide == true && t.typePage == EnumType.Type.titlepageProfileCandidate.ToString()).OrderBy(t => t.datebegin).FirstOrDefault();

            return PartialView(tableTitleListJob);
        }
        public ActionResult GetImageNameCandidates()
        {
            var tablemenus = db.InforCandidates.Where(t => t.username == "nguyenalexJR").FirstOrDefault();
            return PartialView(tablemenus);
        }
        public ActionResult GetListTitle()
        {
            var tablemenus = db.tablebanners.Where(t => t.hide == true && t.typeRow == EnumType.Type.listCandidate.ToString()).OrderBy(t => t.arrange).ToList();
            return PartialView(tablemenus);
        }
        public ActionResult GetInfoCandidate()
        {
            var info = db.InforCandidates.Where(t => t.username == "nguyenalexJR").FirstOrDefault();
            return PartialView(info);
        }
    }
}