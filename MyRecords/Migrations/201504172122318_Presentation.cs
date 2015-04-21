namespace MyRecords.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Presentation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Presentation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Presentation");
        }
    }
}
