namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDll : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssemblyInfoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AssemblyInfoes");
        }
    }
}
