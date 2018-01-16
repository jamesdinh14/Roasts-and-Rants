namespace Roasts_and_Rants.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserReferenceToReview : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "UserName");
        }
    }
}
