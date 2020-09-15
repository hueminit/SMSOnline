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
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Gender = Gender.Male,
                    Address = "43 nguyễn chí thanh hà nội"
                };

                manager.Create(user, "123654$");
                manager.AddToRole(user.Id, "Admin");
            }
        }

    }
}
