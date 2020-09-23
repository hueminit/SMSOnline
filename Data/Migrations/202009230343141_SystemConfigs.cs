namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SystemConfigs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SystemConfigs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 50, unicode: false),
                        ValueString = c.String(maxLength: 50),
                        ValueNumber = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SystemConfigs");
        }
    }
}
