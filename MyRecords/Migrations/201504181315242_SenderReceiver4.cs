namespace MyRecords.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SenderReceiver4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PostReceivers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PostSenders", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.PostReceivers", new[] { "UserId" });
            DropIndex("dbo.PostSenders", new[] { "UserId" });
            DropTable("dbo.PostReceivers");
            DropTable("dbo.PostSenders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PostSenders",
                c => new
                    {
                        PostSenderId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PostSenderId);
            
            CreateTable(
                "dbo.PostReceivers",
                c => new
                    {
                        PostReceiverId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PostReceiverId);
            
            CreateIndex("dbo.PostSenders", "UserId");
            CreateIndex("dbo.PostReceivers", "UserId");
            AddForeignKey("dbo.PostSenders", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.PostReceivers", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
