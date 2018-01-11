namespace Roasts_and_Rants.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.AddressID)
                .ForeignKey("dbo.Restaurants", t => t.AddressID)
                .Index(t => t.AddressID);
            
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        RestaurantID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.RestaurantID);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewID = c.Int(nullable: false, identity: true),
                        Rating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Content = c.String(),
                        RestaurantID = c.Int(nullable: false),
                        UserEmail = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ReviewID)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserEmail)
                .Index(t => t.RestaurantID)
                .Index(t => t.UserEmail);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        Username = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Email);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "AddressID", "dbo.Restaurants");
            DropForeignKey("dbo.Reviews", "UserEmail", "dbo.Users");
            DropForeignKey("dbo.Reviews", "RestaurantID", "dbo.Restaurants");
            DropIndex("dbo.Reviews", new[] { "UserEmail" });
            DropIndex("dbo.Reviews", new[] { "RestaurantID" });
            DropIndex("dbo.Addresses", new[] { "AddressID" });
            DropTable("dbo.Users");
            DropTable("dbo.Reviews");
            DropTable("dbo.Restaurants");
            DropTable("dbo.Addresses");
        }
    }
}
