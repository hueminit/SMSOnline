﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Web;

namespace SMSOnline.Helpers
{
    public static class IdentityHelper
    {
        public static bool CurrentUserLogged => (System.Web.HttpContext.Current.User != null) &&
                                                System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

        public static string CurrentUserId => System.Web.HttpContext.Current.User.Identity.GetUserId();
        public static string CurrentUserName => System.Web.HttpContext.Current.User.Identity.GetUserName();

        public static Dictionary<string, DateTime> GetLoggedInUsers()
        {
            Dictionary<string, DateTime> loggedInUsers = new Dictionary<string, DateTime>();

            if (HttpContext.Current != null)
            {
                loggedInUsers = (Dictionary<string, DateTime>)HttpContext.Current.Application["loggedinusers"];
                if (loggedInUsers == null)
                {
                    loggedInUsers = new Dictionary<string, DateTime>();
                    HttpContext.Current.Application["loggedinusers"] = loggedInUsers;
                }
            }
            return loggedInUsers;
        }

        public static bool IsUserLogged(string userName)
        {
            var users = GetLoggedInUsers();
            if (users != null && users.Count > 0)
            {
                return users.ContainsKey(userName);
            }
            return false;
        }

        public static AppUser GetAllInfoCurrentLogged()
        {
            var id = System.Web.HttpContext.Current.User.Identity.GetUserId();
            AppUser user = System.Web.HttpContext.Current?.GetOwinContext()?.GetUserManager<ApplicationUserManager>()
                .FindById(id);
            if(user != null)
            {
                return user;
            }
            return null;
        }

        public static AppUser GetUserById(string userId)
        {
            AppUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()
                .FindById(userId);

            return user;
        }
    }
}