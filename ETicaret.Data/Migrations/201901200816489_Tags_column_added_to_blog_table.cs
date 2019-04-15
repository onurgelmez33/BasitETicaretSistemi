namespace ETicaret.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tags_column_added_to_blog_table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blog", "Tags", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blog", "Tags");
        }
    }
}
