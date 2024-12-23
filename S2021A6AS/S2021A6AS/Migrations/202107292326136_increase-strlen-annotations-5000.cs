namespace S2021A6AS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class increasestrlenannotations5000 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Albums", "Summary", c => c.String(maxLength: 5000));
            AlterColumn("dbo.Artists", "Biography", c => c.String(maxLength: 5000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Artists", "Biography", c => c.String(maxLength: 2000));
            AlterColumn("dbo.Albums", "Summary", c => c.String(maxLength: 2000));
        }
    }
}
