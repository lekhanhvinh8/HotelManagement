namespace HotelManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateInvoiceIdNotIncreament : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Invoices");
            AlterColumn("dbo.Invoices", "InvoiceID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Invoices", "InvoiceID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Invoices");
            AlterColumn("dbo.Invoices", "InvoiceID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Invoices", "InvoiceID");
        }
    }
}
