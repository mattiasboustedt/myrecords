namespace MyRecords.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class albumUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Albums", "UserId");
            AddForeignKey("dbo.Albums", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Albums", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Albums", new[] { "UserId" });
            DropColumn("dbo.Albums", "UserId");
        }
    }
}
