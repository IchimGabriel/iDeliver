namespace iDeliver.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            // test commit

            CreateTable(
                "dbo.Drivers",
                c => new
                {
                    DriverId = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    OnLine = c.Boolean(),
                    OnDelivery = c.Boolean(),
                    Offline = c.Boolean(),
                })
                .PrimaryKey(t => t.DriverId);

            CreateTable(
                "dbo.Orders",
                c => new
                {
                    OrderId = c.Int(nullable: false, identity: true),
                    TimeStamp = c.DateTime(nullable: false),
                    Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Commission = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Address = c.String(),
                    IsDelivered = c.Boolean(nullable: false),
                    DriverId = c.Int(),
                    ShopId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Drivers", t => t.DriverId)
                .ForeignKey("dbo.Shops", t => t.ShopId, cascadeDelete: true)
                .Index(t => t.DriverId)
                .Index(t => t.ShopId);

            CreateTable(
                "dbo.Shops",
                c => new
                {
                    ShopId = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Open = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ShopId);








            CreateTable(
                "dbo.Drivers",
                c => new
                {
                    DriverId = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    OnLine = c.Boolean(nullable: false),
                    OnDelivery = c.Boolean(nullable: false),
                    Offline = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.DriverId);

            CreateTable(
                "dbo.Orders",
                c => new
                {
                    OrderId = c.Int(nullable: false, identity: true),
                    DateTime = c.DateTime(nullable: false),
                    Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Commission = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Address = c.String(),
                    IsDelivered = c.Boolean(nullable: false),
                    DriverId = c.Int(),
                    ShopId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Drivers", t => t.DriverId)
                .ForeignKey("dbo.Shops", t => t.ShopId, cascadeDelete: true)
                .Index(t => t.DriverId)
                .Index(t => t.ShopId);

            CreateTable(
                "dbo.Shops",
                c => new
                {
                    ShopId = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Open = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ShopId);

            CreateTable(
                "dbo.AspNetRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    RoleId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.AspNetUsers",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Email = c.String(maxLength: 256),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(nullable: false, maxLength: 128),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                {
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Orders", "ShopId", "dbo.Shops");
            DropForeignKey("dbo.Orders", "DriverId", "dbo.Drivers");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Orders", new[] { "ShopId" });
            DropIndex("dbo.Orders", new[] { "DriverId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Shops");
            DropTable("dbo.Orders");
            DropTable("dbo.Drivers");
        }
    }
}
