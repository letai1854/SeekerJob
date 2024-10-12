using SeekerJob.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class HomeController : Controller
    {


        MYDB db = new MYDB();




        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult GetTitileAndAmountJob1()
        {
            var title = db.TitlePages.Where(t => t.hide == true && t.typePage==EnumType.TypeTitleHome.titlepagehome1.ToString()).OrderBy(t => t.datebegin).ToList();


            var amount = db.Jobs.Count().ToString();


            ViewData["title"] = title;
            ViewData["amount"] = amount;

            return PartialView("GetTitileAndAmountJob1");
        }
        public ActionResult GetTitlePageHome2()
        {
            var title = db.TitlePages.Where(t => t.hide == true && t.typePage == EnumType.TypeTitleHome.titlepagehome2.ToString()).OrderBy(t => t.datebegin).ToList();
            ViewData["title"] = title;
            return PartialView("GetTitlePageHome2");
        }
        public ActionResult GetImageCompanyHome()
        {
            var table = db.tablebanners.Where(t => t.hide == true && t.typeRow == EnumType.TypeTitleHome.headercompany.ToString()).FirstOrDefault() ;
            var title = db.tablebannerparts.Where(t => t.hide == true && t.idtable == table.id).OrderBy(t => t.datebegin).Take(8).ToList();
            ViewData["title"] = title;
            return PartialView("GetImageCompanyHome");
        }
        public ActionResult GetTitlePageHomeNews()
        {
            var title = db.TitlePages.Where(t => t.hide == true && t.typePage == EnumType.TypeTitleHome.titlepagehomenews.ToString()).OrderBy(t => t.datebegin).ToList();
            ViewData["title"] = title;
            return PartialView("GetTitlePageHomeNews");
        }
        public ActionResult GetListAttractiveJobInHome()
        {
             var listjobuser = new List<CombineJobUser>();
            var listjob = db.Jobs.Where(t => t.endday>=DateTime.Now).OrderByDescending(t=>t.endday).Take(12).ToList();


            foreach(var job in listjob)
            {
                var inforEmployer = db.InforEmployers
                                     .Where(t => t.username == job.username)
                                     .FirstOrDefault();
                listjobuser.Add(new CombineJobUser()
                {
                    job=job,
                    employer=inforEmployer
                });
            }
            ViewBag.metacontroller = "chi-tiet-viec-lam";
            ViewData["listjobuser"] = listjobuser;
            return PartialView("GetListAttractiveJobInHome");
        }
        public ActionResult GetTitleListCompany()
        {
            var title = db.TitlePages.Where(t => t.hide == true && t.typePage == EnumType.TypeTitleHome.titlepagehomecompany.ToString()).OrderBy(t => t.datebegin).FirstOrDefault();
            ViewData["title"] = title;
            return PartialView("GetTitleListCompany");
        }
        public ActionResult AdvertiseHomeFirst()
        {
            var adv = db.tablebanners.Where(t => t.hide == true && t.typeRow == EnumType.Adertise.advertisehome1.ToString()).FirstOrDefault();
            var listadv = db.tablebannerparts.Where(t => t.hide == true && t.link != null && t.idtable == adv.id).Take(8).ToList();
            ViewData["listadv"] = listadv;
            return PartialView("AdvertiseHomeFirst");
        }
        public ActionResult GetListJobLike()
        {
            var listjobuser = new List<CombineJobUser>();

            var listjob = db.Jobs
                            .Where(t => t.endday >= DateTime.Now)
                            .OrderByDescending(t => t.likeNumber)
                            .Take(8)
                            .ToList();
            foreach (var job in listjob)
            {
                var inforEmployer = db.InforEmployers
                                     .Where(t => t.username == job.username)
                                     .FirstOrDefault();
                listjobuser.Add(new CombineJobUser()
                {
                    job = job,
                    employer = inforEmployer
                });
            }
            ViewData["listjobuser"] = listjobuser;
            return PartialView("GetListJobLike");
        }
        public ActionResult AdvertiseHomeSecond()
        {
            var adv = db.tablebanners.Where(t => t.hide == true && t.typeRow == EnumType.Adertise.advertisehome2.ToString()).FirstOrDefault();
            var listadv = db.tablebannerparts.Where(t => t.hide == true && t.link != null && t.idtable == adv.id).Take(3).ToList();
            ViewData["listadv"] = listadv;
            return PartialView("AdvertiseHomeSecond");
        }

        public ActionResult GetListNewsHome()
        {
            var infoemployee = new List<NewsEmployee>();
            var infocandidate = new List<NewsCandidate>();

            var news = db.News
                         .OrderByDescending(t => t.daypost)
                         .Take(4)
                         .ToList();

            foreach (var item in news)
            {
                var inforEmployer = db.InforEmployers
                                      .Where(t => t.username == item.username)
                                      .FirstOrDefault();

                if (inforEmployer != null)
                {
                    infoemployee.Add(new NewsEmployee
                    {
                        inforEmployer = inforEmployer,
                        newsinfo = item
                    });
                }

                var inforcandidate = db.InforCandidates
                                       .Where(t => t.username == item.username)
                                       .FirstOrDefault();

                if (inforcandidate != null) 
                {
                    infocandidate.Add(new NewsCandidate
                    {
                        inforCandidate = inforcandidate,
                        newsinfo = item
                    });
                }
            }
            ViewBag.metacontroller = "Chi-tiet-tin-tuc";
            ViewData["infoemployee"] = infoemployee;
            ViewData["infocandidate"] = infocandidate;

            return PartialView("GetListNewsHome");
        }


        public ActionResult GetFooter()
        {
            var imgcompany = db.tablefooters.Where(t => t.typerow == EnumType.footer.imgcompany.ToString() && t.hide == true)
                                            .OrderByDescending(t => t.datebegin)
                                            .FirstOrDefault();
            var package = db.tablefooters.Where(t => t.typerow == EnumType.footer.packgagesoftware.ToString() && t.hide == true)
                                         .OrderByDescending(t => t.datebegin)
                                         .FirstOrDefault();
            var contact = db.tablefooters.Where(t => t.typerow == EnumType.footer.contactinfo.ToString() && t.hide == true)
                                         .OrderByDescending(t => t.datebegin)
                                         .Take(3).ToList();
            var infofooter = db.tablefooters.Where(t => t.typerow == EnumType.footer.infofooter.ToString() && t.hide == true)
                                            .OrderByDescending(t => t.datebegin)
                                            .Take(4).ToList();
            var copyright = db.tablefooters.Where(t => t.typerow == EnumType.footer.copyright.ToString() && t.hide == true)
                                            .OrderByDescending(t => t.datebegin)
                                            .FirstOrDefault();
            var connectdiff = db.tablefooters.Where(t => t.typerow == EnumType.footer.connectdiff.ToString() && t.hide == true)
                                .OrderByDescending(t => t.datebegin)
                                .Take(4).ToList();
            var listfooter = new List<FooterCombineTable>();

            foreach (var item in infofooter)
            {
                var parts = db.tablefooterparts.Where(t => t.hide == true && t.idtable == item.id)
                                               .OrderByDescending(t => t.datebegin)
                                               .Take(4).ToList();

                listfooter.Add(new FooterCombineTable
                {
                    footercombine = new Dictionary<tablefooter, List<tablefooterpart>>()
            {
                { item, parts } 
            }
                });
            }
            ViewData["imgcompany"] = imgcompany;
            ViewData["package"] = package;
            ViewData["contact"] = contact;
            ViewData["listfooter"] = listfooter;
            ViewData["copyright"] = copyright;
            ViewData["connectdiff"] = connectdiff;

            return PartialView("GetFooter");
        }



    }
}