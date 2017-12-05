using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StreamControl.Models
{
    public class Overlay
    {
        public int ScoreboardID { get; set; }
        public string OwnerID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Element> Elements { get; set; }
        public virtual ICollection<View> Views { get; set; }
    }
}