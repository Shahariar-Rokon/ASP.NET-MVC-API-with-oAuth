namespace TestAPIV5N1.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TestAPIV5N1.Models.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TestAPIV5N1.Models.AppDbContext context)
        {
            context.Users.AddOrUpdate(
                new Models.User { UserId=1,UserName="Admin",Password="1234",Email="a@gmail.com",Roles="Admin"}
                );
            context.Services.AddOrUpdate(
                new Models.Service { ServiceId=1,ServiceName="Parkin",Price=100}
                );
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
