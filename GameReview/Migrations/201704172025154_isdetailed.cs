namespace GameReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isdetailed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Detailed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "Detailed");
        }
    }
}
