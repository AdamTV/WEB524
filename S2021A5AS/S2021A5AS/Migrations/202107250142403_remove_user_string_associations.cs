namespace S2021A5AS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_user_string_associations : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AlbumAddFormViewModels", "Coordinator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AlbumAddFormViewModels", "Coordinator", c => c.String());
        }
    }
}
