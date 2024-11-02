using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekerJob.Areas.Admin.Controllers
{
    public class ImageController : Controller
    {
        // GET: Admin/Image
        [HttpPost]
        public JsonResult UploadImage()
        {
            try
            {
                var file = Request.Files["imageFile"];
                string fileName = null;
                string uniqueFileName = null;

                if (file != null && file.ContentLength > 0)
                {
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/ContentImage/images"), uniqueFileName); // Check this path!
                    file.SaveAs(path);
                    return Json(new { success = true, fileName = fileName,nameImage = uniqueFileName });
                }
                else
                {
                    return Json(new { success = false, message = "No file selected or file empty.", nameImage = "" });
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }



    }
}