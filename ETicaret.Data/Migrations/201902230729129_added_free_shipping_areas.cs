namespace ETicaret.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_free_shipping_areas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Urun", "KargoDurum", c => c.Boolean(nullable: false));
            AddColumn("dbo.Urun", "KargoFiyat", c => c.Decimal(precision: 18, scale: 2, nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Urun", "KargoFiyat");
            DropColumn("dbo.Urun", "KargoDurum");
        }
    }
}
