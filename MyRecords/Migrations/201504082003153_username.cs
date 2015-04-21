namespace MyRecords.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class username : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Artists", name: "UserId", newName: "UserName");
            RenameIndex(table: "dbo.Artists", name: "IX_UserId", newName: "IX_UserName");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Artists", name: "IX_UserName", newName: "IX_UserId");
            RenameColumn(table: "dbo.Artists", name: "UserName", newName: "UserId");
        }
    }
}
