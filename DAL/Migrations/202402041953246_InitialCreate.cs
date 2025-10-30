namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        InofActivity = c.String(),
                        RegActivity = c.DateTime(nullable: false),
                        DeleteStatus = c.Boolean(nullable: false),
                        category_id = c.Int(),
                        customer_id = c.Int(),
                        user_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.ActivityCategories", t => t.category_id)
                .ForeignKey("dbo.Customers", t => t.customer_id)
                .ForeignKey("dbo.Users", t => t.user_id)
                .Index(t => t.category_id)
                .Index(t => t.customer_id)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.ActivityCategories",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        Deletestatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PhoneNumber = c.String(),
                        DeleteStatus = c.Boolean(nullable: false),
                        RegCustomer = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        InvoiceNumber = c.String(),
                        RegInvoice = c.DateTime(nullable: false),
                        IsCheckedout = c.Boolean(nullable: false),
                        CheckoutData = c.DateTime(),
                        customer_id = c.Int(),
                        User_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Customers", t => t.customer_id)
                .ForeignKey("dbo.Users", t => t.User_id)
                .Index(t => t.customer_id)
                .Index(t => t.User_id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        Stock = c.Int(nullable: false),
                        DeleteStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        Pic = c.String(),
                        status = c.Boolean(nullable: false),
                        RegUser = c.DateTime(nullable: false),
                        userGroup_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.UserGroups", t => t.userGroup_id)
                .Index(t => t.userGroup_id);
            
            CreateTable(
                "dbo.Reminders",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        IonfoReminder = c.String(),
                        IsDone = c.Boolean(),
                        RegReminder = c.DateTime(nullable: false),
                        DeleteStatus = c.Boolean(nullable: false),
                        ReminderDate = c.DateTime(nullable: false),
                        users_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.users_id)
                .Index(t => t.users_id);
            
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.UserAccessRoles",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Section = c.String(),
                        CanEnter = c.Boolean(nullable: false),
                        CanCreate = c.Boolean(nullable: false),
                        CanUpdate = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                        userGroup_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.UserGroups", t => t.userGroup_id)
                .Index(t => t.userGroup_id);
            
            CreateTable(
                "dbo.ProductInvoices",
                c => new
                    {
                        Product_id = c.Int(nullable: false),
                        Invoice_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_id, t.Invoice_id })
                .ForeignKey("dbo.Products", t => t.Product_id, cascadeDelete: true)
                .ForeignKey("dbo.Invoices", t => t.Invoice_id, cascadeDelete: true)
                .Index(t => t.Product_id)
                .Index(t => t.Invoice_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "userGroup_id", "dbo.UserGroups");
            DropForeignKey("dbo.UserAccessRoles", "userGroup_id", "dbo.UserGroups");
            DropForeignKey("dbo.Reminders", "users_id", "dbo.Users");
            DropForeignKey("dbo.Invoices", "User_id", "dbo.Users");
            DropForeignKey("dbo.Activities", "user_id", "dbo.Users");
            DropForeignKey("dbo.ProductInvoices", "Invoice_id", "dbo.Invoices");
            DropForeignKey("dbo.ProductInvoices", "Product_id", "dbo.Products");
            DropForeignKey("dbo.Invoices", "customer_id", "dbo.Customers");
            DropForeignKey("dbo.Activities", "customer_id", "dbo.Customers");
            DropForeignKey("dbo.Activities", "category_id", "dbo.ActivityCategories");
            DropIndex("dbo.ProductInvoices", new[] { "Invoice_id" });
            DropIndex("dbo.ProductInvoices", new[] { "Product_id" });
            DropIndex("dbo.UserAccessRoles", new[] { "userGroup_id" });
            DropIndex("dbo.Reminders", new[] { "users_id" });
            DropIndex("dbo.Users", new[] { "userGroup_id" });
            DropIndex("dbo.Invoices", new[] { "User_id" });
            DropIndex("dbo.Invoices", new[] { "customer_id" });
            DropIndex("dbo.Activities", new[] { "user_id" });
            DropIndex("dbo.Activities", new[] { "customer_id" });
            DropIndex("dbo.Activities", new[] { "category_id" });
            DropTable("dbo.ProductInvoices");
            DropTable("dbo.UserAccessRoles");
            DropTable("dbo.UserGroups");
            DropTable("dbo.Reminders");
            DropTable("dbo.Users");
            DropTable("dbo.Products");
            DropTable("dbo.Invoices");
            DropTable("dbo.Customers");
            DropTable("dbo.ActivityCategories");
            DropTable("dbo.Activities");
        }
    }
}
