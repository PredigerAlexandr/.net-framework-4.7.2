namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        SurName = c.String(),
                        Phone = c.String(),
                        FirstPlace = c.String(),
                        LastPlace = c.String(),
                        Weight = c.Double(nullable: false),
                        Size = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        Distance = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        TcId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tcs", t => t.TcId, cascadeDelete: true)
                .Index(t => t.TcId);
            
            CreateTable(
                "dbo.Tcs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CoefficientOfKilometer = c.Double(nullable: false),
                        CoefficientOfKilogram = c.Double(nullable: false),
                        CoefficientOfSize = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "TcId", "dbo.Tcs");
            DropIndex("dbo.Orders", new[] { "TcId" });
            DropTable("dbo.Tcs");
            DropTable("dbo.Orders");
        }
    }
}
