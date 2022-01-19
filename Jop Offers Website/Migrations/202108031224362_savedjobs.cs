namespace Jop_Offers_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class savedjobs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SavedJobs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        userId = c.String(maxLength: 128),
                        jobId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Jobs", t => t.jobId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.userId)
                .Index(t => t.userId)
                .Index(t => t.jobId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SavedJobs", "userId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SavedJobs", "jobId", "dbo.Jobs");
            DropIndex("dbo.SavedJobs", new[] { "jobId" });
            DropIndex("dbo.SavedJobs", new[] { "userId" });
            DropTable("dbo.SavedJobs");
        }
    }
}
