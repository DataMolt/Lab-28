using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_28.Models
{
    public class Draw
    {
        public bool success { get; set; }
        public string deck_id { get; set; }
        public Card[] cards { get; set; }
        public int remaining { get; set; }
    }

    public class Card
    {
        public string code { get; set; }
        public string value { get; set; }
        public string image { get; set; }
        public Images images { get; set; }
        public string suit { get; set; }
    }

    public class Images
    {
        public string png { get; set; }
        public string svg { get; set; }
    }

}
