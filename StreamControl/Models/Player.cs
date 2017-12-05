using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StreamControl.Models
{
    public class Player
    {
        public int PlayerID { get; set; }
        public string Label { get; set; }
        public string Name { get; set; }
        public string Character { get; set; }
        public int ScoreboardID { get; set; }

    }
}