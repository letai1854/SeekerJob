using SeekerJob.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Areas.Admin.Controllers
{
    public class ListJobController : Controller
    {
        // GET: Admin/ListJob
        mydatabase db = new mydatabase();
        public ActionResult Index()
        {
            var table = db.Jobs.Where(t => t.endday >= DateTime.Now).OrderBy(t => t.endday).ToList();
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