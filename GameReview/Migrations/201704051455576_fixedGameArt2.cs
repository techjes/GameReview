namespace GameReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedGameArt2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GameArts", "Game_ID1", "dbo.Games");
            DropForeignKey("dbo.GameArts", "Game_ID2", "dbo.Games");
            DropForeignKey("dbo.GameArts", "Game_ID3", "dbo.Games");
            DropForeignKey("dbo.GameArts", "Game_ID", "dbo.Games");
            DropIndex("dbo.GameArts", new[] { "Game_ID" });
            DropIndex("dbo.GameArts", new[] { "Game_ID1" });
            DropIndex("dbo.GameArts", new[] { "Game_ID2" });
            DropIndex("dbo.GameArts", new[] { "Game_ID3" });
            DropColumn("dbo.GameArts", "GameID");
            RenameColumn(table: "dbo.GameArts", name: "Game_ID", newName: "GameID");
            AddColumn("dbo.GameArts", "Type", c => c.Int(nullable: false));
            AlterColumn("dbo.GameArts", "GameID", c => c.Int(nullable: false));
            CreateIndex("dbo.GameArts", "GameID");
            AddForeignKey("dbo.GameArts", "GameID", "dbo.Games", "ID", cascadeDelete: true);
            DropColumn("dbo.GameArts", "Game_ID1");
            DropColumn("dbo.GameArts", "Game_ID2");
            DropColumn("dbo.GameArts", "Game_ID3");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GameArts", "Game_ID3", c => c.Int());
            AddColumn("dbo.GameArts", "Game_ID2", c => c.Int());
            AddColumn("dbo.GameArts", "Game_ID1", c => c.Int());
            DropForeignKey("dbo.GameArts", "GameID", "dbo.Games");
            DropIndex("dbo.GameArts", new[] { "GameID" });
            AlterColumn("dbo.GameArts", "GameID", c => c.Int());
            DropColumn("dbo.GameArts", "Type");
            RenameColumn(table: "dbo.GameArts", name: "GameID", newName: "Game_ID");
            AddColumn("dbo.GameArts", "GameID", c => c.Int(nullable: false));
            CreateIndex("dbo.GameArts", "Game_ID3");
            CreateIndex("dbo.GameArts", "Game_ID2");
            CreateIndex("dbo.GameArts", "Game_ID1");
            CreateIndex("dbo.GameArts", "Game_ID");
            AddForeignKey("dbo.GameArts", "Game_ID", "dbo.Games", "ID");
            AddForeignKey("dbo.GameArts", "Game_ID3", "dbo.Games", "ID");
            AddForeignKey("dbo.GameArts", "Game_ID2", "dbo.Games", "ID");
            AddForeignKey("dbo.GameArts", "Game_ID1", "dbo.Games", "ID");
        }
    }
}
