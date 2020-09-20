namespace Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RemoveIsCancel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Contacts", "IsCancel");
        }

        public override void Down()
        {
            AddColumn("dbo.Contacts", "IsCancel", c => c.Boolean(nullable: false));
        }
    }
}