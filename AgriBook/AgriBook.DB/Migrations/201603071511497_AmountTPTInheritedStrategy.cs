namespace AgriBook.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AmountTPTInheritedStrategy : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FertilizerPlantings", "Fertilizer_Id", "dbo.Fertilizers");
            DropForeignKey("dbo.FertilizerPlantings", "Planting_Id", "dbo.Plantings");
            DropForeignKey("dbo.Plantings", "Crop_Id", "dbo.Crops");
            DropIndex("dbo.Plantings", new[] { "Crop_Id" });
            DropIndex("dbo.FertilizerPlantings", new[] { "Fertilizer_Id" });
            DropIndex("dbo.FertilizerPlantings", new[] { "Planting_Id" });
            DropPrimaryKey("dbo.Yields");
            CreateTable(
                "dbo.Amounts",
                c => new
                    {
                        AmountId = c.Int(nullable: false, identity: true),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MetricUnit_Id = c.Int(),
                        Crop_Id = c.Int(),
                        Fertilizer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.AmountId)
                .ForeignKey("dbo.MetricUnits", t => t.MetricUnit_Id)
                .ForeignKey("dbo.Crops", t => t.Crop_Id)
                .ForeignKey("dbo.Fertilizers", t => t.Fertilizer_Id)
                .Index(t => t.MetricUnit_Id)
                .Index(t => t.Crop_Id)
                .Index(t => t.Fertilizer_Id);
            
            CreateTable(
                "dbo.MetricUnits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PlantingCrops",
                c => new
                    {
                        AmountId = c.Int(nullable: false),
                        Crop_Id = c.Int(),
                        Planting_Id = c.Int(),
                    })
                .PrimaryKey(t => t.AmountId)
                .ForeignKey("dbo.Amounts", t => t.AmountId)
                .ForeignKey("dbo.Crops", t => t.Crop_Id)
                .ForeignKey("dbo.Plantings", t => t.Planting_Id)
                .Index(t => t.AmountId)
                .Index(t => t.Crop_Id)
                .Index(t => t.Planting_Id);
            
            CreateTable(
                "dbo.PlantingFertilizers",
                c => new
                    {
                        AmountId = c.Int(nullable: false),
                        Fertilizer_Id = c.Int(),
                        Planting_Id = c.Int(),
                    })
                .PrimaryKey(t => t.AmountId)
                .ForeignKey("dbo.Amounts", t => t.AmountId)
                .ForeignKey("dbo.Fertilizers", t => t.Fertilizer_Id)
                .ForeignKey("dbo.Plantings", t => t.Planting_Id)
                .Index(t => t.AmountId)
                .Index(t => t.Fertilizer_Id)
                .Index(t => t.Planting_Id);
            
            AddColumn("dbo.Yields", "AmountId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Yields", "AmountId");
            CreateIndex("dbo.Yields", "AmountId");
            AddForeignKey("dbo.Yields", "AmountId", "dbo.Amounts", "AmountId");
            DropColumn("dbo.Plantings", "Crop_Id");
            DropColumn("dbo.Yields", "Id");
            DropColumn("dbo.Yields", "Quantity");
            DropColumn("dbo.Yields", "MetricUnit");
            DropTable("dbo.FertilizerPlantings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FertilizerPlantings",
                c => new
                    {
                        Fertilizer_Id = c.Int(nullable: false),
                        Planting_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Fertilizer_Id, t.Planting_Id });
            
            AddColumn("dbo.Yields", "MetricUnit", c => c.String());
            AddColumn("dbo.Yields", "Quantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Yields", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Plantings", "Crop_Id", c => c.Int());
            DropForeignKey("dbo.PlantingFertilizers", "Planting_Id", "dbo.Plantings");
            DropForeignKey("dbo.PlantingFertilizers", "Fertilizer_Id", "dbo.Fertilizers");
            DropForeignKey("dbo.PlantingFertilizers", "AmountId", "dbo.Amounts");
            DropForeignKey("dbo.PlantingCrops", "Planting_Id", "dbo.Plantings");
            DropForeignKey("dbo.PlantingCrops", "Crop_Id", "dbo.Crops");
            DropForeignKey("dbo.PlantingCrops", "AmountId", "dbo.Amounts");
            DropForeignKey("dbo.Yields", "AmountId", "dbo.Amounts");
            DropForeignKey("dbo.Amounts", "Fertilizer_Id", "dbo.Fertilizers");
            DropForeignKey("dbo.Amounts", "Crop_Id", "dbo.Crops");
            DropForeignKey("dbo.Amounts", "MetricUnit_Id", "dbo.MetricUnits");
            DropIndex("dbo.PlantingFertilizers", new[] { "Planting_Id" });
            DropIndex("dbo.PlantingFertilizers", new[] { "Fertilizer_Id" });
            DropIndex("dbo.PlantingFertilizers", new[] { "AmountId" });
            DropIndex("dbo.PlantingCrops", new[] { "Planting_Id" });
            DropIndex("dbo.PlantingCrops", new[] { "Crop_Id" });
            DropIndex("dbo.PlantingCrops", new[] { "AmountId" });
            DropIndex("dbo.Yields", new[] { "AmountId" });
            DropIndex("dbo.Amounts", new[] { "Fertilizer_Id" });
            DropIndex("dbo.Amounts", new[] { "Crop_Id" });
            DropIndex("dbo.Amounts", new[] { "MetricUnit_Id" });
            DropPrimaryKey("dbo.Yields");
            DropColumn("dbo.Yields", "AmountId");
            DropTable("dbo.PlantingFertilizers");
            DropTable("dbo.PlantingCrops");
            DropTable("dbo.MetricUnits");
            DropTable("dbo.Amounts");
            AddPrimaryKey("dbo.Yields", "Id");
            CreateIndex("dbo.FertilizerPlantings", "Planting_Id");
            CreateIndex("dbo.FertilizerPlantings", "Fertilizer_Id");
            CreateIndex("dbo.Plantings", "Crop_Id");
            AddForeignKey("dbo.Plantings", "Crop_Id", "dbo.Crops", "Id");
            AddForeignKey("dbo.FertilizerPlantings", "Planting_Id", "dbo.Plantings", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FertilizerPlantings", "Fertilizer_Id", "dbo.Fertilizers", "Id", cascadeDelete: true);
        }
    }
}
