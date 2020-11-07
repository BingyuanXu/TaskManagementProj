namespace TaskManagementProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangNotification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "UnRead", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "UnRead");
        }
    }
}
