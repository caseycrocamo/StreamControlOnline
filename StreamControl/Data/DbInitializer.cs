using StreamControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StreamControl.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ScoreBoardContext context)
        {
            
            //context.Database.EnsureCreated();

            // Look for any players.
            if (context.Scoreboards.Any())
            {
                return;   // DB has been seeded
            }
            Style[] styles =
            {
                new Style
                {
                    DivID="Player1",
                    Display = "block",
                    Position = "absolute",
                    Color="red",
                    Left="2%",
                    Top="66%",
                    Width="20%",
                    FontSize="36px",
                    TextAlign="left"
                },
                new Style
                {
                    DivID = "Player2",
                    Display = "block",
                    Position = "absolute",
                    Color = "red",
                    Left = "78%",
                    Top = "66%",
                    Width = "20%",
                    FontSize = "36px",
                    TextAlign = "right"
                },
                new Style
                {
                    DivID = "Round",
                    Display = "block",
                    Position = "absolute",
                    Color = "blue",
                    Left = "5%",
                    Top = "5%",
                    Width = "20%",
                    FontSize = "30px",
                    TextAlign = "center"
                },
                new Style
                {
                    DivID = "Score1",
                    Display = "block",
                    Position = "absolute",
                    Color = "blue",
                    Left = "27%",
                    Top = "66%",
                    Width = "4%",
                    FontSize = "30px",
                    TextAlign = "center"
                },
                new Style
                {
                    DivID = "Score2",
                    Display = "block",
                    Position = "absolute",
                    Color = "blue",
                    Left = "74%",
                    Top = "66%",
                    Width = "4%",
                    FontSize = "30px",
                    TextAlign = "center"
                }
            };
            View[] views =
            {
                new View
                {
                    Name="In Game",
                    Style=styles,
                    Width = 1280,
                    Height = 720
                }
            };

            var players = new Player[]
            {
                new Player{Name= "Sif", Character = "Samus", Label = "Player1"},
                new Player{Name= "n8thegr8", Character = "Fox", Label = "Player2"}
            };
            //foreach(Player p in players)
            //{
            //    context.Players.Add(p);
            //}
            //context.SaveChanges();

            Field[] fields = 
            {
                new Field{Label="Round", Value="Winners Round 1 BO3"},
                new Field{Label="Score1", Value="1"},
                new Field{Label="Score2", Value="2"}
            };
            //foreach (Field f in fields)
            //{
            //    context.Fields.Add(f);
            //}
            //context.SaveChanges();
            Scoreboard scoreboard = new Scoreboard
            {
                Name = "Test Table",
                OwnerID = "test@gmail.com",
                Fields = fields,
                Views = views,
                Players = players
            };
            context.Scoreboards.Add(scoreboard);
            context.SaveChanges();
        }
    }
}