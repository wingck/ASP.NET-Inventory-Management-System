namespace PassionProject5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class purchases : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        PurchaseID = c.Int(nullable: false, identity: true),
                        PurchaseNum = c.String(),
                    })
                .PrimaryKey(t => t.PurchaseID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Purchases");
        }
    }
}
