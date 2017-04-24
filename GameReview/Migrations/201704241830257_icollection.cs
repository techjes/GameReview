namespace GameReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class icollection : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GameArts", "GameID", "dbo.Games");
            DropForeignKey("dbo.Reviews", "GameId", "dbo.Games");
            DropIndex("dbo.GameArts", new[] { "GameID" });
            DropIndex("dbo.Reviews", new[] { "GameId" });
            RenameColumn(table: "dbo.GameArts", name: "GameID", newName: "Game_ID");
            RenameColumn(table: "dbo.Reviews", name: "GameId", newName: "Game_ID");
            AlterColumn("dbo.GameArts", "Game_ID", c => c.Int());
            AlterColumn("dbo.Reviews", "Game_ID", c => c.Int());
            CreateIndex("dbo.GameArts", "Game_ID");
            CreateIndex("dbo.Reviews", "Game_ID");
            AddForeignKey("dbo.GameArts", "Game_ID", "dbo.Games", "ID");
            AddForeignKey("dbo.Reviews", "Game_ID", "dbo.Games", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "Game_ID", "dbo.Games");
            DropForeignKey("dbo.GameArts", "Game_ID", "dbo.Games");
            DropIndex("dbo.Reviews", new[] { "Game_ID" });
            DropIndex("dbo.GameArts", new[] { "Game_ID" });
            AlterColumn("dbo.Reviews", "Game_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.GameArts", "Game_ID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Reviews", name: "Game_ID", newName: "GameId");
            RenameColumn(table: "dbo.GameArts", name: "Game_ID", newName: "GameID");
            CreateIndex("dbo.Reviews", "GameId");
            CreateIndex("dbo.GameArts", "GameID");
            AddForeignKey("dbo.Reviews", "GameId", "dbo.Games", "ID", cascadeDelete: true);
            AddForeignKey("dbo.GameArts", "GameID", "dbo.Games", "ID", cascadeDelete: true);
        }
    }
}
