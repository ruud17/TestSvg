namespace AgriBook.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Crops",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Fertilizers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Parcels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Points = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Plantings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Season = c.DateTime(nullable: false),
                        Crop_Id = c.Int(),
                        Fertilizer_Id = c.Int(),
                        Parcel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Crops", t => t.Crop_Id)
                .ForeignKey("dbo.Fertilizers", t => t.Fertilizer_Id)
                .ForeignKey("dbo.Parcels", t => t.Parcel_Id)
                .Index(t => t.Crop_Id)
                .Index(t => t.Fertilizer_Id)
                .Index(t => t.Parcel_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Plantings", "Parcel_Id", "dbo.Parcels");
            DropForeignKey("dbo.Plantings", "Fertilizer_Id", "dbo.Fertilizers");
            DropForeignKey("dbo.Plantings", "Crop_Id", "dbo.Crops");
            DropIndex("dbo.Plantings", new[] { "Parcel_Id" });
            DropIndex("dbo.Plantings", new[] { "Fertilizer_Id" });
            DropIndex("dbo.Plantings", new[] { "Crop_Id" });
            DropTable("dbo.Plantings");
            DropTable("dbo.Parcels");
            DropTable("dbo.Fertilizers");
            DropTable("dbo.Crops");
        }
    }
}
