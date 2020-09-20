namespace Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Message : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(nullable: false, maxLength: 128),
                    ContactId = c.String(nullable: false, maxLength: 128),
                    DateCreated = c.DateTime(nullable: false),
                    DateModified = c.DateTime(nullable: false),
                    Content = c.String(),
                    Status = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ContactId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ContactId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Messages", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "ContactId", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "ContactId" });
            DropIndex("dbo.Messages", new[] { "UserId" });
            DropTable("dbo.Messages");
        }
    }
}