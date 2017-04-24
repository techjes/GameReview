namespace GameReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class review : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Reviews", "UserName", c => c.Int(nullable: false));
            AlterColumn("dbo.Reviews", "Content", c => c.String(nullable: false));
            DropColumn("dbo.Reviews", "ApplicationUserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reviews", "ApplicationUserID", c => c.Int(nullable: false));
            AlterColumn("dbo.Reviews", "Content", c => c.String());
            DropColumn("dbo.Reviews", "UserName");
            DropColumn("dbo.Reviews", "DateCreated");
        }
    }
}
