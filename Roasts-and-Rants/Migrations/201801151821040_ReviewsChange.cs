namespace Roasts_and_Rants.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewsChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "UserId", c => c.String());
            DropColumn("dbo.Reviews", "UserEmail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reviews", "UserEmail", c => c.String());
            DropColumn("dbo.Reviews", "UserId");
        }
    }
}
