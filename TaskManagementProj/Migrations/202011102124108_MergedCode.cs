namespace TaskManagementProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MergedCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "CreateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "CreateTime");
        }
    }
}
