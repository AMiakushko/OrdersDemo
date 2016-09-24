namespace KyivGazTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NumberRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Number", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Number", c => c.String(maxLength: 50));
        }
    }
}
