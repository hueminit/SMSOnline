namespace Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RemoveTest : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Tests");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.Tests",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 255),
                })
                .PrimaryKey(t => t.Id);
        }
    }
}