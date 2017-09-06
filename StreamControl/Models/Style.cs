using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StreamControl.Models
{
    public class Style
    {
        public int StyleID { get; set; }
        public string DivID { get; set; }
        public string Display { get; set; }
        public string Position { get; set; }
        public string Left { get; set; }
        public string Top { get; set; }
        public string Right { get; set; }
        public string Bottom { get; set; }
        public string TextAlign { get; set; }
        public string Color { get; set; }
        public string BackgroundColor { get; set; }
        public string Font { get; set; }
        public string Width { get; set; }
        public string FontSize { get; set; }
        public string Height { get; set; }
        public string Padding { get; set; }
    }
}