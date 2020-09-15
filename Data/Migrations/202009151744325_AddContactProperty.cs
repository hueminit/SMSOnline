namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContactProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "IsBlock", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "Balance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Balance");
            DropColumn("dbo.Contacts", "IsBlock");
        }
    }
}
