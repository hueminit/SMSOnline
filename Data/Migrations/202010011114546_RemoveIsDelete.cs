namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveIsDelete : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "IsDelete");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "IsDelete", c => c.Boolean(nullable: false));
        }
    }
}
