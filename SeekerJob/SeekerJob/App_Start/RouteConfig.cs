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
                name: "Mẫu cv",
                url: "mau-cv",
                defaults: new { controller = "ListCV", action = "IndexListCV", id = UrlParameter.Optional }
            );
            routes.MapRoute(
            name: "ShowFormWithDownload",
            url: "JotForm/ShowFormWithDownload/{formId}",
            defaults: new { controller = "JotForm", action = "ShowFormWithDownload", formId = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Danh sách việc làm",
                url: "Danh-sach-viec-lam",
                defaults: new { controller = "ListJob", action = "GetJobList", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Hồ sơ thí sinh}",
                url: "ho-so-thi-sinh/{id}",
                defaults: new { controller = "ProfileCandidate", action = "ViewProfileCandidate", id = UrlParameter.Optional }
            );
            routes.MapRoute(
              name: "Hồ sơ công ty",
              url: "ho-so-cong-ty/{id}",
              defaults: new { controller = "ProfileEmployer", action = "ViewProfileEmployer", id = UrlParameter.Optional }
          );
            routes.MapRoute(
                name: "Danh sách tin tức",
                url: "xem-tin-tuc",
                defaults: new { controller = "ListNews", action = "ViewListNew", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Chi tiết tin tức",
                url: "Chi-tiet-tin-tuc/{meta}/{id}",
                defaults: new { controller = "NewsDetail", action = "ViewNewsDetail", id = UrlParameter.Optional }
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
