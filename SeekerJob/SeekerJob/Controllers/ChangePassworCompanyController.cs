using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class ChangePassworCompanyController : Controller
    {
        // GET: ChangePassworCompany
        mydb db = new mydb();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IndexViewChangePassword()
        {
            return View();
        }
        public ActionResult TitleChangePassword()
        {
            var tableTitleListJob = db.TitlePages
                                .Where(t => t.hide == true && t.typePage == EnumType.Type.changepasswordcompany.ToString()).OrderBy(t => t.datebegin).FirstOrDefault();
            ViewData["title"] = tableTitleListJob;
            return PartialView();
        }

        public ActionResult GetListTitle()
        {
            var tablemenus = db.tablebanners.Where(t => t.hide == true && t.typeRow == EnumType.Type.list.ToString()).OrderBy(t => t.arrange).ToList();
            ViewData["list"] = tablemenus;
            return PartialView();
        }
        public ActionResult GetInfoChangePassword()
        {
            return PartialView();
        }
    }
}