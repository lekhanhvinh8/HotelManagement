namespace HotelManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSexPropertyToGuest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guests", "Sex", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Guests", "Sex");
        }
    }
}
