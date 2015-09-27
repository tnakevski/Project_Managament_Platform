using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProjectManagementPlatform.Filters
{
    public class AuthenticationFilter: ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            //get user from session["User"]
            var user = System.Web.HttpContext.Current.Session["User"];

            //check there is user in session
            if (user == null)
            {
                //if not redirect to login
                RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                redirectTargetDictionary.Add("action", "Login");
                redirectTargetDictionary.Add("controller", "Account");
                filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                this.OnActionExecuting(filterContext);
            }

            //if yes continue to action
            this.OnActionExecuting(filterContext);
        }
    }
}