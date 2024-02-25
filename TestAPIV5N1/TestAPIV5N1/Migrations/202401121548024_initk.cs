namespace TestAPIV5N1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initk : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationId = c.Int(nullable: false, identity: true),
                        ReservationNo = c.String(),
                        ReservationDate = c.DateTime(nullable: false),
                        GuestName = c.String(),
                        IsPaid = c.Boolean(nullable: false),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.ReservationId);
            
            CreateTable(
                "dbo.ReservedItems",
                c => new
                    {
                        ReservedItemId = c.Int(nullable: false, identity: true),
                        ReservationId = c.Int(nullable: false),
                        ServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReservedItemId)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .ForeignKey("dbo.Reservations", t => t.ReservationId, cascadeDelete: true)
                .Index(t => t.ReservationId)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ServiceId = c.Int(nullable: false, identity: true),
                        ServiceName = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ServiceId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        Roles = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReservedItems", "ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.ReservedItems", "ServiceId", "dbo.Services");
            DropIndex("dbo.ReservedItems", new[] { "ServiceId" });
            DropIndex("dbo.ReservedItems", new[] { "ReservationId" });
            DropTable("dbo.Users");
            DropTable("dbo.Services");
            DropTable("dbo.ReservedItems");
            DropTable("dbo.Reservations");
        }
    }
}
