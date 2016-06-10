namespace AgriBook.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedColorToCrop : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Crops", "Color", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Crops", "Color");
        }
    }
}
