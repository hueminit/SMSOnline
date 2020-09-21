namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMessageFullName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "FullNameSent", c => c.String(maxLength: 255));
            AddColumn("dbo.Messages", "FullNameReceived", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "FullNameReceived");
            DropColumn("dbo.Messages", "FullNameSent");
        }
    }
}
