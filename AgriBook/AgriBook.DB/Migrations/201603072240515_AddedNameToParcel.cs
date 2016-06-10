namespace AgriBook.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNameToParcel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Parcels", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Parcels", "Name");
        }
    }
}
