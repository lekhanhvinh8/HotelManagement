namespace HotelManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GuestCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Coefficient = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Guests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CMND = c.Int(nullable: false),
                        Name = c.String(),
                        Address = c.String(),
                        GuestCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GuestCategories", t => t.GuestCategoryId, cascadeDelete: true)
                .Index(t => t.GuestCategoryId);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        InvoiceID = c.Int(nullable: false, identity: true),
                        RoomRentalSlipId = c.Int(nullable: false),
                        GuestId = c.Int(nullable: false),
                        DateOfInvoice = c.DateTime(nullable: false),
                        TotalCost = c.Single(),
                    })
                .PrimaryKey(t => t.InvoiceID)
                .ForeignKey("dbo.Guests", t => t.GuestId, cascadeDelete: true)
                .ForeignKey("dbo.RoomRentalSlips", t => t.RoomRentalSlipId, cascadeDelete: true)
                .Index(t => t.RoomRentalSlipId)
                .Index(t => t.GuestId);
            
            CreateTable(
                "dbo.RoomRentalSlips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomNumber = c.String(),
                        Note = c.String(),
                        RoomCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoomCategories", t => t.RoomCategoryId, cascadeDelete: true)
                .Index(t => t.RoomCategoryId);
            
            CreateTable(
                "dbo.RoomCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UnitPrice = c.Single(nullable: false),
                        MaxNumberOfGuests = c.Int(nullable: false),
                        NumStartSurcharge = c.Int(nullable: false),
                        SurchargeRate = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoomRentalSlipGuests",
                c => new
                    {
                        RoomRentalSlip_Id = c.Int(nullable: false),
                        Guest_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoomRentalSlip_Id, t.Guest_Id })
                .ForeignKey("dbo.RoomRentalSlips", t => t.RoomRentalSlip_Id, cascadeDelete: true)
                .ForeignKey("dbo.Guests", t => t.Guest_Id, cascadeDelete: true)
                .Index(t => t.RoomRentalSlip_Id)
                .Index(t => t.Guest_Id);

            Sql("Insert into RoomCategories(Name, UnitPrice, MaxNumberOfGuests, NumStartSurcharge, SurchargeRate) values('Test', 1, 1, 1, 1)");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoomRentalSlips", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Rooms", "RoomCategoryId", "dbo.RoomCategories");
            DropForeignKey("dbo.Invoices", "RoomRentalSlipId", "dbo.RoomRentalSlips");
            DropForeignKey("dbo.RoomRentalSlipGuests", "Guest_Id", "dbo.Guests");
            DropForeignKey("dbo.RoomRentalSlipGuests", "RoomRentalSlip_Id", "dbo.RoomRentalSlips");
            DropForeignKey("dbo.Invoices", "GuestId", "dbo.Guests");
            DropForeignKey("dbo.Guests", "GuestCategoryId", "dbo.GuestCategories");
            DropIndex("dbo.RoomRentalSlipGuests", new[] { "Guest_Id" });
            DropIndex("dbo.RoomRentalSlipGuests", new[] { "RoomRentalSlip_Id" });
            DropIndex("dbo.Rooms", new[] { "RoomCategoryId" });
            DropIndex("dbo.RoomRentalSlips", new[] { "RoomId" });
            DropIndex("dbo.Invoices", new[] { "GuestId" });
            DropIndex("dbo.Invoices", new[] { "RoomRentalSlipId" });
            DropIndex("dbo.Guests", new[] { "GuestCategoryId" });
            DropTable("dbo.RoomRentalSlipGuests");
            DropTable("dbo.RoomCategories");
            DropTable("dbo.Rooms");
            DropTable("dbo.RoomRentalSlips");
            DropTable("dbo.Invoices");
            DropTable("dbo.Guests");
            DropTable("dbo.GuestCategories");
        }
    }
}
