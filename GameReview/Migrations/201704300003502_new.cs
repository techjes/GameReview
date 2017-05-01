namespace GameReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Reviews", "UserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reviews", "UserName", c => c.String());
        }
    }
}
