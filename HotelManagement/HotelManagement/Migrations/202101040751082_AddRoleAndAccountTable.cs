namespace HotelManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoleAndAccountTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(maxLength: 449),
                        PasswordHash = c.String(),
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
            
            Sql("Insert into Roles(RoleName) values('Admin')");
            Sql("Insert into Roles(RoleName) values('Manager')");

            Sql("Insert into Accounts(UserName, PasswordHash, RoleID) values('Admin', 'gFuYE2Bpl7A=', 1)");
            Sql("Insert into Accounts(UserName, PasswordHash, RoleID) values('Manager', 'gFuYE2Bpl7A=', 2)");

        }

        public override void Down()
        {
            Sql("Delete from Accounts");
            Sql("Delete from Roles");

            DropForeignKey("dbo.Accounts", "RoleID", "dbo.Roles");
            DropIndex("dbo.Accounts", new[] { "RoleID" });
            DropIndex("dbo.Accounts", new[] { "Username" });
            DropTable("dbo.Roles");
            DropTable("dbo.Accounts");
        }
    }
}
