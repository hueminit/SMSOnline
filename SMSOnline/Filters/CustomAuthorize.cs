using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMSOnline.Filters
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AuthorizeAttribute), true))
            {
                //skip authorization check if action has Authorize attribute
                return;
            }

            base.OnAuthorization(filterContext);
        }
    }
}