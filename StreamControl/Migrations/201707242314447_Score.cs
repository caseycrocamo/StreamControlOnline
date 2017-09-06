namespace StreamControl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Score : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Scores",
                c => new
                    {
                        ScoreId = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ScoreId);
            
            AddColumn("dbo.Player", "Score_ScoreId", c => c.Int());
            AddColumn("dbo.Style", "Score_ScoreId", c => c.Int());
            CreateIndex("dbo.Player", "Score_ScoreId");
            CreateIndex("dbo.Style", "Score_ScoreId");
            AddForeignKey("dbo.Style", "Score_ScoreId", "dbo.Scores", "ScoreId");
            AddForeignKey("dbo.Player", "Score_ScoreId", "dbo.Scores", "ScoreId");
            DropColumn("dbo.Player", "Score");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Player", "Score", c => c.Int(nullable: false));
            DropForeignKey("dbo.Player", "Score_ScoreId", "dbo.Scores");
            DropForeignKey("dbo.Style", "Score_ScoreId", "dbo.Scores");
            DropIndex("dbo.Style", new[] { "Score_ScoreId" });
            DropIndex("dbo.Player", new[] { "Score_ScoreId" });
            DropColumn("dbo.Style", "Score_ScoreId");
            DropColumn("dbo.Player", "Score_ScoreId");
            DropTable("dbo.Scores");
        }
    }
}
