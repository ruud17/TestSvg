namespace AgriBook.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDescriptionToCropAndFertilizerAndMetricUnitToParcel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Crops", "Description", c => c.String());
            AddColumn("dbo.Fertilizers", "Description", c => c.String());
            AddColumn("dbo.Parcels", "MetricUnit", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Parcels", "MetricUnit");
            DropColumn("dbo.Fertilizers", "Description");
            DropColumn("dbo.Crops", "Description");
        }
    }
}
