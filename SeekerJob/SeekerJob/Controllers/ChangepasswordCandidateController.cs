using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class ChangepasswordCandidateController : Controller
    {
        // GET: ChangepasswordCandidate
        MyDB db = new MyDB();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IndexChangePasswordCandidate()
        {
            return View();
        }
        public ActionResult IndexViewChangePassword()
        {
            return View();
        }
        public ActionResult Changepassword()
        {
            return PartialView();
        }
        public ActionResult GetTitle() {
            var tableTitleListJob = db.TitlePages
                                .Where(t => t.hide == true && t.typePage == EnumType.Type.changepasswordcandidate.ToString()).OrderBy(t => t.datebegin).FirstOrDefault();
            ViewData["password"] = tableTitleListJob;
            return PartialView();
        }
        public ActionResult GetListChoosen()
        {
            var tablemenus = db.tablebanners.Where(t => t.hide == true && t.typeRow == EnumType.Type.listCandidate.ToString()).OrderBy(t => t.arrange).ToList();
            ViewData["list"] = tablemenus;
            return PartialView();
        }
        public ActionResult GetInfoChangepassword()
        {

            return PartialView();
        }
    }
}