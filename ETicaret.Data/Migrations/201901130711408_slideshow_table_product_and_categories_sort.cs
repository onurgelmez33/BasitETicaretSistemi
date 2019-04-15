namespace ETicaret.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class slideshow_table_product_and_categories_sort : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Slideshow",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ResimId = c.Int(nullable: false),
                        Baslik = c.String(nullable: true),
                        Aciklama = c.String(nullable: true),
                        ButonAdi = c.String(nullable: true),
                        ButonLink = c.String(nullable: true),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Urun", "Sira", c => c.Int(nullable: false, defaultValue: 0));
            AddColumn("dbo.Kategori", "ShowOnHomePage", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.Kategori", "Sira", c => c.Int(nullable: false, defaultValue: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Kategori", "Sira");
            DropColumn("dbo.Kategori", "ShowOnHomePage");
            DropColumn("dbo.Urun", "Sira");
            DropTable("dbo.Slideshow");
        }
    }
}
