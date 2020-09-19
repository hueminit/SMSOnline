namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateContact : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "FullNameContactSent", c => c.String(maxLength: 255));
            AddColumn("dbo.Contacts", "FullNameContactReceived", c => c.String(maxLength: 255));
            DropColumn("dbo.Contacts", "FullName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contacts", "FullName", c => c.String(maxLength: 255));
            DropColumn("dbo.Contacts", "FullNameContactReceived");
            DropColumn("dbo.Contacts", "FullNameContactSent");
        }
    }
}
