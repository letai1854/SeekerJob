using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeekerJob.Models;
using SeekerJob.Services;
namespace SeekerJob.Areas.Admin.Controllers
{
    public class AdvertiseController : Controller
    {
        // GET: Admin/Advertise
        testdbs2425Entities db = new testdbs2425Entities();
        public ActionResult Index()
        {
            var tablepro = db.tablebannerparts.Where(t => t.typeRow == EnumType.Type.advproduct.ToString()).OrderBy(t => t.id).ToList();
            var tablenews = db.tablebannerparts.Where(t => t.typeRow == "advnews").OrderBy(t => t.id).ToList();
            var tablecompany = db.tablebannerparts.Where(t => t.typeRow == EnumType.Type.advcompany.ToString()).OrderBy(t => t.id).ToList();
            ViewData["tablepro"] = tablepro;
            ViewData["tablenews"] = tablenews;
            ViewData["tablecompany"] = tablecompany;
            return PartialView();
        }
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id, int idtable)
        {
            var inforadv = db.tablebannerparts.Where(t => t.idtable==idtable && t.id==id).FirstOrDefault();
            ViewBag.adv = inforadv;
            return PartialView();
        }


        [HttpPost]
        public JsonResult CreateAdvertise(FormCollection Data)
        {
            try
            {
                // Lấy dữ liệu từ form
                string url = Data["url"];
                string type = Data["type"];

                // Xử lý file ảnh
                var file = Request.Files["image"];
                string uniqueFileName = null;

                if (file != null && file.ContentLength > 0)
                {
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/ContentImage/images"), uniqueFileName);
                    file.SaveAs(path);
                }
                else
                {
                    return Json(new { Data = new { status = "ERROR", message = "Vui lòng chọn ảnh" } }, JsonRequestBehavior.AllowGet);
                }

                // Tạo đối tượng Advertisement
                //Advertisement advertisement = new Advertisement()
                //{
                //    image = uniqueFileName,
                //    type = Convert.ToInt32(type),
                //    url = url,
                //    createDate = DateTime.Now,
                //    status = true
                //};
                // Lưu vào database
                IO io = new IO();
                if (type == "1")
                {
                  
                    int check = db.tablebannerparts.Where(t => t.idtable == 11).Count();
                   
                    tablebannerpart table = new tablebannerpart
                    {
                        idtable = 11,
                        content = url,
                        link = uniqueFileName,
                        meta="Quảng cáo 1",
                        hide=true,
                        arrange = check+1,
                        datebegin = DateTime.Now,
                        typeRow= "advproduct"
                    };
                    io.AddObject(table);
                    io.Save();
                }
                if (type == "2")
                {
                    int check = db.tablebannerparts.Where(t => t.idtable == 12).Count();

                    tablebannerpart table = new tablebannerpart
                    {
                        idtable = 12,
                        content = url,
                        link = uniqueFileName,
                        meta = "Quảng cáo 2",
                        hide = true,
                        arrange = check + 1,
                        datebegin = DateTime.Now,
                        typeRow = "advnews"
                    };
                    io.AddObject(table);
                    io.Save();
                }
                if (type == "3")
                {
                    int check = db.tablebannerparts.Where(t => t.idtable == 18).Count();

                    tablebannerpart table = new tablebannerpart
                    {
                        idtable = 18,
                        content = url,
                        link = uniqueFileName,
                        meta = "Quảng cáo 3",
                        hide = true,
                        arrange = check + 1,
                        datebegin = DateTime.Now,
                        typeRow = "advcompany"
                    };
                    io.AddObject(table);
                    io.Save();
                }

                return Json(new { Data = new { status = "OK" } }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Data = new { status = "ERROR", message = ex.Message } }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult Delete1(FormCollection Data)
        {
            int id = Convert.ToInt32(Data["id"]);
            IO io = new IO();
            JsonResult js = new JsonResult();
            Login user = Session["admin"] as Login;

            bool checkDeleteNews = io.DeleteTablebannerpart(id);
            if (checkDeleteNews)
            {
                io.Save();
                js.Data = new
                {
                    status = "OK"
                };
            }
            return Json(js, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete2(FormCollection Data)
        {
            int id = Convert.ToInt32(Data["id"]);
            IO io = new IO();
            JsonResult js = new JsonResult();
            Login user = Session["admin"] as Login;

            bool checkDeleteNews = io.DeleteTablebannerpart(id);
            if (checkDeleteNews)
            {
                io.Save();
                js.Data = new
                {
                    status = "OK"
                };
            }
            return Json(js, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete3(FormCollection Data)
        {
            int id = Convert.ToInt32(Data["id"]);
            IO io = new IO();
            JsonResult js = new JsonResult();
            Login user = Session["admin"] as Login;

            bool checkDeleteNews = io.DeleteTablebannerpart(id);
            if (checkDeleteNews)
            {
                io.Save();
                js.Data = new
                {
                    status = "OK"
                };
            }
            return Json(js, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult EditAdv(FormCollection Data)
        {
            try
            {
                // Lấy dữ liệu từ form
                int id = Convert.ToInt32(Data["id"]);
                string type = Data["type"];
                string url = Data["url"];
                bool hide = Convert.ToBoolean(Data["hide"]);

                // Tìm bản ghi cần sửa
                var banner = db.tablebannerparts.Find(id);
                if (banner == null)
                {
                    return Json(new { Data = new { status = "ERROR", message = "Không tìm thấy quảng cáo" } });
                }

                // Xử lý file ảnh nếu có
                var file = Request.Files["image"];
                if (file != null && file.ContentLength > 0)
                {
                    // Kiểm tra định dạng file
                    string extension = Path.GetExtension(file.FileName).ToLower();
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

                    if (!allowedExtensions.Contains(extension))
                    {
                        return Json(new { Data = new { status = "ERROR", message = "Chỉ chấp nhận file ảnh .jpg, .jpeg, .png, .gif" } });
                    }

                    try
                    {
                        // Tạo tên file mới
                        string uniqueFileName = Guid.NewGuid().ToString() + extension;
                        string uploadPath = Server.MapPath("~/ContentImage/images");
                        string filePath = Path.Combine(uploadPath, uniqueFileName);

                        // Đảm bảo thư mục tồn tại
                        if (!Directory.Exists(uploadPath))
                        {
                            Directory.CreateDirectory(uploadPath);
                        }

                        // Lưu file mới
                        file.SaveAs(filePath);

                        // Xóa file cũ nếu có
                        if (!string.IsNullOrEmpty(banner.link))
                        {
                            string oldFilePath = Path.Combine(uploadPath, banner.link);
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }
                        }

                        // Cập nhật đường dẫn ảnh mới
                        banner.link = uniqueFileName;
                    }
                    catch (Exception ex)
                    {
                        return Json(new { Data = new { status = "ERROR", message = "Lỗi khi xử lý file: " + ex.Message } });
                    }
                }
                if(type=="Quảng cáo 1")
                {
                    banner.meta = type;
                    banner.content = url;
                    banner.hide = hide;
                    banner.datebegin = DateTime.Now;
                    banner.typeRow = "advproduct";
                }
                if(type=="Quảng cáo 2")
                {
                    banner.meta = type;
                    banner.content = url;
                    banner.hide = hide;
                    banner.datebegin = DateTime.Now;
                    banner.typeRow = "advnews";
                }
                if(type=="Quảng cáo 3")
                {
                    banner.meta = type;
                    banner.content = url;
                    banner.hide = hide;
                    banner.datebegin = DateTime.Now;
                    banner.typeRow = "advcompany";
                }
                //// Cập nhật các thông tin khác
               

                try
                {
                    // Lưu thay đổi
                    db.SaveChanges();

                    return Json(new { Data = new { status = "OK" } });
                }
                catch (Exception ex)
                {
                    return Json(new { Data = new { status = "ERROR", message = "Lỗi khi cập nhật database: " + ex.Message } });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Data = new { status = "ERROR", message = "Lỗi: " + ex.Message } });
            }
        }



    }
}