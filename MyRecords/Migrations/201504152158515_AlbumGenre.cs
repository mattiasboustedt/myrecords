namespace MyRecords.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlbumGenre : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "Genre", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "Genre");
        }
    }
}
