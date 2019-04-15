namespace ETicaret.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Viewed_column_added_to_urun_table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Urun", "Viewed", c => c.Int(nullable: false, defaultValue: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Urun", "Viewed");
        }
    }
}
