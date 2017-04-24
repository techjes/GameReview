namespace GameReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class useridreview : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "ApplicationUserID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "ApplicationUserID");
        }
    }
}
