namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsCancel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "IsCancel", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contacts", "IsCancel");
        }
    }
}
