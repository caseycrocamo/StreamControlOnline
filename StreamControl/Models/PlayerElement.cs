using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StreamControl.Models
{
    public class PlayerElement
    {
        public int PlayerElementID { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
        public string Character { get; set; }
        public int ScoreboardID { get; set; }

        public PlayerElement() { }

        public PlayerElement(Player player)
        {
            Value = player.Name;
        }
    }
}