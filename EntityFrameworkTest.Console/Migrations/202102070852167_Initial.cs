namespace EntityFrameworkTest.Console.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnyChilds",
                c => new
                    {
                        AnyChildID = c.Int(nullable: false, identity: true),
                        Root_RootID = c.Int(),
                    })
                .PrimaryKey(t => t.AnyChildID)
                .ForeignKey("dbo.Roots", t => t.Root_RootID)
                .Index(t => t.Root_RootID);
            
            CreateTable(
                "dbo.AnyEntities",
                c => new
                    {
                        AnyEntityID = c.Int(nullable: false, identity: true),
                        AnyEntityParentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnyEntityID)
                .ForeignKey("dbo.AnyEntityParents", t => t.AnyEntityParentID, cascadeDelete: true)
                .Index(t => t.AnyEntityParentID);
            
            CreateTable(
                "dbo.AnyEntityParents",
                c => new
                    {
                        AnyEntityParentID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.AnyEntityParentID);
            
            CreateTable(
                "dbo.DateChilds",
                c => new
                    {
                        DateChildID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(nullable: false),
                        Root_RootID = c.Int(),
                    })
                .PrimaryKey(t => t.DateChildID)
                .ForeignKey("dbo.Roots", t => t.Root_RootID)
                .Index(t => t.Root_RootID);
            
            CreateTable(
                "dbo.Roots",
                c => new
                    {
                        RootID = c.Int(nullable: false, identity: true),
                        ParentRootID = c.Int(),
                        AnyEntityID = c.Int(nullable: false),
                        ParentRoot_RootID = c.Int(),
                    })
                .PrimaryKey(t => t.RootID)
                .ForeignKey("dbo.AnyEntities", t => t.AnyEntityID, cascadeDelete: true)
                .ForeignKey("dbo.Roots", t => t.ParentRoot_RootID)
                .Index(t => t.AnyEntityID)
                .Index(t => t.ParentRoot_RootID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Roots", "ParentRoot_RootID", "dbo.Roots");
            DropForeignKey("dbo.DateChilds", "Root_RootID", "dbo.Roots");
            DropForeignKey("dbo.Roots", "AnyEntityID", "dbo.AnyEntities");
            DropForeignKey("dbo.AnyChilds", "Root_RootID", "dbo.Roots");
            DropForeignKey("dbo.AnyEntities", "AnyEntityParentID", "dbo.AnyEntityParents");
            DropIndex("dbo.Roots", new[] { "ParentRoot_RootID" });
            DropIndex("dbo.Roots", new[] { "AnyEntityID" });
            DropIndex("dbo.DateChilds", new[] { "Root_RootID" });
            DropIndex("dbo.AnyEntities", new[] { "AnyEntityParentID" });
            DropIndex("dbo.AnyChilds", new[] { "Root_RootID" });
            DropTable("dbo.Roots");
            DropTable("dbo.DateChilds");
            DropTable("dbo.AnyEntityParents");
            DropTable("dbo.AnyEntities");
            DropTable("dbo.AnyChilds");
        }
    }
}
