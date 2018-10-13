namespace MyCoach.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MyCoach.Models;
    using Microsoft.AspNet.Identity;

    internal sealed class Configuration : DbMigrationsConfiguration<MyCoach.Models.TrainingModels>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "MyCoach.Models.TrainingModels";
        }

        //protected override void Seed(MyCoach.Models.TrainingModels context)
        //{
        //    base.Seed(context);

        //    var strokeTag = new Tag { Name = "ストローク" };
        //    context.Tags.Add(strokeTag);

        //    var volleyTag = new Tag { Name = "ボレー" };
        //    context.Tags.Add(volleyTag);
        //    context.SaveChanges();

        //    var gohonuchi = new Training
        //    {
        //        Title = "ストローク５本打ち",
        //        Purpose = "ストローク力の強化",
        //        Description = "クロス、ミドル、クロス、ストレートロブ、トップ打ちの順番に練習します。",
        //        AddDateTime = DateTime.Now,
        //        UpdateDateTime = DateTime.Now,
        //        Tags = new List<Tag>()

        //    };

        //    using (var appUserContext = new ApplicationDbContext())
        //    {
        //        var userId = appUserContext.Users.FirstOrDefault(x => x.UserName == "kumata").Id;
        //        gohonuchi.ApplicationUserId = userId;
        //    }
        //    gohonuchi.Tags.Add(strokeTag);
        //    context.Trainings.Add(gohonuchi);
        //    context.SaveChanges();



        //    //  This method will be called after migrating to the latest version.

        //    //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
        //    //  to avoid creating duplicate seed data.
        //}

    }
}
