using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity;

namespace MyCoach.Models
{
    public class MyCoachDatabaseInitializer :
        DropCreateDatabaseIfModelChanges<TrainingModels>
        //DropCreateDatabaseAlways<TrainingModels>
    {

        protected override void Seed(MyCoach.Models.TrainingModels context)
        {
            base.Seed(context);


            var strokeTag = new Tag { Name = "ストローク" };
            context.Tags.Add(strokeTag);

            var volleyTag = new Tag { Name = "ボレー" };
            context.Tags.Add(volleyTag);
            context.Tags.Add(new Tag { Name = "サービス" });
            context.Tags.Add(new Tag { Name = "レシーブ" });
            context.Tags.Add(new Tag { Name = "スマッシュ" });
            context.Tags.Add(new Tag { Name = "フォーメーション" });
            context.Tags.Add(new Tag { Name = "シングルス" });
            context.SaveChanges();

            var gohonuchi = new Training
            {
                Title = "ストローク５本打ち",
                Purpose = "ストローク力の強化",
                Description = "クロス、ミドル、クロス、ストレートロブ、トップ打ちの順番に練習します。",
                AddDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now,
                Tags = new List<Tag>()

            };

            using (var appUserContext = new ApplicationDbContext())
            {
                //var user = new ApplicationUser { UserName = "MyCoach事務局", Email = "MyCoach@MyCoach.com" };
                //var result = await UserManager.CreateAsync(user, "Test_01");
                //if (result.Succeeded)
                //{
                //    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                //}
            }
            gohonuchi.ApplicationUserId = "";
            gohonuchi.Tags.Add(strokeTag);
            context.Trainings.Add(gohonuchi);
            context.SaveChanges();



            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}