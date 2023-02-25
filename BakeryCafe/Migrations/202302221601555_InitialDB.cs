namespace BakeryCafe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDB : DbMigration
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
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        productName = c.String(),
                        weight = c.Int(nullable: false),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CategoryBakerys_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CategoryBakeries", t => t.CategoryBakerys_ID)
                .Index(t => t.CategoryBakerys_ID);
            
            CreateTable(
                "dbo.Manufacturers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ManufacturerName = c.String(),
                        AveragePriceMan = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CategoryBakery_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CategoryBakeries", t => t.CategoryBakery_ID)
                .Index(t => t.CategoryBakery_ID);
            
            CreateTable(
                "dbo.ManufacturerProducts",
                c => new
                    {
                        Manufacturer_ID = c.Int(nullable: false),
                        Product_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Manufacturer_ID, t.Product_ID })
                .ForeignKey("dbo.Manufacturers", t => t.Manufacturer_ID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_ID, cascadeDelete: true)
                .Index(t => t.Manufacturer_ID)
                .Index(t => t.Product_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ManufacturerProducts", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.ManufacturerProducts", "Manufacturer_ID", "dbo.Manufacturers");
            DropForeignKey("dbo.Manufacturers", "CategoryBakery_ID", "dbo.CategoryBakeries");
            DropForeignKey("dbo.Products", "CategoryBakerys_ID", "dbo.CategoryBakeries");
            DropIndex("dbo.ManufacturerProducts", new[] { "Product_ID" });
            DropIndex("dbo.ManufacturerProducts", new[] { "Manufacturer_ID" });
            DropIndex("dbo.Manufacturers", new[] { "CategoryBakery_ID" });
            DropIndex("dbo.Products", new[] { "CategoryBakerys_ID" });
            DropTable("dbo.ManufacturerProducts");
            DropTable("dbo.Manufacturers");
            DropTable("dbo.Products");
            DropTable("dbo.CategoryBakeries");
        }
    }
}
