namespace Jop_Offers_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "MinSalary", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "MaxSalary", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "MinAge", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "MaxAge", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "MaxAge");
            DropColumn("dbo.Jobs", "MinAge");
            DropColumn("dbo.Jobs", "MaxSalary");
            DropColumn("dbo.Jobs", "MinSalary");
        }
    }
}
