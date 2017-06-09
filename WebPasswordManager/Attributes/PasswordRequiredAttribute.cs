using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPasswordManager.Attributes
{
    public class PasswordRequiredAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (MvcApplication.Password == null)
            {
                UrlHelper url = new UrlHelper(filterContext.RequestContext);
                var redirectUrl = url.Action("Index", "LogOn");
                filterContext.Result = new RedirectResult(redirectUrl);
            }
        }
    }
}