namespace ETicaret.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShowOnHomePage_added_to_Product_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Urun", "ShowOnHomePage", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Urun", "ShowOnHomePage");
        }
    }
}
