namespace Jop_Offers_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addimgforuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "About", c => c.String());
            AddColumn("dbo.AspNetUsers", "image", c => c.String());
            AddColumn("dbo.AspNetUsers", "YearOfBirth", c => c.String());
            AddColumn("dbo.AspNetUsers", "Governorate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Governorate");
            DropColumn("dbo.AspNetUsers", "YearOfBirth");
            DropColumn("dbo.AspNetUsers", "image");
            DropColumn("dbo.AspNetUsers", "About");
        }
    }
}
