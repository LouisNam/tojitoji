namespace tojitoji.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTrialBalanceTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TrialBalances",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DocumentTypeID = c.String(maxLength: 2, unicode: false),
                        Date = c.DateTime(nullable: false),
                        DocumentID = c.String(maxLength: 20, unicode: false),
                        Description = c.String(),
                        HumanID = c.Int(nullable: false),
                        AccountID = c.String(),
                        CorrespondingAccountID = c.String(),
                        DebitIncurred = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreditIncurred = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TrialBalances");
        }
    }
}
