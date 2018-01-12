namespace Roasts_and_Rants.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComputedColumnAverageRating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurants", "AverageRating", c => c.Decimal(nullable: true, precision: 18, scale: 2));

        }
        
        public override void Down()
        {
            DropColumn("dbo.Restaurants", "AverageRating");
        }
    }
}
