namespace AgriBook.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedParcelArea : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Parcels", "MetricUnit_Id", "dbo.MetricUnits");
            DropIndex("dbo.Parcels", new[] { "MetricUnit_Id" });
            CreateTable(
                "dbo.ParcelArea",
                c => new
                    {
                        AmountId = c.Int(nullable: false),
                        Parcel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.AmountId)
                .ForeignKey("dbo.Amounts", t => t.AmountId)
                .ForeignKey("dbo.Parcels", t => t.Parcel_Id)
                .Index(t => t.AmountId)
                .Index(t => t.Parcel_Id);
            
            DropColumn("dbo.Parcels", "Area");
            DropColumn("dbo.Parcels", "MetricUnit_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Parcels", "MetricUnit_Id", c => c.Int());
            AddColumn("dbo.Parcels", "Area", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropForeignKey("dbo.ParcelArea", "Parcel_Id", "dbo.Parcels");
            DropForeignKey("dbo.ParcelArea", "AmountId", "dbo.Amounts");
            DropIndex("dbo.ParcelArea", new[] { "Parcel_Id" });
            DropIndex("dbo.ParcelArea", new[] { "AmountId" });
            DropTable("dbo.ParcelArea");
            CreateIndex("dbo.Parcels", "MetricUnit_Id");
            AddForeignKey("dbo.Parcels", "MetricUnit_Id", "dbo.MetricUnits", "Id");
        }
    }
}
