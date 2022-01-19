namespace Jop_Offers_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addcvcol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplyForJobs", "cv", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplyForJobs", "cv");
        }
    }
}
