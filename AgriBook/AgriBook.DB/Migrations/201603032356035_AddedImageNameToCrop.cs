namespace AgriBook.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedImageNameToCrop : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Crops", "ImageName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Crops", "ImageName");
        }
    }
}
