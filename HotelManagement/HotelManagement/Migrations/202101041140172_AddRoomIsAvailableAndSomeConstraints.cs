namespace HotelManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoomIsAvailableAndSomeConstraints : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.RoomRentalSlips", new[] { "RoomId" });
            AddColumn("dbo.Rooms", "IsAvailable", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Rooms", "RoomNumber", c => c.String(nullable: false, maxLength: 10));
            CreateIndex("dbo.Guests", "CMND", unique: true);
            CreateIndex("dbo.RoomRentalSlips", new[] { "RoomId", "StartDate", "EndDate" }, unique: true);
            CreateIndex("dbo.Rooms", "RoomNumber", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Rooms", new[] { "RoomNumber" });
            DropIndex("dbo.RoomRentalSlips", new[] { "RoomId", "StartDate", "EndDate" });
            DropIndex("dbo.Guests", new[] { "CMND" });
            AlterColumn("dbo.Rooms", "RoomNumber", c => c.String());
            DropColumn("dbo.Rooms", "IsAvailable");
            CreateIndex("dbo.RoomRentalSlips", "RoomId");
        }
    }
}
