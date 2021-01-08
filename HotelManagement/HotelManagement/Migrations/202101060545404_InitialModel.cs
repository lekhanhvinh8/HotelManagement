namespace HotelManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 449),
                        PasswordHash = c.String(nullable: false),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.Username, unique: true)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                        Descriptions = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
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
                        Sex = c.Boolean(nullable: false),
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
                        ID = c.String(nullable: false, maxLength: 449),
                        GuestId = c.Int(nullable: false),
                        DateOfInvoice = c.DateTime(nullable: false),
                        TotalCost = c.Single(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Guests", t => t.GuestId, cascadeDelete: true)
                .Index(t => t.GuestId);
            
            CreateTable(
                "dbo.RoomRentalSlips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        InvoiceID = c.String(maxLength: 449),
                        LineTotal = c.Single(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .ForeignKey("dbo.Invoices", t => t.InvoiceID)
                .Index(t => new { t.RoomId, t.StartDate, t.EndDate }, unique: true)
                .Index(t => t.InvoiceID);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomNumber = c.String(nullable: false, maxLength: 10),
                        Note = c.String(),
                        RoomCategoryId = c.Int(nullable: false),
                        IsAvailable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoomCategories", t => t.RoomCategoryId, cascadeDelete: true)
                .Index(t => t.RoomNumber, unique: true)
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

            Sql("Insert Into Roles(RoleName) values('Admin')");
            Sql("Insert Into Roles(RoleName) values('Manager')");

            Sql("Insert Into Accounts(UserName, PasswordHash, RoleID) Values('admin', '0Nm+EkIrk0g=', 1)");
            Sql("Insert Into Accounts(UserName, PasswordHash, RoleID) Values('manager', '0Nm+EkIrk0g=', 2)");

            Sql("Insert Into RoomCategories(Name, UnitPrice, MaxNumberOfGuests, NumStartSurcharge, SurchargeRate) Values('Test', 1, 1, 1, 1)");
        }

        public override void Down()
        {
            Sql("Delete from RoomCategories where Id = 1");
            Sql("Delete from Roles where Id = 1 or Id = 2");
            Sql("Delete from Accounts where Id = 1 or Id = 2");


            DropForeignKey("dbo.RoomRentalSlips", "InvoiceID", "dbo.Invoices");
            DropForeignKey("dbo.RoomRentalSlips", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Rooms", "RoomCategoryId", "dbo.RoomCategories");
            DropForeignKey("dbo.RoomRentalSlipGuests", "Guest_Id", "dbo.Guests");
            DropForeignKey("dbo.RoomRentalSlipGuests", "RoomRentalSlip_Id", "dbo.RoomRentalSlips");
            DropForeignKey("dbo.Invoices", "GuestId", "dbo.Guests");
            DropForeignKey("dbo.Guests", "GuestCategoryId", "dbo.GuestCategories");
            DropForeignKey("dbo.Accounts", "RoleID", "dbo.Roles");
            DropIndex("dbo.RoomRentalSlipGuests", new[] { "Guest_Id" });
            DropIndex("dbo.RoomRentalSlipGuests", new[] { "RoomRentalSlip_Id" });
            DropIndex("dbo.Rooms", new[] { "RoomCategoryId" });
            DropIndex("dbo.Rooms", new[] { "RoomNumber" });
            DropIndex("dbo.RoomRentalSlips", new[] { "InvoiceID" });
            DropIndex("dbo.RoomRentalSlips", new[] { "RoomId", "StartDate", "EndDate" });
            DropIndex("dbo.Invoices", new[] { "GuestId" });
            DropIndex("dbo.Guests", new[] { "GuestCategoryId" });
            DropIndex("dbo.Accounts", new[] { "RoleID" });
            DropIndex("dbo.Accounts", new[] { "Username" });
            DropTable("dbo.RoomRentalSlipGuests");
            DropTable("dbo.RoomCategories");
            DropTable("dbo.Rooms");
            DropTable("dbo.RoomRentalSlips");
            DropTable("dbo.Invoices");
            DropTable("dbo.Guests");
            DropTable("dbo.GuestCategories");
            DropTable("dbo.Roles");
            DropTable("dbo.Accounts");
        }
    }
}
