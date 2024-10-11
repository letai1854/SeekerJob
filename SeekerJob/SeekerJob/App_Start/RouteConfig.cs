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
                 name: "Chi tiết tin tức",
                 url: "Chi-tiet-tin-tuc/{meta}/{id}",
                 defaults: new { controller = "NewsDetail", action = "ViewNewsDetail", id = UrlParameter.Optional }
             );
            routes.MapRoute(
                name: "Danh sách việc làm",
                url: "Danh-sach-viec-lam",
                defaults: new { controller = "ListJob", action = "GetJobList", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "Sửa tin tức",
               url: "Sua-tin-tuc/{meta}/{id}",
               defaults: new { controller = "EditNews", action = "IndexPostNews", id = UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "Đổi mật khẩu tuyển dụng",
                url: "doi-mat-khau-tuyen-dung",
                defaults: new { controller = "ChangePassworCompany", action = "IndexViewChangePassword", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Admin quản lý tài khoản",
                url: "admin-quan-ly-tai-khoan",
                defaults: new { controller = "AdminManageUser", action = "IndexUser", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Admin quản lý quảng cáo",
                url: "admin-quan-ly-quang-cao",
                defaults: new { controller = "AdminManageAdvertisement", action = "IndexViewAdverstisement", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Admin quản lý tin tức",
                url: "admin-quan-ly-tin-tuc",
                defaults: new { controller = "AdminManageNews", action = "IndexViewNews", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Admin quản lý bài tuyển dụng",
                url: "admin-quan-ly-bai-tuyen-dung",
                defaults: new { controller = "AdminManagePost", action = "IndexPost", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Đổi mật khẩu thí sinh",
                url: "doi-mat-khau-ung-tuyen",
                defaults: new { controller = "ChangepasswordCandidate", action = "IndexViewChangePassword", id = UrlParameter.Optional }
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
                defaults: new { controller = "ViewListNews", action = "ViewListNew", id = UrlParameter.Optional }
            );
 

           routes.MapRoute(
                name: "Đăng nhập",
                url: "Dang-nhap",
                defaults: new { controller = "Login", action = "IndexLogin", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Hồ sơ công ty",
                url: "ho-so-tuyen-dung",
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
                  name: "Trang chủ",
                  url: "{trang-chu}",
                  defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
              );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
