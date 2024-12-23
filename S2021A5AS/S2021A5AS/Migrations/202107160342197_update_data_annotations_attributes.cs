namespace S2021A5AS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_data_annotations_attributes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Album", "Coordinator", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Album", "Genre", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Album", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Album", "UrlAlbum", c => c.String(nullable: false, maxLength: 512));
            AlterColumn("dbo.Track", "Clerk", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Track", "Composers", c => c.String(nullable: false, maxLength: 512));
            AlterColumn("dbo.Track", "Genre", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Track", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Track", "Name", c => c.String());
            AlterColumn("dbo.Track", "Genre", c => c.String());
            AlterColumn("dbo.Track", "Composers", c => c.String());
            AlterColumn("dbo.Track", "Clerk", c => c.String());
            AlterColumn("dbo.Album", "UrlAlbum", c => c.String());
            AlterColumn("dbo.Album", "Name", c => c.String());
            AlterColumn("dbo.Album", "Genre", c => c.String());
            AlterColumn("dbo.Album", "Coordinator", c => c.String());
        }
    }
}
