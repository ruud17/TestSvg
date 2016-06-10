namespace AgriBook.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedGruntIdToParcelModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Parcels", "GruntId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Parcels", "GruntId");
        }
    }
}
