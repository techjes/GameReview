namespace GameReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nulldate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Games", "ReleaseDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Games", "ReleaseDate", c => c.DateTime(nullable: false));
        }
    }
}
