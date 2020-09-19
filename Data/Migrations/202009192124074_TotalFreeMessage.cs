namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TotalFreeMessage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "TotalFreeMessage", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "TotalFreeMessage");
        }
    }
}
