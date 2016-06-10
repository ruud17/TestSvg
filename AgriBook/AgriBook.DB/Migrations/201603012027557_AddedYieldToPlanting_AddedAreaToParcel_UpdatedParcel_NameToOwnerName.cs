namespace AgriBook.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedYieldToPlanting_AddedAreaToParcel_UpdatedParcel_NameToOwnerName : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Plantings", "Fertilizer_Id", "dbo.Fertilizers");
            DropIndex("dbo.Plantings", new[] { "Fertilizer_Id" });
            CreateTable(
                "dbo.FertilizerPlantings",
                c => new
                    {
                        Fertilizer_Id = c.Int(nullable: false),
                        Planting_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Fertilizer_Id, t.Planting_Id })
                .ForeignKey("dbo.Fertilizers", t => t.Fertilizer_Id, cascadeDelete: true)
                .ForeignKey("dbo.Plantings", t => t.Planting_Id, cascadeDelete: true)
                .Index(t => t.Fertilizer_Id)
                .Index(t => t.Planting_Id);
            
            AddColumn("dbo.Parcels", "OwnerName", c => c.String(nullable: false));
            AddColumn("dbo.Parcels", "Area", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Plantings", "Yield", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Parcels", "Name");
            DropColumn("dbo.Plantings", "Fertilizer_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Plantings", "Fertilizer_Id", c => c.Int());
            AddColumn("dbo.Parcels", "Name", c => c.String(nullable: false));
            DropForeignKey("dbo.FertilizerPlantings", "Planting_Id", "dbo.Plantings");
            DropForeignKey("dbo.FertilizerPlantings", "Fertilizer_Id", "dbo.Fertilizers");
            DropIndex("dbo.FertilizerPlantings", new[] { "Planting_Id" });
            DropIndex("dbo.FertilizerPlantings", new[] { "Fertilizer_Id" });
            DropColumn("dbo.Plantings", "Yield");
            DropColumn("dbo.Parcels", "Area");
            DropColumn("dbo.Parcels", "OwnerName");
            DropTable("dbo.FertilizerPlantings");
            CreateIndex("dbo.Plantings", "Fertilizer_Id");
            AddForeignKey("dbo.Plantings", "Fertilizer_Id", "dbo.Fertilizers", "Id");
        }
    }
}
