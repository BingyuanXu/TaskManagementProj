namespace TaskManagementProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mergeBrach : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TaskModels", "Deadline", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TaskModels", "Deadline", c => c.DateTime(nullable: false));
        }
    }
}
