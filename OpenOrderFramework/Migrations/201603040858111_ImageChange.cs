namespace OpenOrderFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Items", "InternalImage", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Items", "InternalImage", c => c.Binary(maxLength: 4000));
        }
    }
}
