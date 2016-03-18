namespace VictoryTechnique.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeBeltsNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Belt", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Belt", c => c.Int(nullable: false));
        }
    }
}
