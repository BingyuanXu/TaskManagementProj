namespace TaskManagementProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBudgetToProject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Budget", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "Budget");
        }
    }
}
