namespace BakeryCafe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correctTabls : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Products", name: "CategoryBakery_ID", newName: "CategoryBakerys_ID");
            RenameIndex(table: "dbo.Products", name: "IX_CategoryBakery_ID", newName: "IX_CategoryBakerys_ID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Products", name: "IX_CategoryBakerys_ID", newName: "IX_CategoryBakery_ID");
            RenameColumn(table: "dbo.Products", name: "CategoryBakerys_ID", newName: "CategoryBakery_ID");
        }
    }
}
