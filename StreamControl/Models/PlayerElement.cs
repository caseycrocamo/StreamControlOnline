using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StreamControl.Models
{
    public class PlayerElement : Element
    {
        public string Value { get; set; }
        public string Character { get; set; }

        public PlayerElement() { }

        public PlayerElement(Player player)
        {
            Value = player.Name;
        }
    }
}