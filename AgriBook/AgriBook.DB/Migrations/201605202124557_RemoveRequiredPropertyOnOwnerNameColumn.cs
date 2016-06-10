namespace AgriBook.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRequiredPropertyOnOwnerNameColumn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Parcels", "OwnerName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Parcels", "OwnerName", c => c.String(nullable: false));
        }
    }
}
