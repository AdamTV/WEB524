namespace S2021A5AS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class revertdb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AlbumAddFormViewModels", "Coordinator", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AlbumAddFormViewModels", "Coordinator");
        }
    }
}
