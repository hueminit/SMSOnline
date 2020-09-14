using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Models.Entities;
using Models.Enums;

namespace Data
{
    public static class DbInitializer
    {
        public static void CreateAppUser(AppDbContext context)
        {
            if (context.Roles.Any())
            {
                var roleManager = new RoleManager<AppRole>(new RoleStore<AppRole>(new AppDbContext()));
                roleManager.Create(new AppRole()
                {
                    Name = "Admin",
                    Description = "Top manager"
                });

                roleManager.Create(new AppRole()
                {
                    Name = "Customer",
                    Description = "Customer"
                });
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                var userManager = new UserManager<AppUser>(new UserStore<AppUser>(new AppDbContext()));
                userManager.Create(new AppUser()
                {
                    UserName = "admin",
                    FullName = "Administrator",
                    Email = "admin@gmail.com",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Gender = Gender.Male,
                    Address = "43 nguyễn chí thanh hà nội"
                }, "123654$");
                var user = userManager.FindByNameAsync("admin");
                userManager.AddToRole(user.Result.Id, "Admin");
                context.SaveChanges();
            }


        }

    }
}
