using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
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
            //tai khoan admin
            if (uid == "tien856" && pwd == "123456")
            {
                Session["user"] = 1;
                Session.Timeout = 5;
                jr.Data = new
                {
                    status = "OK"
                };
            }
            if (uid == "nguyenalexJR" && pwd == "123456")
            {
                Session["user"] = "candidate";
                Session.Timeout = 5;
                jr.Data = new
                {
                    status = "OK"
                };
            }
            else if (uid == "taday" && pwd == "123456")
            {
                Session["user"] = 2;
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

            //DBIO bIO = new DBIO();
            //LoginTest login = bIO.GetObject(uid);
            //if (login == null)
            //{
            //    jr.Data = new
            //    {
            //        status = "F"
            //    };
            //}
            //else
            //{
            //    if (login.password == pwd)
            //    {
            //        jr.Data = new
            //        {
            //            status = "OK"
            //        };
            //        Session["user"] = login;
            //        Session.Timeout = 5;
            //    }
            //    else
            //    {
            //        jr.Data = new
            //        {
            //            status = "F"
            //        };
            //    }
            //}


            return Json(jr, JsonRequestBehavior.AllowGet);
        }
    }
}