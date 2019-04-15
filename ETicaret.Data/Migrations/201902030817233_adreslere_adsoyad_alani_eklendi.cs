namespace ETicaret.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adreslere_adsoyad_alani_eklendi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Adres", "AdSoyad", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Adres", "AdSoyad");
        }
    }
}
