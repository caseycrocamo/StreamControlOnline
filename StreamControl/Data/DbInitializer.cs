using StreamControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StreamControl.Data
{
    public static class DbInitializer
    {
        public static void Initialize(OverlayContext context)
        {
            
            //context.Database.EnsureCreated();

            // Look for any overlays.
            if (context.Overlays.Any())
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

            context.Players.Add(new Player { Name = "Sif" });
            context.Players.Add(new Player { Name = "n8thegr8" });

            Element[] elements = 
            {
                new TextElement{ Label="Round", Value="Winners Round 1 BO3" },
                new TextElement{ Label="Score1", Value="1" },
                new TextElement{ Label="Score2", Value="2" },
                new PlayerElement(new Player { Name = "Sif" }){ Label="Player1" },
                new PlayerElement(new Player { Name = "Sif" }){ Label="Player2" }
            };

            Overlay overlay = new Overlay
            {
                Name = "Test Table",
                OwnerID = "test@gmail.com",
                Elements = elements,
                Views = views
            };
            context.Overlays.Add(overlay);
            context.SaveChanges();
        }
    }
}