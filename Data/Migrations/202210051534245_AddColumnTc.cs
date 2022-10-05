namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnTc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tcs", "Guid", c => c.Guid(nullable: false));
            AddColumn("dbo.Tcs", "NameDll", c => c.String());
        }
        
        public override void Down()
        {
        }
    }
}
