namespace GameReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class apiid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "ApiID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "ApiID");
        }
    }
}
