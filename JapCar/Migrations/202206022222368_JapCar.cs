namespace JapCar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JapCar : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.UsedCars");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UsedCars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PriceStatus = c.String(),
                        CreateDate = c.Int(nullable: false),
                        Mileage = c.Int(nullable: false),
                        WasInCrash = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
