namespace EFDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Users", name: "DisplayName", newName: "display_name");
            DropColumn("dbo.Blogs", "Test3");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Blogs", "Test3", c => c.String());
            RenameColumn(table: "dbo.Users", name: "display_name", newName: "DisplayName");
        }
    }
}
