namespace Services.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriorityAndTask : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Priority",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Level = c.Int(nullable: false),
                        Description = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Task",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PriorityId = c.Int(nullable: false),
                        Description = c.String(maxLength: 250),
                        Completed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Priority", t => t.PriorityId, cascadeDelete: true)
                .Index(t => t.PriorityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Task", "PriorityId", "dbo.Priority");
            DropIndex("dbo.Task", new[] { "PriorityId" });
            DropTable("dbo.Task");
            DropTable("dbo.Priority");
        }
    }
}
