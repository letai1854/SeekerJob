using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeekerJob.Models;
namespace SeekerJob.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        testdbs2425Entities db = new testdbs2425Entities();
        public ActionResult IndexLogin()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CheckLoginCandidate(FormCollection collection)
        {
            string uid = collection["uid"];
            string pwd = collection["pwd"];
            JsonResult jr = new JsonResult();

            
            var item = db.Logins.Where(t=>t.username==uid && t.password==pwd && t.typeRow=="Thí sinh").FirstOrDefault();
            if (item != null)
            {
                Session["candidate"] = 1;
                Session.Timeout = 5;
                jr.Data = new
                {
                    status = "OK"
                };
            }
            else
            {
                jr.Data = new
                {
                    status = "F"
                };
            }


            return Json(jr, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult CheckLoginEmployer(FormCollection collection)
        {
            string uid = collection["uid"];
            string pwd = collection["pwd"];
            JsonResult jr = new JsonResult();


            var item = db.Logins.Where(t => t.username == uid && t.password == pwd && t.typeRow == "Tuyển dụng").FirstOrDefault();
            if (item != null)
            {
                Session["employer"] = 1;
                Session.Timeout = 5;
                jr.Data = new
                {
                    status = "OK"
                };
            }
            else
            {
                jr.Data = new
                {
                    status = "F"
                };
            }


            return Json(jr, JsonRequestBehavior.AllowGet);
        }


    }


}