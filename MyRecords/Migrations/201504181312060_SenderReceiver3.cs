namespace MyRecords.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SenderReceiver3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "PostReceiverId", "dbo.PostReceivers");
            DropForeignKey("dbo.Posts", "PostSenderId", "dbo.PostSenders");
            DropIndex("dbo.Posts", new[] { "PostReceiverId" });
            DropIndex("dbo.Posts", new[] { "PostSenderId" });
            AlterColumn("dbo.Posts", "PostReceiverId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Posts", "PostSenderId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Posts", "PostReceiverId");
            CreateIndex("dbo.Posts", "PostSenderId");
            AddForeignKey("dbo.Posts", "PostReceiverId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Posts", "PostSenderId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "PostSenderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "PostReceiverId", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "PostSenderId" });
            DropIndex("dbo.Posts", new[] { "PostReceiverId" });
            AlterColumn("dbo.Posts", "PostSenderId", c => c.Int(nullable: false));
            AlterColumn("dbo.Posts", "PostReceiverId", c => c.Int(nullable: false));
            CreateIndex("dbo.Posts", "PostSenderId");
            CreateIndex("dbo.Posts", "PostReceiverId");
            AddForeignKey("dbo.Posts", "PostSenderId", "dbo.PostSenders", "PostSenderId", cascadeDelete: true);
            AddForeignKey("dbo.Posts", "PostReceiverId", "dbo.PostReceivers", "PostReceiverId", cascadeDelete: true);
        }
    }
}
