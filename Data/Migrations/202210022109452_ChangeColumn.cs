namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssemblyInfoes", "TcName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AssemblyInfoes", "TcName");
        }
    }
}
