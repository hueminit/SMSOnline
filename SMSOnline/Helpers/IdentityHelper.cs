using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace SMSOnline.Helpers
{
    public static class IdentityHelper
    {
        public static bool CurrentUserLogged => (System.Web.HttpContext.Current.User != null) &&
                                                System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

        public static string CurrentUserId => System.Web.HttpContext.Current.User.Identity.GetUserId();
        public static string CurrentUserName => System.Web.HttpContext.Current.User.Identity.GetUserName();
    }
}