using SeekerJob.Models;
using SeekerJob.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace SeekerJob.Controllers
{
    public class ProfileCompanyController : Controller
    {
        // GET: ProfileCompany


        testdbs2425Entities db = new testdbs2425Entities();

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
            Login login = Session["employer"] as Login;
            var tablemenus = db.InforEmployers.Where(t => t.username == login.username).FirstOrDefault();
            return PartialView(tablemenus);
        }
        public ActionResult GetListTitle()
        {
            var tablemenus = db.tablebanners.Where(t => t.hide == true && t.typeRow == EnumType.Type.list.ToString()).OrderBy(t => t.arrange).ToList();
            return PartialView(tablemenus);
        }
        public ActionResult GetInfoCompany() 
        {

            object info = null;
            Login login = Session["employer"] as Login;
            info = db.InforEmployers.Where(t => t.username == login.username).FirstOrDefault();
            return PartialView(info);
        }
        [HttpPost]
        public JsonResult UpdateInfoCompany(FormCollection Data)
        {
            // Giải mã nội dung từ CKEditor
            string description = HttpUtility.UrlDecode(Data["brief"]);
            string name = Data["name"];
            string phone = Data["phone"];
            string email = Data["email"];
            string country = Data["country"];
            string addressfull = Data["addressfull"];
            string facebook = Data["facebook"];
            string Twitter = Data["Twitter"];
            string linkedin = Data["linkedin"];
            string Instagram = Data["Instagram"];
            string Website = Data["web"];
            


            IO io = new IO();
            JsonResult js = new JsonResult();
            Login user = new Login();
           
            io.Save();
            js.Data = new
            {
                status = "OK"
            };
            return Json(js, JsonRequestBehavior.AllowGet);
        }
    }
}