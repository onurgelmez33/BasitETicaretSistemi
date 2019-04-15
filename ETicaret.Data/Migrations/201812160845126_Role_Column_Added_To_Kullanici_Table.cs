namespace ETicaret.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Role_Column_Added_To_Kullanici_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kullanici", "Role", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Kullanici", "Role");
        }
    }
}
