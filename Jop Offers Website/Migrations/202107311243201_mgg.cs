namespace Jop_Offers_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mgg : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExperienceLevels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExpLevel = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        gender = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Qualifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        qualification = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Jobs", "ExperienceId", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "GenderId", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "QualificationId", c => c.Int(nullable: false));
            CreateIndex("dbo.Jobs", "ExperienceId");
            CreateIndex("dbo.Jobs", "GenderId");
            CreateIndex("dbo.Jobs", "QualificationId");
            AddForeignKey("dbo.Jobs", "ExperienceId", "dbo.ExperienceLevels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Jobs", "GenderId", "dbo.Genders", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Jobs", "QualificationId", "dbo.Qualifications", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "QualificationId", "dbo.Qualifications");
            DropForeignKey("dbo.Jobs", "GenderId", "dbo.Genders");
            DropForeignKey("dbo.Jobs", "ExperienceId", "dbo.ExperienceLevels");
            DropIndex("dbo.Jobs", new[] { "QualificationId" });
            DropIndex("dbo.Jobs", new[] { "GenderId" });
            DropIndex("dbo.Jobs", new[] { "ExperienceId" });
            DropColumn("dbo.Jobs", "QualificationId");
            DropColumn("dbo.Jobs", "GenderId");
            DropColumn("dbo.Jobs", "ExperienceId");
            DropTable("dbo.Qualifications");
            DropTable("dbo.Genders");
            DropTable("dbo.ExperienceLevels");
        }
    }
}
