using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StreamControl.Models
{
    public class View
    {
        public int ViewID { get; set; }
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public virtual ICollection<Style> Style { get; set; }
        public int OverlayID { get; set; }
    }
}