namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Messages", newName: "Tests");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Tests", newName: "Messages");
        }
    }
}
