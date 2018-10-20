using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyCoach.Models
{
    public class MyCoachDatabaseInitializer :
        //DropCreateDatabaseIfModelChanges<MyCoachDatabaseContext>
        DropCreateDatabaseAlways<MyCoachDatabaseContext>
    {
        //public override void InitializeDatabase(MyCoachDatabaseContext context)
        //{
        //    context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction
        //        , string.Format("ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", context.Database.Connection.Database));

        //    base.InitializeDatabase(context);
        //}
        protected override void Seed(MyCoach.Models.MyCoachDatabaseContext context)
        {
            base.Seed(context);

            //タグの追加
            var strokeTag = new Tag { Name = "ストローク" };
            context.Tags.Add(strokeTag);

            var volleyTag = new Tag { Name = "ボレー" };
            context.Tags.Add(volleyTag);

            var serviceTag = new Tag { Name = "サービス" };
            context.Tags.Add(serviceTag);


            var receiveTag = new Tag { Name = "レシーブ" };
            context.Tags.Add(receiveTag);

            var smashTag = new Tag { Name = "スマッシュ" };
            context.Tags.Add(smashTag);

            var formationTag = new Tag { Name = "フォーメーション" };
            context.Tags.Add(formationTag);


            var singlesTag = new Tag { Name = "シングルス" };
            context.Tags.Add(singlesTag);
            context.SaveChanges();


            //ユーザーの追加
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser { UserName = "test01@test.com", Email = "test01@test.com", Name = "test01" };
            var result = userManager.Create(user, "test01");
            user = new ApplicationUser { UserName = "test02@test.com", Email = "test02@test.com", Name = "test02" };
            result = userManager.Create(user, "test02");
            context.SaveChanges();


            var gohonuchi = new Training
            {
                Title = "ストローク５本打ち",
                Purpose = "ストローク力の強化",
                Description = "クロス、ミドル、クロス、ストレートロブ、トップ打ちの順番に練習します。",
                AddDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now,
                Tags = new List<Tag>(),
                ApplicationUserId = user.Id

            };
            gohonuchi.Tags.Add(strokeTag);
            context.Trainings.Add(gohonuchi);
            context.SaveChanges();



            var netPlay = new Training
            {
                Title = "ネットプレー５本打ち",
                Purpose = "ネットプレー力の強化",
                Description = "正面ボレー、ストレートの守りボレー、クロスポーチボレー、スマッシュ、ローボレーの順番に練習します。",
                AddDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now,
                Tags = new List<Tag>(),
                ApplicationUserId = user.Id

            };
            netPlay.Tags.Add(volleyTag);
            netPlay.Tags.Add(smashTag);
            context.Trainings.Add(netPlay);
            context.SaveChanges();



            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}