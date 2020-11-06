namespace TaskManagementProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTitlefieldToTaskModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaskModels", "title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaskModels", "title");
        }
    }
}
