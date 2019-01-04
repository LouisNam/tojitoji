namespace tojitoji.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditColumnTrialBalance : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TrialBalances", "DebitIncurred", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.TrialBalances", "CreditIncurred", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TrialBalances", "CreditIncurred", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.TrialBalances", "DebitIncurred", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
