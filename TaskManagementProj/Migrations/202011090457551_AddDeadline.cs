namespace TaskManagementProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeadline : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaskModels", "Deadline", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaskModels", "Deadline");
        }
    }
}
