namespace BakeryCafe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateProd : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ManufacturerProducts", newName: "ProductManufacturers");
            DropForeignKey("dbo.Manufacturers", "CategoryBakery_ID", "dbo.CategoryBakeries");
            DropIndex("dbo.Manufacturers", new[] { "CategoryBakery_ID" });
            DropPrimaryKey("dbo.ProductManufacturers");
            CreateTable(
                "dbo.ManufacturerCategoryBakeries",
                c => new
                    {
                        Manufacturer_ID = c.Int(nullable: false),
                        CategoryBakery_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Manufacturer_ID, t.CategoryBakery_ID })
                .ForeignKey("dbo.Manufacturers", t => t.Manufacturer_ID, cascadeDelete: true)
                .ForeignKey("dbo.CategoryBakeries", t => t.CategoryBakery_ID, cascadeDelete: true)
                .Index(t => t.Manufacturer_ID)
                .Index(t => t.CategoryBakery_ID);
            
            AddColumn("dbo.Products", "dateOfManuf", c => c.DateTime(nullable: false));
            AddPrimaryKey("dbo.ProductManufacturers", new[] { "Product_ID", "Manufacturer_ID" });
            DropColumn("dbo.Manufacturers", "CategoryBakery_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Manufacturers", "CategoryBakery_ID", c => c.Int());
            DropForeignKey("dbo.ManufacturerCategoryBakeries", "CategoryBakery_ID", "dbo.CategoryBakeries");
            DropForeignKey("dbo.ManufacturerCategoryBakeries", "Manufacturer_ID", "dbo.Manufacturers");
            DropIndex("dbo.ManufacturerCategoryBakeries", new[] { "CategoryBakery_ID" });
            DropIndex("dbo.ManufacturerCategoryBakeries", new[] { "Manufacturer_ID" });
            DropPrimaryKey("dbo.ProductManufacturers");
            DropColumn("dbo.Products", "dateOfManuf");
            DropTable("dbo.ManufacturerCategoryBakeries");
            AddPrimaryKey("dbo.ProductManufacturers", new[] { "Manufacturer_ID", "Product_ID" });
            CreateIndex("dbo.Manufacturers", "CategoryBakery_ID");
            AddForeignKey("dbo.Manufacturers", "CategoryBakery_ID", "dbo.CategoryBakeries", "ID");
            RenameTable(name: "dbo.ProductManufacturers", newName: "ManufacturerProducts");
        }
    }
}
