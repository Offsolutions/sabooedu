using System.Web.Mvc;

namespace sabooedu.Areas.Auth
{
    public class AuthAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Auth";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Auth_default",
                "Auth/{controller}/{action}/{id}",
                 defaults: new { controller = "Accounts", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}