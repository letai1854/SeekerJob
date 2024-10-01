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
                name: "Đăng tuyển",
                url: "post-job",
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
