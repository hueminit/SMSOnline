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
                manager.Create(new IdentityRole()
                {
                    Name = "User",
                });
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
                    PhoneNumber = "0392118580",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Gender = Gender.Male,
                    Address = "238 Hoàng Quốc Việt, Cầu Giấy, Hà Nội",
                    PhoneNumberConfirmed = true,
                    EmailConfirmed = true
                };

                manager.Create(user, "123456$");
                manager.AddToRole(user.Id, "Admin");

                var user1 = new AppUser()
                {
                    UserName = "huent",
                    FullName = "Nguyen Thi Hue",
                    Email = "huent@gmail.com",
                    PhoneNumber="0392118581",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Gender = Gender.Male,
                    Address = "238 Hoàng Quốc Việt, Cầu Giấy, Hà Nội",
                    PhoneNumberConfirmed = true,
                    EmailConfirmed = true
                };
                manager.Create(user1, "123456$");

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