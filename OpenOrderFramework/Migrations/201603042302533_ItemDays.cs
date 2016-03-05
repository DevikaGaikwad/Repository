namespace OpenOrderFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ItemDays : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ItemAvailabilities", "Item_ID", "dbo.Items");
            DropIndex("dbo.ItemAvailabilities", new[] { "Item_ID" });
            AddColumn("dbo.Items", "Mon", c => c.Boolean(nullable: false));
            AddColumn("dbo.Items", "Tue", c => c.Boolean(nullable: false));
            AddColumn("dbo.Items", "Wed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Items", "Thu", c => c.Boolean(nullable: false));
            AddColumn("dbo.Items", "Fri", c => c.Boolean(nullable: false));
            AddColumn("dbo.Items", "Sat", c => c.Boolean(nullable: false));
            AddColumn("dbo.Items", "Sun", c => c.Boolean(nullable: false));
            DropTable("dbo.ItemAvailabilities");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ItemAvailabilities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Day = c.String(nullable: false, maxLength: 4000),
                        Item_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.Items", "Sun");
            DropColumn("dbo.Items", "Sat");
            DropColumn("dbo.Items", "Fri");
            DropColumn("dbo.Items", "Thu");
            DropColumn("dbo.Items", "Wed");
            DropColumn("dbo.Items", "Tue");
            DropColumn("dbo.Items", "Mon");
            CreateIndex("dbo.ItemAvailabilities", "Item_ID");
            AddForeignKey("dbo.ItemAvailabilities", "Item_ID", "dbo.Items", "ID");
        }
    }
}
