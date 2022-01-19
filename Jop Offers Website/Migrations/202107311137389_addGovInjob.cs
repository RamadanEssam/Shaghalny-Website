namespace Jop_Offers_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addGovInjob : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "GovernoratesId", c => c.Int(nullable: false));
            CreateIndex("dbo.Jobs", "GovernoratesId");
            AddForeignKey("dbo.Jobs", "GovernoratesId", "dbo.Governorates", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "GovernoratesId", "dbo.Governorates");
            DropIndex("dbo.Jobs", new[] { "GovernoratesId" });
            DropColumn("dbo.Jobs", "GovernoratesId");
        }
    }
}
