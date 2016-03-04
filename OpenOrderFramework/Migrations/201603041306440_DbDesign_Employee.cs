namespace OpenOrderFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbDesign_Employee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        ProfilePicture = c.Binary(),
                        ProfilePictureUrl = c.String(maxLength: 1024),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Orders", "Employee_ID", c => c.Int());
            CreateIndex("dbo.Orders", "Employee_ID");
            AddForeignKey("dbo.Orders", "Employee_ID", "dbo.Employees", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Employee_ID", "dbo.Employees");
            DropIndex("dbo.Orders", new[] { "Employee_ID" });
            DropColumn("dbo.Orders", "Employee_ID");
            DropTable("dbo.Employees");
        }
    }
}
