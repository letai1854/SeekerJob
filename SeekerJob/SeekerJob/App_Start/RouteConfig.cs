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
                name: "Đăng nhập",
                url: "Dang-nhap",
                defaults: new { controller = "Login", action = "IndexLogin", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Hồ sơ công ty",
                url: "Ho-so-cong-ty",
                defaults: new { controller = "ProfileCompany", action = "IndexProfileCompany", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Hồ sơ các nhân",
                url: "Ho-so",
                defaults: new { controller = "ProfileCandidates", action = "IndexProfileCandidate", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Chi tiết việc làm",
                url: "chi-tiet-viec-lam/{meta}/{id}",
                defaults: new { controller = "JobDetail", action = "ViewJobDetail", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Quản lý tin tức",
                url: "quan-ly-tin-tuc",
                defaults: new { controller = "ManagerNews", action = "IndexManagerNews", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Quản lý bài tuyển dụng",
                url: "quan-ly-bai-tuyen-dung",
                defaults: new { controller = "ManagerJob", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Đăng tin tức",
                url: "dang-tin-tuc",
                defaults: new { controller = "PostNews", action = "IndexPostNews", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Việc làm đã lưu",
                url: "Viec-lam-da-luu",
                defaults: new { controller = "SavedJobCandidates", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Đăng tuyển",
                url: "Dang-tuyen",
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
