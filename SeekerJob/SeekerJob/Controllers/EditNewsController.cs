using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeekerJob.Models;
namespace SeekerJob.Controllers
{
    public class EditNewsController : Controller
    {
        // GET: EditNews
        testdbs2425Entities db = new testdbs2425Entities();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IndexPostNews()
        {
            return View();
        }
        public ActionResult GetImageNameCompany()
        {
            var tablemenus = db.InforEmployers.Where(t => t.username == "tuanta").FirstOrDefault();
            return PartialView(tablemenus);
        }
        public ActionResult TitlePostNews()
        {
            var tableTitleListJob = db.TitlePages
                                .Where(t => t.hide == true && t.typePage == EnumType.Type.titlepagePostNews.ToString()).OrderBy(t => t.datebegin).FirstOrDefault();

            return PartialView(tableTitleListJob);
        }
        public ActionResult GetListTitle()
        {
            var tablemenus = db.tablebanners.Where(t => t.hide == true && t.typeRow == EnumType.Type.listNews.ToString()).OrderBy(t => t.arrange).ToList();
            return PartialView(tablemenus);
        }
    }
}