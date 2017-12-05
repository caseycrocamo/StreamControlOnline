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
        public virtual ICollection<TextElement> TextElements { get; set; }
        public virtual ICollection<PlayerElement> PlayerElements { get; set; }
        public virtual ICollection<View> Views { get; set; }
    }
}