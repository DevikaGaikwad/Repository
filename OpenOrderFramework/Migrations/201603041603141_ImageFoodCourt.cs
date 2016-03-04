namespace OpenOrderFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageFoodCourt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FoodCourts", "InternalImage", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FoodCourts", "InternalImage", c => c.Binary(maxLength: 4000));
        }
    }
}
