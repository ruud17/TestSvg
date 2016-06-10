namespace AgriBook.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedImageUrlPropertyToCropModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Crops", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Crops", "ImageUrl");
        }
    }
}
