namespace TDevs.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        To = c.String(),
                        CreateAt = c.DateTime(nullable: false),
                        CreateBy = c.String(),
                        Read = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.TicketItems", "Read", c => c.Boolean(nullable: false));
            AddColumn("dbo.Tickets", "Message", c => c.String());
            DropColumn("dbo.Tickets", "HasNewPost");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "HasNewPost", c => c.Boolean(nullable: false));
            DropColumn("dbo.Tickets", "Message");
            DropColumn("dbo.TicketItems", "Read");
            DropTable("dbo.Notifications");
        }
    }
}
