namespace HotelManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeGuestCMNDUnique : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Guests", new[] { "CMND" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Guests", "CMND", unique: true);
        }
    }
}
