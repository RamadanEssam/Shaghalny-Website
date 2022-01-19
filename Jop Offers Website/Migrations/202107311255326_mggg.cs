namespace Jop_Offers_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mggg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Genders", "gen", c => c.String());
            AddColumn("dbo.Qualifications", "Quali", c => c.String());
            DropColumn("dbo.Genders", "gender");
            DropColumn("dbo.Qualifications", "qualification");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Qualifications", "qualification", c => c.String());
            AddColumn("dbo.Genders", "gender", c => c.String());
            DropColumn("dbo.Qualifications", "Quali");
            DropColumn("dbo.Genders", "gen");
        }
    }
}
