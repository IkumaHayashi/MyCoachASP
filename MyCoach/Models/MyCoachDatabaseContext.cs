using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyCoach.Models
{

    public class Configuration : DbMigrationsConfiguration<MyCoachDatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
    }

    public class MyCoachDatabaseContext : IdentityDbContext<ApplicationUser>
    {
        // コンテキストは、アプリケーションの構成ファイル (App.config または Web.config) から 'Trainings' 
        // 接続文字列を使用するように構成されています。既定では、この接続文字列は LocalDb インスタンス上
        // の 'MyCoach.Models.Trainings' データベースを対象としています。 
        // 
        // 別のデータベースとデータベース プロバイダーまたはそのいずれかを対象とする場合は、
        // アプリケーション構成ファイルで 'Trainings' 接続文字列を変更してください。
        public MyCoachDatabaseContext()
            : base("name=MyCoachDatabaseContext")
        {
        }

        public static MyCoachDatabaseContext Create()
        {
            return new MyCoachDatabaseContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        // モデルに含めるエンティティ型ごとに DbSet を追加します。Code First モデルの構成および使用の
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=390109 を参照してください。

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Training> Trainings { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        public virtual DbSet<Favorite> Favorites { get; set; }
    }
}