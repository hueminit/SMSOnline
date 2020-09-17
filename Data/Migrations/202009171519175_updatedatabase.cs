namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Contacts", new[] { "UserId" });
            DropIndex("dbo.Messages", new[] { "UserId" });
            RenameColumn(table: "dbo.Messages", name: "ContactId", newName: "UserReceivedId");
            RenameColumn(table: "dbo.Contacts", name: "UserId", newName: "ContactReceivedId");
            RenameIndex(table: "dbo.Messages", name: "IX_ContactId", newName: "IX_UserReceivedId");
            AddColumn("dbo.Contacts", "ContactSentId", c => c.String());
            AddColumn("dbo.Messages", "UserSentId", c => c.String());
            AlterColumn("dbo.Contacts", "ContactReceivedId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Contacts", "ContactReceivedId");
            DropColumn("dbo.Messages", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "UserId", c => c.String(nullable: false, maxLength: 128));
            DropIndex("dbo.Contacts", new[] { "ContactReceivedId" });
            AlterColumn("dbo.Contacts", "ContactReceivedId", c => c.String(maxLength: 128));
            DropColumn("dbo.Messages", "UserSentId");
            DropColumn("dbo.Contacts", "ContactSentId");
            RenameIndex(table: "dbo.Messages", name: "IX_UserReceivedId", newName: "IX_ContactId");
            RenameColumn(table: "dbo.Contacts", name: "ContactReceivedId", newName: "UserId");
            RenameColumn(table: "dbo.Messages", name: "UserReceivedId", newName: "ContactId");
            CreateIndex("dbo.Messages", "UserId");
            CreateIndex("dbo.Contacts", "UserId");
            AddForeignKey("dbo.Messages", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
