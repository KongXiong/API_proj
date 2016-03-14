namespace Charts_Proj.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Charts_Proj.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Charts_Proj.Models.ApplicationDbContext context)
        {
            if (!context.Users.Any(u => u.UserName == "admin@admin.com"))
            {
                RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);
                RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);
                UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(context);
                UserManager<ApplicationUser> userManager = new ApplicationUserManager(userStore);
                ApplicationUser admin = new ApplicationUser { UserName = "admin@admin.com" };

                userManager.Create(admin, password: "password");
                roleManager.Create(new IdentityRole { Name = "admin" });
                userManager.AddToRole(admin.Id, "admin");
            }
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
