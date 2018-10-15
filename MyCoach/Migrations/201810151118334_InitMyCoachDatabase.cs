namespace MyCoach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitMyCoachDatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Favorites", "FavoriteTraining_ID", "dbo.Trainings");
            DropIndex("dbo.Favorites", new[] { "FavoriteTraining_ID" });
            RenameColumn(table: "dbo.Favorites", name: "FavoriteTraining_ID", newName: "TrainingID");
            AlterColumn("dbo.Favorites", "TrainingID", c => c.Int(nullable: false));
            CreateIndex("dbo.Favorites", "TrainingID");
            AddForeignKey("dbo.Favorites", "TrainingID", "dbo.Trainings", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Favorites", "TrainingID", "dbo.Trainings");
            DropIndex("dbo.Favorites", new[] { "TrainingID" });
            AlterColumn("dbo.Favorites", "TrainingID", c => c.Int());
            RenameColumn(table: "dbo.Favorites", name: "TrainingID", newName: "FavoriteTraining_ID");
            CreateIndex("dbo.Favorites", "FavoriteTraining_ID");
            AddForeignKey("dbo.Favorites", "FavoriteTraining_ID", "dbo.Trainings", "ID");
        }
    }
}
