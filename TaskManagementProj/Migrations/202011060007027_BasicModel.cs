namespace TaskManagementProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BasicModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Detail = c.String(),
                        UserId = c.String(maxLength: 128),
                        IsCompleted = c.Boolean(nullable: false),
                        CreatDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TaskModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        Detail = c.String(),
                        FinishedComment = c.String(),
                        CompletePercentage = c.Int(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                        UserId = c.String(maxLength: 128),
                        CreatDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ProjectId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UrgentNotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Detail = c.String(),
                        UserId = c.String(maxLength: 128),
                        TaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaskModels", t => t.TaskId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.TaskId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UrgentNotes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TaskModels", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Projects", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UrgentNotes", "TaskId", "dbo.TaskModels");
            DropForeignKey("dbo.TaskModels", "ProjectId", "dbo.Projects");
            DropIndex("dbo.UrgentNotes", new[] { "TaskId" });
            DropIndex("dbo.UrgentNotes", new[] { "UserId" });
            DropIndex("dbo.TaskModels", new[] { "UserId" });
            DropIndex("dbo.TaskModels", new[] { "ProjectId" });
            DropIndex("dbo.Projects", new[] { "UserId" });
            DropTable("dbo.UrgentNotes");
            DropTable("dbo.TaskModels");
            DropTable("dbo.Projects");
        }
    }
}
