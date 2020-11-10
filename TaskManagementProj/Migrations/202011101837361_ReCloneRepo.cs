namespace TaskManagementProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReCloneRepo : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Projects", "Priority");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "Priority", c => c.String());
        }
    }
}
