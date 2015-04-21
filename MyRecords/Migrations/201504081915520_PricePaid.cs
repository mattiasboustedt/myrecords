namespace MyRecords.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PricePaid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "PricePaid", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "PricePaid");
        }
    }
}
