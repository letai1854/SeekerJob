using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class ProfileCompanyController : Controller
    {
        // GET: ProfileCompany
        mysql db = new mysql();
        public ActionResult IndexProfileCompany()
        {
            return View();
        }
        public ActionResult TitleProfileCompany()
        {
            var tableTitleListJob = db.TitlePages
                                .Where(t => t.hide == true && t.typePage == EnumType.Type.titlepageProfileCompany.ToString()).OrderBy(t => t.datebegin).FirstOrDefault();

            return PartialView(tableTitleListJob);
        }
        public ActionResult GetImageNameCompany()
        {
            var tablemenus = db.InforEmployers.Where(t => t.username == "tuanta").FirstOrDefault();
            return PartialView(tablemenus);
        }
        public ActionResult GetListTitle()
        {
            var tablemenus = db.tablebanners.Where(t => t.hide == true && t.typeRow == EnumType.Type.list.ToString()).OrderBy(t => t.arrange).ToList();
            return PartialView(tablemenus);
        }
        public ActionResult GetInfoCompany() 
        {
            var info = db.InforEmployers.Where(t => t.username == "tuanta").FirstOrDefault();
            return PartialView(info);
        }
    }
}