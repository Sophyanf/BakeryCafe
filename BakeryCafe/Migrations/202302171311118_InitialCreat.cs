namespace BakeryCafe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoryBakeries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        AveragePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Manufacturers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ManufacturerName = c.String(),
                        AveragePriceMan = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        productName = c.String(),
                        weight = c.Int(nullable: false),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CategoryBakeries", t => t.Category_ID)
                .Index(t => t.Category_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Category_ID", "dbo.CategoryBakeries");
            DropIndex("dbo.Products", new[] { "Category_ID" });
            DropTable("dbo.Products");
            DropTable("dbo.Manufacturers");
            DropTable("dbo.CategoryBakeries");
        }
    }
}
