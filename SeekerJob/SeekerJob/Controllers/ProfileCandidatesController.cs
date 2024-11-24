using SeekerJob.Models;
using SeekerJob.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static SeekerJob.EnumType;

namespace SeekerJob.Controllers
{
    public class ProfileCandidatesController : Controller
    {


        testdbs2425Entities db = new testdbs2425Entities();

        public ActionResult IndexProfileCandidate()
        {
            return View();
        }
        public ActionResult TitleProfileCandidates()
        {
            var tableTitleListJob = db.TitlePages
                                .Where(t => t.hide == true && t.typePage == EnumType.Type.titlepageProfileCandidate.ToString()).OrderBy(t => t.datebegin).FirstOrDefault();

            return PartialView(tableTitleListJob);
        }
        public ActionResult GetImageNameCandidates()
        {
            if (Session["candidate"] is Login candidateUser)
            {
                var tablemenus = db.InforCandidates
                                   .Where(t => t.username == candidateUser.username)
                                   .FirstOrDefault();
                InforCandidate check = tablemenus as InforCandidate;
                ViewData["tablemenus"] = tablemenus;
                ViewData["name"] = check.name;
                return PartialView(tablemenus);

            }
            else if (Session["admin"] is Login adminuser)
            {
                var tablemenus = db.InforCandidates
                                   .Where(t => t.username == adminuser.username)
                                   .FirstOrDefault();
                InforCandidate check = tablemenus as InforCandidate;
                ViewData["tablemenus"] = tablemenus;
                ViewData["name"] = check.name;
                return PartialView(tablemenus);
            }
            else if (Session["employer"] is Login employer)
            {
                var tablemenus = db.InforEmployers
                                   .Where(t => t.username == employer.username)
                                   .FirstOrDefault();
                InforEmployer check = tablemenus as InforEmployer;
                ViewData["tablemenus"] = tablemenus;
                ViewData["name"] = check.namecompany;
                return PartialView(tablemenus);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "User not logged in");
            }

            return new EmptyResult();
        }
        public ActionResult GetListTitle()
        {
            if (Session["candidate"] is SeekerJob.Models.Login user)
            {
                var tablemenus = db.tablebanners.Where(t => t.hide == true && t.typeRow == EnumType.Type.listCandidate.ToString()).OrderBy(t => t.arrange).ToList();
                return PartialView(tablemenus);
            }
            else if (Session["admin"] is SeekerJob.Models.Login admin)
            {
                var tablemenus = db.tablebanners.Where(t => t.hide == true && t.id != 20 && t.typeRow == EnumType.Type.listCandidate.ToString()).OrderBy(t => t.arrange).ToList();
                return PartialView(tablemenus);

            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "User not logged in");
            }
            return new EmptyResult();
        }
        public ActionResult GetInfoCandidate()
        {
            if (Session["candidate"] is Login candidateUser)
            {
                var info = db.InforCandidates.Where(t => t.username == candidateUser.username).FirstOrDefault();
                return PartialView(info);
            }
            else if (Session["admin"] is Login adminuser)
            {
                var info = db.InforCandidates.Where(t => t.username == adminuser.username).FirstOrDefault();
                return PartialView(info);
            }

            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "User not logged in");
            }

            return new EmptyResult();

        }
        [HttpPost]
        public JsonResult UpdateProfile (FormCollection Data)
        {


            // Giải mã nội dung từ CKEditor
            string description = HttpUtility.UrlDecode(Data["brief"]);
            string name = Data["name"];
            string gender = Data["gender"];
            string birthday = Data["birthday"];
            string phone = Data["phone"];
            string email = Data["email"];
            string qualifiaction = Data["qualifiaction"];
            string skill = Data["skill"];
            string jobcategory = Data["jobcategory"];
            string experience = Data["experience"];
            string address = Data["address"];
            string img = Data["img"];
            var file = Request.Files["img"];
            string uniqueFileName = null;
            string facebook = Data["facebook"];
            string twitter = Data["twitter"];
            string linkedin = Data["linkedin"];
            string instagram = Data["instagram"];
            if (file != null && file.ContentLength > 0)
            {
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/ContentImage/images"), uniqueFileName); // Check this path!
                file.SaveAs(path);
            }


            IO io = new IO();
            JsonResult js = new JsonResult();
            Login user = Session["candidate"] as Login;
            InforCandidate info = io.GetInfoCandidte(user.username);
            info.name = name;
            info.description = description;
            info.phone = int.Parse(phone);
            info.email = email;
            info.adrress = address;
            info.facebook = facebook;
            info.twitter = twitter;
            info.linkedin = linkedin;
            info.instagram = instagram;
            info.qualification = qualifiaction;
            info.skill = skill;
            info.jobcategory = jobcategory;
            info.experience = !string.IsNullOrEmpty(experience) ? (double?)double.Parse(experience) : null;
            info.gender = gender;
            info.birthday = ParseExactDateTime(birthday);
            if (uniqueFileName != null)
            {
                info.image = uniqueFileName;
            }
            io.Save();
            js.Data = new
            {
                status = "OK",
                name = name,
                imgnew = uniqueFileName
            };
            return Json(js, JsonRequestBehavior.AllowGet);
        }


        private DateTime? ParseExactDateTime(string dateString)
        {
            if (DateTime.TryParseExact(dateString, "dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out DateTime date))
            {
                return date;
            }
            return null; // Return null if parsing fails
        }


        [HttpPost]
        public JsonResult UploadImage(FormCollection Data)
        {


            string img = Data["img"];
            var file = Request.Files["img"];
            string uniqueFileName = null;

            if (file != null && file.ContentLength > 0)
            {
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/ContentImage/images"), uniqueFileName); // Check this path!
                file.SaveAs(path);
            }



            IO io = new IO();
            JsonResult js = new JsonResult();
            if (Session["candidate"] != null)
            {
                Login usercandidate = Session["candidate"] as Login;
                InforCandidate infocandiate = io.GetInfoCandidte(usercandidate.username);
                infocandiate.image = uniqueFileName;
                io.Save();
            }

            if (Session["admin"] != null)
            {
                Login usercandidate = Session["admin"] as Login;
                InforCandidate infocandiate = io.GetInfoCandidte(usercandidate.username);
                infocandiate.image = uniqueFileName;
                io.Save();
            }

            if (Session["employer"]!=null)
            {
                Login user = Session["employer"] as Login;
                InforEmployer info = io.GetInfoCompany(user.username);
                info.image = uniqueFileName;
                io.Save();
            }
            
            js.Data = new
            {
                status = "OK"
            };
            return Json(js, JsonRequestBehavior.AllowGet);
        }


    }
}