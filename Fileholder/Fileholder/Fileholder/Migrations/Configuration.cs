namespace Fileholder.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Fileholder.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //ContextKey = "Fileholder.Models.ApplicationDbContext";
        }

        protected override void Seed(Fileholder.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Users.Add(new Models.ApplicationUser
            {
                //Id = "ADMIN_ID",       
                //Email = null,
                //EmailConfirmed = true,
                //PasswordHash = null,
                //SecurityStamp = null,
                //PhoneNumber = null,
                //PhoneNumberConfirmed = false,
                //TwoFactorEnabled = false,
                //LockoutEndDateUtc = null,
                //LockoutEnabled = false,
                //AccessFailedCount = 0,
                UserName = "Anonymous"
            });
        }
    }
}
