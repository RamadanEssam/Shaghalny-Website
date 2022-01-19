namespace Jop_Offers_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDateofJob : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "jobDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "jobDate");
        }
    }
}
