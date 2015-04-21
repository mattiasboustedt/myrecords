namespace MyRecords.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateCreatedArtist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Artists", "DateCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Artists", "DateCreated");
        }
    }
}
