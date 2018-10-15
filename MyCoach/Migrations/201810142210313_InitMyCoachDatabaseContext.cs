namespace MyCoach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitMyCoachDatabaseContext : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            CreateTable(
                "dbo.Favorites",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(),
                        AddDateTime = c.DateTime(nullable: false),
                        FavoriteTraining_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Trainings", t => t.FavoriteTraining_ID)
                .Index(t => t.FavoriteTraining_ID);
            
            CreateTable(
                "dbo.Trainings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 60),
                        Purpose = c.String(nullable: false, maxLength: 60),
                        Description = c.String(nullable: false),
                        YoutubeURL = c.String(),
                        AddDateTime = c.DateTime(nullable: false),
                        UpdateDateTime = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TagTrainings",
                c => new
                    {
                        Tag_ID = c.Int(nullable: false),
                        Training_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_ID, t.Training_ID })
                .ForeignKey("dbo.Tags", t => t.Tag_ID, cascadeDelete: true)
                .ForeignKey("dbo.Trainings", t => t.Training_ID, cascadeDelete: true)
                .Index(t => t.Tag_ID)
                .Index(t => t.Training_ID);
            
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUserLogins");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId });
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 100),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId });
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Favorites", "FavoriteTraining_ID", "dbo.Trainings");
            DropForeignKey("dbo.TagTrainings", "Training_ID", "dbo.Trainings");
            DropForeignKey("dbo.TagTrainings", "Tag_ID", "dbo.Tags");
            DropIndex("dbo.TagTrainings", new[] { "Training_ID" });
            DropIndex("dbo.TagTrainings", new[] { "Tag_ID" });
            DropIndex("dbo.Favorites", new[] { "FavoriteTraining_ID" });
            DropTable("dbo.TagTrainings");
            DropTable("dbo.Tags");
            DropTable("dbo.Trainings");
            DropTable("dbo.Favorites");
            CreateIndex("dbo.AspNetUserLogins", "UserId");
            CreateIndex("dbo.AspNetUserClaims", "UserId");
            CreateIndex("dbo.AspNetUsers", "UserName", unique: true, name: "UserNameIndex");
            CreateIndex("dbo.AspNetUserRoles", "RoleId");
            CreateIndex("dbo.AspNetUserRoles", "UserId");
            CreateIndex("dbo.AspNetRoles", "Name", unique: true, name: "RoleNameIndex");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
        }
    }
}
