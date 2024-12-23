namespace S2021A6AS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class album_summary_artist_bio : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "Summary", c => c.String(maxLength: 500));
            AddColumn("dbo.Artists", "Biography", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Artists", "Biography");
            DropColumn("dbo.Albums", "Summary");
        }
    }
}
