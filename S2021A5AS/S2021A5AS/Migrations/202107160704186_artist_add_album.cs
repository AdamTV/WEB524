namespace S2021A5AS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class artist_add_album : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlbumAddFormViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Coordinator = c.String(),
                        GenreList_DataGroupField = c.String(),
                        GenreList_DataTextField = c.String(),
                        GenreList_DataValueField = c.String(),
                        ArtistList_DataGroupField = c.String(),
                        ArtistList_DataTextField = c.String(),
                        ArtistList_DataValueField = c.String(),
                        TracksList_DataGroupField = c.String(),
                        TracksList_DataTextField = c.String(),
                        TracksList_DataValueField = c.String(),
                        Name = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        UrlAlbum = c.String(),
                        ArtistName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AlbumAddFormViewModels");
        }
    }
}
