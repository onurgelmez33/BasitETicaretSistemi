namespace ETicaret.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class blog_slug_column_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blog", "Slug", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blog", "Slug");
        }
    }
}
