namespace TaskManagementProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateEnumOfPriority : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Priority", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "Priority");
        }
    }
}
