using System.Web.Mvc;

namespace SeekerJob.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
            name: "Admin_ChiTietTinTuc",
            url: "Admin/Chi-tiet-tin-tuc/{meta}/{id}",
            defaults: new { controller = "news", action = "ViewNewsDetail", id = UrlParameter.Optional });
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Default", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}