namespace PassionProject5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class purchaseitem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchases", "ItemID", c => c.Int(nullable: false));
            CreateIndex("dbo.Purchases", "ItemID");
            AddForeignKey("dbo.Purchases", "ItemID", "dbo.Items", "ItemID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Purchases", "ItemID", "dbo.Items");
            DropIndex("dbo.Purchases", new[] { "ItemID" });
            DropColumn("dbo.Purchases", "ItemID");
        }
    }
}
