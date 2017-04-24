namespace GameReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GameTitle = c.String(),
                        Platform = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        Overview = c.String(),
                        ESRB = c.String(),
                        Players = c.Int(nullable: false),
                        CoOp = c.Boolean(nullable: false),
                        Youtube = c.String(),
                        Publisher = c.String(),
                        Rating = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.GameArts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GameID = c.Int(nullable: false),
                        OriginalWidth = c.Int(nullable: false),
                        OriginalHeight = c.Int(nullable: false),
                        URL = c.String(),
                        ThumbURL = c.String(),
                        Game_ID = c.Int(),
                        Game_ID1 = c.Int(),
                        Game_ID2 = c.Int(),
                        Game_ID3 = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Games", t => t.Game_ID)
                .ForeignKey("dbo.Games", t => t.Game_ID1)
                .ForeignKey("dbo.Games", t => t.Game_ID2)
                .ForeignKey("dbo.Games", t => t.Game_ID3)
                .Index(t => t.Game_ID)
                .Index(t => t.Game_ID1)
                .Index(t => t.Game_ID2)
                .Index(t => t.Game_ID3);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Recommended = c.Boolean(nullable: false),
                        HelpfulCount = c.Int(nullable: false),
                        NotHelpfulCount = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.GameArts", "Game_ID3", "dbo.Games");
            DropForeignKey("dbo.Reviews", "GameId", "dbo.Games");
            DropForeignKey("dbo.GameArts", "Game_ID2", "dbo.Games");
            DropForeignKey("dbo.GameArts", "Game_ID1", "dbo.Games");
            DropForeignKey("dbo.GameArts", "Game_ID", "dbo.Games");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Reviews", new[] { "GameId" });
            DropIndex("dbo.GameArts", new[] { "Game_ID3" });
            DropIndex("dbo.GameArts", new[] { "Game_ID2" });
            DropIndex("dbo.GameArts", new[] { "Game_ID1" });
            DropIndex("dbo.GameArts", new[] { "Game_ID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Reviews");
            DropTable("dbo.GameArts");
            DropTable("dbo.Games");
        }
    }
}
