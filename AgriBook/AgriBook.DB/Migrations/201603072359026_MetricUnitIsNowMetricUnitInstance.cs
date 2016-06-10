namespace AgriBook.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MetricUnitIsNowMetricUnitInstance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Parcels", "MetricUnit_Id", c => c.Int());
            CreateIndex("dbo.Parcels", "MetricUnit_Id");
            AddForeignKey("dbo.Parcels", "MetricUnit_Id", "dbo.MetricUnits", "Id");
            DropColumn("dbo.Parcels", "MetricUnit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Parcels", "MetricUnit", c => c.String());
            DropForeignKey("dbo.Parcels", "MetricUnit_Id", "dbo.MetricUnits");
            DropIndex("dbo.Parcels", new[] { "MetricUnit_Id" });
            DropColumn("dbo.Parcels", "MetricUnit_Id");
        }
    }
}
