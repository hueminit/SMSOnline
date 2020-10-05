using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Models.Entities;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data
{
    public static class DbInitializer
    {
        public static void CreateUser(AppDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };
                manager.Create(role);
                context.SaveChanges();
            }

            if (!context.Users.Any(u => u.UserName == "admin"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                var user = new AppUser()
                {
                    UserName = "admin",
                    FullName = "Administrator",
                    Email = "admin@gmail.com",
                    PhoneNumber = "0392118585",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Gender = Gender.Male,
                    Address = "238 Hoang Quoc Viet, Cau Giay, Ha Noi",
                    PhoneNumberConfirmed = true,
                    EmailConfirmed = true,
                    Avatar= "/Content/uploads/admin-avatar.png"
                };
                manager.Create(user, "123654$");
                manager.AddToRole(user.Id, "Admin");
            }
        }

        public static void CreateSystemConfig(AppDbContext context)
        {
            if (!context.SystemConfigs.Any())
            {
                var config = new List<SystemConfig>()
                {
                    new SystemConfig()
                    {
                        Code = Common.Constants.MessageFreeKey,
                        ValueNumber = Common.Constants.MessageFreeDefault
                    },
                    new SystemConfig()
                    {
                        Code = Common.Constants.MessagePriceKey,
                        ValueNumber = Common.Constants.MessagePrice
                    }
                };
                context.SystemConfigs.AddRange(config);
                context.SaveChanges();
            }
        }
    }
}