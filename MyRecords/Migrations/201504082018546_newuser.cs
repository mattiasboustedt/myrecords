namespace MyRecords.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newuser : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Artists", name: "UserName", newName: "UserId");
            RenameIndex(table: "dbo.Artists", name: "IX_UserName", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Artists", name: "IX_UserId", newName: "IX_UserName");
            RenameColumn(table: "dbo.Artists", name: "UserId", newName: "UserName");
        }
    }
}
