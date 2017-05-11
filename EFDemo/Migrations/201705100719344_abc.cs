namespace EFDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "Test3", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blogs", "Test3");
        }
    }
}
