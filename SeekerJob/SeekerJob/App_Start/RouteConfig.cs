using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SeekerJob
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Danh sách việc làm",
                url: "Danh-sach-viec-lam",
                defaults: new { controller = "ListJob", action = "GetJobList", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Quản lý bài tuyển dụng",
                url: "quan-ly-bai-tuyen-dung",
                defaults: new { controller = "ManagerJob", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Việc làm đã lưu",
                url: "Viec-lam-da-luu",
                defaults: new { controller = "SavedJobCandidates", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Đăng tuyển",
                url: "Dang-tuyen-viec-lam",
                defaults: new { controller = "PostJob", action = "ShowPostJob", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
