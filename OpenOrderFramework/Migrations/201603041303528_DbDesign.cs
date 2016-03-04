namespace OpenOrderFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbDesign : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cuisines",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        FoodCourt_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FoodCourts", t => t.FoodCourt_ID)
                .Index(t => t.FoodCourt_ID);
            
            CreateTable(
                "dbo.FoodCourts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        InternalImage = c.Binary(maxLength: 4000),
                        FoodCourtPictureUrl = c.String(maxLength: 1024),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ItemAvailabilities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Day = c.String(nullable: false, maxLength: 4000),
                        Item_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Items", t => t.Item_ID)
                .Index(t => t.Item_ID);
            
            AddColumn("dbo.Items", "Description", c => c.String(maxLength: 500));
            AddColumn("dbo.Items", "IsVeg", c => c.Boolean(nullable: false));
            AddColumn("dbo.Items", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.Items", "Calories", c => c.Int(nullable: false));
            AddColumn("dbo.Items", "PreperationTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Items", "Cuisine_ID", c => c.Int(nullable: false));
            AddColumn("dbo.Items", "Vendor_ID", c => c.Int());
            AddColumn("dbo.Orders", "PickUpTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Orders", "Rating", c => c.Int(nullable: false));
            CreateIndex("dbo.Items", "Cuisine_ID");
            CreateIndex("dbo.Items", "Vendor_ID");
            AddForeignKey("dbo.Items", "Cuisine_ID", "dbo.Cuisines", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Items", "Vendor_ID", "dbo.Vendors", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemAvailabilities", "Item_ID", "dbo.Items");
            DropForeignKey("dbo.Items", "Vendor_ID", "dbo.Vendors");
            DropForeignKey("dbo.Vendors", "FoodCourt_ID", "dbo.FoodCourts");
            DropForeignKey("dbo.Items", "Cuisine_ID", "dbo.Cuisines");
            DropIndex("dbo.ItemAvailabilities", new[] { "Item_ID" });
            DropIndex("dbo.Vendors", new[] { "FoodCourt_ID" });
            DropIndex("dbo.Items", new[] { "Vendor_ID" });
            DropIndex("dbo.Items", new[] { "Cuisine_ID" });
            DropColumn("dbo.Orders", "Rating");
            DropColumn("dbo.Orders", "PickUpTime");
            DropColumn("dbo.Items", "Vendor_ID");
            DropColumn("dbo.Items", "Cuisine_ID");
            DropColumn("dbo.Items", "PreperationTime");
            DropColumn("dbo.Items", "Calories");
            DropColumn("dbo.Items", "Quantity");
            DropColumn("dbo.Items", "IsVeg");
            DropColumn("dbo.Items", "Description");
            DropTable("dbo.ItemAvailabilities");
            DropTable("dbo.FoodCourts");
            DropTable("dbo.Vendors");
            DropTable("dbo.Cuisines");
        }
    }
}
