namespace AgriBook.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedYieldModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Yields",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MetricUnit = c.String(),
                        Planting_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Plantings", t => t.Planting_Id)
                .Index(t => t.Planting_Id);
            
            DropColumn("dbo.Plantings", "Yield");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Plantings", "Yield", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropForeignKey("dbo.Yields", "Planting_Id", "dbo.Plantings");
            DropIndex("dbo.Yields", new[] { "Planting_Id" });
            DropTable("dbo.Yields");
        }
    }
}
