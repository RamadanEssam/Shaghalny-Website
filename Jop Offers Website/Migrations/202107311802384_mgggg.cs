namespace Jop_Offers_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mgggg : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "CatogryName", c => c.String());
            AlterColumn("dbo.Categories", "CatogryDescription", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "CatogryDescription", c => c.String(nullable: false));
            AlterColumn("dbo.Categories", "CatogryName", c => c.String(nullable: false));
        }
    }
}
