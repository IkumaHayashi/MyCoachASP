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

        //    var strokeTag = new Tag { Name = "�X�g���[�N" };
        //    context.Tags.Add(strokeTag);

        //    var volleyTag = new Tag { Name = "�{���[" };
        //    context.Tags.Add(volleyTag);
        //    context.SaveChanges();

        //    var gohonuchi = new Training
        //    {
        //        Title = "�X�g���[�N�T�{�ł�",
        //        Purpose = "�X�g���[�N�͂̋���",
        //        Description = "�N���X�A�~�h���A�N���X�A�X�g���[�g���u�A�g�b�v�ł��̏��Ԃɗ��K���܂��B",
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
