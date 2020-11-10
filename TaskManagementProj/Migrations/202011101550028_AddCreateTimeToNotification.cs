namespace TaskManagementProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateTimeToNotification : DbMigration
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
