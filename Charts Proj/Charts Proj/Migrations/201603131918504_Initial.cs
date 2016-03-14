namespace Charts_Proj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.Int(nullable: false),
                        Email = c.String(),
                        RegisteredUserID = c.String(),
                        RegisteredUser_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RegisteredUsers", t => t.RegisteredUser_ID)
                .Index(t => t.RegisteredUser_ID);
            
            CreateTable(
                "dbo.RegisteredUsers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
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
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ExpenseCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Expenses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Payee = c.String(),
                        Total = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                        RegisteredUserID = c.String(),
                        ExpenseCategoryID = c.Int(nullable: false),
                        RegisteredUser_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ExpenseCategories", t => t.ExpenseCategoryID, cascadeDelete: true)
                .ForeignKey("dbo.RegisteredUsers", t => t.RegisteredUser_ID)
                .Index(t => t.ExpenseCategoryID)
                .Index(t => t.RegisteredUser_ID);
            
            CreateTable(
                "dbo.RevenueCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Revenues",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Total = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                        RegisteredUserID = c.String(),
                        ClientID = c.Int(),
                        RevenueCategoryID = c.Int(nullable: false),
                        RegisteredUser_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clients", t => t.ClientID)
                .ForeignKey("dbo.RegisteredUsers", t => t.RegisteredUser_ID)
                .ForeignKey("dbo.RevenueCategories", t => t.RevenueCategoryID, cascadeDelete: true)
                .Index(t => t.ClientID)
                .Index(t => t.RevenueCategoryID)
                .Index(t => t.RegisteredUser_ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Revenues", "RevenueCategoryID", "dbo.RevenueCategories");
            DropForeignKey("dbo.Revenues", "RegisteredUser_ID", "dbo.RegisteredUsers");
            DropForeignKey("dbo.Revenues", "ClientID", "dbo.Clients");
            DropForeignKey("dbo.Expenses", "RegisteredUser_ID", "dbo.RegisteredUsers");
            DropForeignKey("dbo.Expenses", "ExpenseCategoryID", "dbo.ExpenseCategories");
            DropForeignKey("dbo.Clients", "RegisteredUser_ID", "dbo.RegisteredUsers");
            DropForeignKey("dbo.RegisteredUsers", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Revenues", new[] { "RegisteredUser_ID" });
            DropIndex("dbo.Revenues", new[] { "RevenueCategoryID" });
            DropIndex("dbo.Revenues", new[] { "ClientID" });
            DropIndex("dbo.Expenses", new[] { "RegisteredUser_ID" });
            DropIndex("dbo.Expenses", new[] { "ExpenseCategoryID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.RegisteredUsers", new[] { "UserID" });
            DropIndex("dbo.Clients", new[] { "RegisteredUser_ID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Revenues");
            DropTable("dbo.RevenueCategories");
            DropTable("dbo.Expenses");
            DropTable("dbo.ExpenseCategories");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.RegisteredUsers");
            DropTable("dbo.Clients");
        }
    }
}
