namespace S2021A6AS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class artistmediaitem1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArtistMediaItemContentViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContentType = c.String(),
                        Content = c.Binary(),
                        Artist_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artists", t => t.Artist_Id)
                .Index(t => t.Artist_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArtistMediaItemContentViewModels", "Artist_Id", "dbo.Artists");
            DropIndex("dbo.ArtistMediaItemContentViewModels", new[] { "Artist_Id" });
            DropTable("dbo.ArtistMediaItemContentViewModels");
        }
    }
}
