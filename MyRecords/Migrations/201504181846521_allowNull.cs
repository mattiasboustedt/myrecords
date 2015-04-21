namespace MyRecords.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allowNull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Files", "ArtistId", "dbo.Artists");
            DropIndex("dbo.Files", new[] { "ArtistId" });
            AlterColumn("dbo.Files", "ArtistId", c => c.Int());
            CreateIndex("dbo.Files", "ArtistId");
            AddForeignKey("dbo.Files", "ArtistId", "dbo.Artists", "ArtistId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "ArtistId", "dbo.Artists");
            DropIndex("dbo.Files", new[] { "ArtistId" });
            AlterColumn("dbo.Files", "ArtistId", c => c.Int(nullable: false));
            CreateIndex("dbo.Files", "ArtistId");
            AddForeignKey("dbo.Files", "ArtistId", "dbo.Artists", "ArtistId", cascadeDelete: true);
        }
    }
}
