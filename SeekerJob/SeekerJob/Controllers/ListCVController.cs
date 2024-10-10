﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Controllers
{
    public class ListCVController : Controller
    {
        // GET: ListCV
        MyDB db = new MyDB();
        public ActionResult IndexListCV()
        {
            return View();
        }
        public ActionResult GetListCV()
        {
            var tablecv = db.AddCVs.Where(t=>t.typeCv=="Chuyên nghiệp").ToList();
            return PartialView(tablecv);
        }
        public ActionResult GetListCVTC()
        {
            var tablecv = db.AddCVs.Where(t => t.typeCv == "Tiêu chuẩn").ToList();
            return PartialView(tablecv);
        }

    }
}