namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Description", c => c.String(maxLength: 256));
            AlterColumn("dbo.Messages", "Content", c => c.String(maxLength: 120));
            DropColumn("dbo.Contacts", "IsBlock");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contacts", "IsBlock", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Messages", "Content", c => c.String());
            DropColumn("dbo.AspNetUsers", "Description");
        }
    }
}
