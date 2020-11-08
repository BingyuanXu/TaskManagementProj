namespace TaskManagementProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeNotificationValue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "IsRead", c => c.Boolean(nullable: false));
            DropColumn("dbo.Notifications", "UnRead");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "UnRead", c => c.Boolean(nullable: false));
            DropColumn("dbo.Notifications", "IsRead");
        }
    }
}
