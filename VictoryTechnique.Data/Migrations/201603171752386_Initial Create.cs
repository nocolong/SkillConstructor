namespace VictoryTechnique.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AreaOfStudies",
                c => new
                    {
                        AreaOfStudyId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.AreaOfStudyId);
            
            CreateTable(
                "dbo.VidSubmissions",
                c => new
                    {
                        VidSubmissionId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        AreaOfStudyId = c.Int(nullable: false),
                        VidSubmissionUrl = c.String(),
                        Description = c.String(),
                        DateOpened = c.DateTime(nullable: false),
                        DateClosed = c.DateTime(),
                    })
                .PrimaryKey(t => t.VidSubmissionId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AreaOfStudies", t => t.AreaOfStudyId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.AreaOfStudyId);
            
            CreateTable(
                "dbo.VidSubmissionTags",
                c => new
                    {
                        VidSubmissionId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.VidSubmissionId, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.VidSubmissions", t => t.VidSubmissionId, cascadeDelete: true)
                .Index(t => t.VidSubmissionId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhotoUrl = c.String(),
                        Email = c.String(),
                        Belt = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VidCritiqueTags",
                c => new
                    {
                        VidCritiqueId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.VidCritiqueId, t.UserId })
                .ForeignKey("dbo.VidCritiques", t => t.VidCritiqueId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.VidCritiqueId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.VidCritiques",
                c => new
                    {
                        VidCritiqueId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        VidSubmissionId = c.Int(nullable: false),
                        VidCritiqueUrl = c.String(),
                        VidCritiqueText = c.String(),
                    })
                .PrimaryKey(t => t.VidCritiqueId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.VidSubmissions", t => t.VidSubmissionId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.VidSubmissionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VidSubmissions", "AreaOfStudyId", "dbo.AreaOfStudies");
            DropForeignKey("dbo.VidCritiques", "VidSubmissionId", "dbo.VidSubmissions");
            DropForeignKey("dbo.VidSubmissionTags", "VidSubmissionId", "dbo.VidSubmissions");
            DropForeignKey("dbo.VidSubmissions", "UserId", "dbo.Users");
            DropForeignKey("dbo.VidCritiques", "UserId", "dbo.Users");
            DropForeignKey("dbo.VidSubmissionTags", "UserId", "dbo.Users");
            DropForeignKey("dbo.VidCritiqueTags", "UserId", "dbo.Users");
            DropForeignKey("dbo.VidCritiqueTags", "VidCritiqueId", "dbo.VidCritiques");
            DropIndex("dbo.VidCritiques", new[] { "VidSubmissionId" });
            DropIndex("dbo.VidCritiques", new[] { "UserId" });
            DropIndex("dbo.VidCritiqueTags", new[] { "UserId" });
            DropIndex("dbo.VidCritiqueTags", new[] { "VidCritiqueId" });
            DropIndex("dbo.VidSubmissionTags", new[] { "UserId" });
            DropIndex("dbo.VidSubmissionTags", new[] { "VidSubmissionId" });
            DropIndex("dbo.VidSubmissions", new[] { "AreaOfStudyId" });
            DropIndex("dbo.VidSubmissions", new[] { "UserId" });
            DropTable("dbo.VidCritiques");
            DropTable("dbo.VidCritiqueTags");
            DropTable("dbo.Users");
            DropTable("dbo.VidSubmissionTags");
            DropTable("dbo.VidSubmissions");
            DropTable("dbo.AreaOfStudies");
        }
    }
}
