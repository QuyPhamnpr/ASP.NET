using System.Web.Mvc;

namespace WebBanSmartphones.Areas.AdminQP
{
    public class AdminQPAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AdminQP";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AdminQP_default",
                "AdminQP/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "WebBanSmartphones.Areas.Admin.Controllers" }
            );
        }
    }
}