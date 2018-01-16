namespace Roasts_and_Rants.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUserTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviews", "UserEmail", "dbo.Users");
            DropIndex("dbo.Reviews", new[] { "UserEmail" });
            AlterColumn("dbo.Reviews", "UserEmail", c => c.String());
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        Username = c.String(maxLength: 32),
                        Password = c.String(maxLength: 56),
                    })
                .PrimaryKey(t => t.Email);
            
            AlterColumn("dbo.Reviews", "UserEmail", c => c.String(maxLength: 128));
            CreateIndex("dbo.Reviews", "UserEmail");
            AddForeignKey("dbo.Reviews", "UserEmail", "dbo.Users", "Email");
        }
    }
}
