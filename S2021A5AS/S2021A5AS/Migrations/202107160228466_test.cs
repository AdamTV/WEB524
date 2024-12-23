namespace S2021A5AS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Artist", "BirthName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Artist", "Executive", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Artist", "Genre", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Artist", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.Artist", "UrlArtist", c => c.String(nullable: false, maxLength: 512));
            AlterColumn("dbo.Genre", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Genre", "Name", c => c.String());
            AlterColumn("dbo.Artist", "UrlArtist", c => c.String());
            AlterColumn("dbo.Artist", "Name", c => c.String());
            AlterColumn("dbo.Artist", "Genre", c => c.String());
            AlterColumn("dbo.Artist", "Executive", c => c.String());
            AlterColumn("dbo.Artist", "BirthName", c => c.String());
        }
    }
}
