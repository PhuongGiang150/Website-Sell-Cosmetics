using System.Web.Mvc;

namespace CosmeticsStore.Areas.Admin
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
               "QuanLySanPham",
               "Admin/quan-ly-san-pham",
               new { Controller = "Products", action = "Index", id = UrlParameter.Optional },
              new[] { "CosmeticsStore.Areas.Admin.Controllers" }
           );
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { Controller = "Dashboard", action = "Index", id = UrlParameter.Optional },
               new[] { "CosmeticsStore.Areas.Admin.Controllers" }
            );
        }
    }
}