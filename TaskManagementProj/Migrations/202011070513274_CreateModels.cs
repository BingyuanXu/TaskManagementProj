namespace TaskManagementProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Detail = c.String(),
                        ProjectId = c.Int(),
                        TaskId = c.Int(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .ForeignKey("dbo.TaskModels", t => t.TaskId)
                .Index(t => t.ProjectId)
                .Index(t => t.TaskId)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "TaskId", "dbo.TaskModels");
            DropForeignKey("dbo.Notifications", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Notifications", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Notifications", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Notifications", new[] { "TaskId" });
            DropIndex("dbo.Notifications", new[] { "ProjectId" });
            DropTable("dbo.Notifications");
        }
    }
}
