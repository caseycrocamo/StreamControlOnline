using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StreamControl.Models
{
    public class Scoreboard
    {
        public int ScoreboardID { get; set; }
        public string OwnerID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Field> Fields { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<View> Views { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}