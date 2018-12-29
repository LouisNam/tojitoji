namespace tojitoji.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddDocumentAndTransactionTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Documents",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    DocumentTypeID = c.String(maxLength: 2, unicode: false),
                    Date = c.DateTime(nullable: false),
                    Description = c.String(),
                    HumanID = c.Int(nullable: false),
                    Serial = c.String(),
                    BillNo = c.String(),
                    BillDate = c.DateTime(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.DocumentTypes", t => t.DocumentTypeID)
                .ForeignKey("dbo.Humans", t => t.HumanID, cascadeDelete: true)
                .Index(t => t.DocumentTypeID)
                .Index(t => t.HumanID);

            CreateTable(
                "dbo.Transactions",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Date = c.DateTime(nullable: false),
                    Description = c.String(),
                    Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    DocumentID = c.Int(nullable: false),
                    HumanID = c.Int(nullable: false),
                    TKN = c.String(maxLength: 10, unicode: false),
                    TKC = c.String(maxLength: 10, unicode: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Documents", t => t.DocumentID, cascadeDelete: true)
                .ForeignKey("dbo.Humans", t => t.HumanID, cascadeDelete: false)
                .Index(t => t.DocumentID)
                .Index(t => t.HumanID);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "HumanID", "dbo.Humans");
            DropForeignKey("dbo.Transactions", "DocumentID", "dbo.Documents");
            DropForeignKey("dbo.Documents", "HumanID", "dbo.Humans");
            DropForeignKey("dbo.Documents", "DocumentTypeID", "dbo.DocumentTypes");
            DropIndex("dbo.Transactions", new[] { "HumanID" });
            DropIndex("dbo.Transactions", new[] { "DocumentID" });
            DropIndex("dbo.Documents", new[] { "HumanID" });
            DropIndex("dbo.Documents", new[] { "DocumentTypeID" });
            DropTable("dbo.Transactions");
            DropTable("dbo.Documents");
        }
    }
}