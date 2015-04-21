namespace MyRecords.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Artists", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Artists", "UserId");
            AddForeignKey("dbo.Artists", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Artists", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Artists", new[] { "UserId" });
            DropColumn("dbo.Artists", "UserId");
        }
    }
}
