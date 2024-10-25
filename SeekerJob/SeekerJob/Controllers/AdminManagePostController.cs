using SeekerJob.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeekerJob.Models;
namespace SeekerJob.Controllers
{
    public class AdminManagePostController : Controller
    {
        // GET: AdminManagePost

        testdbs2425Entities db = new testdbs2425Entities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexPost()
        {
            return View();
        }
        public ActionResult TitleManagePost()
        {
            var tableTitleListJob = db.TitlePages
                                .Where(t => t.hide == true && t.typePage == EnumType.Type.adminpost.ToString()).OrderBy(t => t.datebegin).FirstOrDefault();
            ViewData["title"] = tableTitleListJob;
            return PartialView();
        }

        public ActionResult GetListTitle()
        {
            var tablemenus = db.tablebanners.Where(t => t.hide == true && t.typeRow == EnumType.Type.adminmange.ToString()).OrderBy(t => t.arrange).ToList();
            ViewData["list"] = tablemenus;
            return PartialView();
        }
        public ActionResult GetListPost()
        {
            var table = db.Jobs.Where(t=>t.endday>=DateTime.Now).OrderBy(t=>t.endday).ToList();
            var listnewsemployer = new List<CombineJobUser>();

            foreach (var item in table)
            {
                var employee = db.InforEmployers.Where(t => t.username == item.username).FirstOrDefault();
           
                if (employee != null)
                {
                    listnewsemployer.Add(new CombineJobUser
                    {
                        employer = employee,
                        job = item
                    });
                }
            }
            ViewData["listnewsemployer"] = listnewsemployer;
            ViewBag.vieclam = "chi-tiet-viec-lam";
            ViewBag.suatintuc = "sua-viec-lam";
            return PartialView();
        }
    }


}
