namespace S2021A6AS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class artistmediaitem2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ArtistMediaItemContentViewModels", new[] { "Artist_Id" });
            AddColumn("dbo.ArtistMediaItems", "Artist_Id", c => c.Int());
            CreateIndex("dbo.ArtistMediaItems", "Artist_Id");
            DropTable("dbo.ArtistMediaItemContentViewModels");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            DropIndex("dbo.ArtistMediaItems", new[] { "Artist_Id" });
            DropColumn("dbo.ArtistMediaItems", "Artist_Id");
            CreateIndex("dbo.ArtistMediaItemContentViewModels", "Artist_Id");
        }
    }
}
