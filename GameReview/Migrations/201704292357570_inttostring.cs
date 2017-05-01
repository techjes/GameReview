namespace GameReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inttostring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reviews", "UserName", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "UserName", c => c.Int(nullable: false));
        }
    }
}
