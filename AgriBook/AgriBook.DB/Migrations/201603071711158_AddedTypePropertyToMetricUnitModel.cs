namespace AgriBook.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTypePropertyToMetricUnitModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MetricUnits", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MetricUnits", "Type");
        }
    }
}
