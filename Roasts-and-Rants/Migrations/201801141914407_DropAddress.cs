namespace Roasts_and_Rants.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropAddress : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Addresses", "AddressID", "dbo.Restaurants");
            DropIndex("dbo.Addresses", new[] { "AddressID" });
            AddColumn("dbo.Restaurants", "Street", c => c.String());
            AddColumn("dbo.Restaurants", "City", c => c.String());
            AddColumn("dbo.Restaurants", "State", c => c.String());
            AddColumn("dbo.Restaurants", "Country", c => c.String());
            AddColumn("dbo.Restaurants", "PostalCode", c => c.String());
            DropTable("dbo.Addresses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressID = c.Int(nullable: false),
                        Street = c.String(),
                        City = c.String(),
                        State = c.String(),
                        PostalCode = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.AddressID);
            
            DropColumn("dbo.Restaurants", "PostalCode");
            DropColumn("dbo.Restaurants", "Country");
            DropColumn("dbo.Restaurants", "State");
            DropColumn("dbo.Restaurants", "City");
            DropColumn("dbo.Restaurants", "Street");
            CreateIndex("dbo.Addresses", "AddressID");
            AddForeignKey("dbo.Addresses", "AddressID", "dbo.Restaurants", "RestaurantID");
        }
    }
}
