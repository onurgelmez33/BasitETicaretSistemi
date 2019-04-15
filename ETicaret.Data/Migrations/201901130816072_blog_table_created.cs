namespace ETicaret.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class blog_table_created : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adi = c.String(nullable: false),
                        KisaAciklama = c.String(nullable: false),
                        Aciklama = c.String(nullable: false),
                        ResimId = c.Int(),
                        OlusturulmaTarihi = c.DateTime(nullable: false, defaultValue: DateTime.Now),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Blog");
        }
    }
}
