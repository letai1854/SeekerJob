using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class PostJobController : Controller
    {
        // GET: PostJob
        public ActionResult ShowPostJob()
        {
            return View();
        }
    }
}