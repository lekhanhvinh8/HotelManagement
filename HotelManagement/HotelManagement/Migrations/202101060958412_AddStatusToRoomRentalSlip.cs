namespace HotelManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusToRoomRentalSlip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoomRentalSlips", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoomRentalSlips", "Status");
        }
    }
}
