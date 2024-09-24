using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class ListJobController : Controller
    {
        // GET: ListJob
        public ActionResult GetJobList()
        {
            return View();
        }
    }
}