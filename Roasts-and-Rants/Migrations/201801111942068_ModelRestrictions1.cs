namespace Roasts_and_Rants.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelRestrictions1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "ModifiedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Restaurants", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Reviews", "Content", c => c.String(maxLength: 500));
            AlterColumn("dbo.Users", "Username", c => c.String(maxLength: 32));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Username", c => c.String());
            AlterColumn("dbo.Reviews", "Content", c => c.String());
            AlterColumn("dbo.Restaurants", "Name", c => c.String());
            DropColumn("dbo.Reviews", "ModifiedDate");
        }
    }
}
