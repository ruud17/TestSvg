namespace AgriBook.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUniqueConstraintToParcelNameProperty : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Parcels", "Name", c => c.String(maxLength: 450));
            CreateIndex("dbo.Parcels", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Parcels", new[] { "Name" });
            AlterColumn("dbo.Parcels", "Name", c => c.String());
        }
    }
}
